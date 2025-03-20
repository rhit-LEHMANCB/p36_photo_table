using System.Windows.Forms;
using System;
using EDSDKLib;
using System.Runtime.InteropServices;
using System.Drawing.Imaging;
using System.Drawing;
using System.IO;

namespace CameraControl
{
    internal class CameraController
    {
        private CameraModel cameraModel;
        private int horizontalIncrementValue;
        private int verticalIncrementValue;
        private float partHeight;
        private float partLength;
        private float partWidth;
        private string fileLocation;
        private string filePrefix;

        public CameraController(int horizontalIncrementValue, int verticalIncrementValue, float partHeight, float partLength, float partWidth, string fileLocation, string filePrefix)
        {
            this.horizontalIncrementValue = horizontalIncrementValue;
            this.verticalIncrementValue = verticalIncrementValue;
            this.partHeight = partHeight;
            this.partLength = partLength;
            this.partWidth = partWidth;
            this.fileLocation = fileLocation;
            this.filePrefix = filePrefix;
        }

        public bool InitializeCamera()
        {
            uint err = EDSDKLib.EDSDK.EDS_ERR_OK;

            //Acquisition of camera list
            IntPtr cameraList = IntPtr.Zero;
            if (err == EDSDKLib.EDSDK.EDS_ERR_OK)
            {
                err = EDSDKLib.EDSDK.EdsGetCameraList(out cameraList);
            }

            //Acquisition of number of Cameras
            if (err == EDSDKLib.EDSDK.EDS_ERR_OK)
            {
                int count = 0;
                err = EDSDKLib.EDSDK.EdsGetChildCount(cameraList, out count);
                if (count == 0)
                {
                    err = EDSDKLib.EDSDK.EDS_ERR_DEVICE_NOT_FOUND;
                }
            }


            //Acquisition of camera at the head of the list
            IntPtr camera = IntPtr.Zero;
            if (err == EDSDKLib.EDSDK.EDS_ERR_OK)
            {
                err = EDSDKLib.EDSDK.EdsGetChildAtIndex(cameraList, 0, out camera);
            }

            //Acquisition of camera information
            EDSDKLib.EDSDK.EdsDeviceInfo deviceInfo;
            if (err == EDSDKLib.EDSDK.EDS_ERR_OK)
            {
                err = EDSDKLib.EDSDK.EdsGetDeviceInfo(camera, out deviceInfo);
                if (err == EDSDKLib.EDSDK.EDS_ERR_OK && camera == IntPtr.Zero)
                {
                    err = EDSDKLib.EDSDK.EDS_ERR_DEVICE_NOT_FOUND;
                }
            }


            //Release camera list
            if (cameraList != IntPtr.Zero)
            {
                EDSDKLib.EDSDK.EdsRelease(cameraList);
            }

            //Create Camera model
            if (err == EDSDKLib.EDSDK.EDS_ERR_OK)
            {
                this.cameraModel = new CameraModel(camera);
            }

            if (err == EDSDKLib.EDSDK.EDS_ERR_OK)
            {
                err = EDSDKLib.EDSDK.EdsOpenSession(this.cameraModel.Camera);
            }

            if (err == EDSDK.EDS_ERR_OK)
            {
                err = EDSDK.EdsSetObjectEventHandler(camera, EDSDK.ObjectEvent_All, HandleObjectEvent, IntPtr.Zero);
            }

            if (err == EDSDK.EDS_ERR_OK)
            {
                EDSDK.EdsSaveTo saveTarget = EDSDK.EdsSaveTo.Host;
                err = EDSDK.EdsSetPropertyData(this.cameraModel.Camera, EDSDK.PropID_SaveTo, 0, 4, (uint)saveTarget);
            }

            if (err == EDSDK.EDS_ERR_OK)
            {
                EDSDK.EdsCapacity capacity = new EDSDK.EdsCapacity { NumberOfFreeClusters = 0x7FFFFFFF, BytesPerSector = 0x1000, Reset = 1 };
                err = EDSDK.EdsSetCapacity(camera, capacity);
            }

            if (err != EDSDKLib.EDSDK.EDS_ERR_OK)
            {
                return false;
            }

            return true;
        }

        public void TakePicture()
        {
            EDSDK.EdsSendCommand(cameraModel.Camera, EDSDK.CameraCommand_TakePicture, 0);
        }

        public static bool InitializeSDK()
        {
            uint err = EDSDKLib.EDSDK.EdsInitializeSDK();

            if (err == EDSDKLib.EDSDK.EDS_ERR_OK)
            {
                return true;
            }

            return false;
        }

        static uint HandleObjectEvent(uint inEvent, IntPtr inRef, IntPtr inContext)
        {
            Console.WriteLine($"Object event detected: {inEvent:X}");
            if (inEvent == EDSDK.ObjectEvent_DirItemRequestTransfer)
            {
                Console.WriteLine("New file detected!");

                CapturedItem item = GetCapturedItem(inRef);

                if (!item.IsFolder)
                {
                    SaveImage(item);
                }
            }

            return EDSDK.EDS_ERR_OK;
        }

        private static void SaveImage(CapturedItem item)
        {
            Console.WriteLine("Saving image: " + item.Name);
            using (Image image = Image.FromStream(new MemoryStream(item.Image)))
            {
                image.Save("output.png", ImageFormat.Png);
            }
        }

        private static CapturedItem GetCapturedItem(IntPtr inRef)
        {
            uint err = EDSDK.EDS_ERR_OK;
            IntPtr stream = IntPtr.Zero;

            EDSDK.EdsDirectoryItemInfo dirItemInfo;

            err = EDSDK.EdsGetDirectoryItemInfo(inRef, out dirItemInfo);

            if (err != EDSDK.EDS_ERR_OK)
            {
                throw new Exception("Unable to get captured item info!");
            }

            //  Fill the stream with the resulting image
            if (err == EDSDK.EDS_ERR_OK)
            {
                err = EDSDK.EdsCreateMemoryStream((uint)dirItemInfo.Size, out stream);
            }

            //  Copy the stream to a byte[] and 
            if (err == EDSDK.EDS_ERR_OK)
            {
                err = EDSDK.EdsDownload(inRef, (uint)dirItemInfo.Size, stream);
            }

            CapturedItem item = new CapturedItem();

            if (err == EDSDK.EDS_ERR_OK)
            {
                IntPtr imageRef = IntPtr.Zero;

                err = EDSDK.EdsCreateImageRef(stream, out imageRef);

                if (err == EDSDK.EDS_ERR_OK)
                {
                    EDSDK.EdsDownloadComplete(inRef);
                    EDSDK.EdsRelease(imageRef);
                }
            }

            if (err == EDSDK.EDS_ERR_OK)
            {
                byte[] buffer = new byte[(int)dirItemInfo.Size];

                GCHandle gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);

                IntPtr address = gcHandle.AddrOfPinnedObject();

                IntPtr streamPtr = IntPtr.Zero;

                err = EDSDK.EdsGetPointer(stream, out streamPtr);

                if (err != EDSDK.EDS_ERR_OK)
                {
                    throw new Exception("Unable to get resultant image.");
                }

                try
                {
                    Marshal.Copy(streamPtr, buffer, 0, (int)dirItemInfo.Size);

                    item.Image = buffer;
                    item.Name = dirItemInfo.szFileName;
                    item.Size = (long)dirItemInfo.Size;
                    item.IsFolder = Convert.ToBoolean(dirItemInfo.isFolder);

                    return item;
                }
                catch (AccessViolationException ave)
                {
                    throw new Exception("Error copying unmanaged stream to managed byte[].", ave);
                }
                finally
                {
                    gcHandle.Free();
                    EDSDK.EdsRelease(stream);
                    EDSDK.EdsRelease(streamPtr);
                }
            }
            else
            {
                throw new Exception("Unable to get resultant image.");
            }
        }

        internal void CloseSession()
        {
            EDSDK.EdsCloseSession(cameraModel.Camera);
        }
    }
}