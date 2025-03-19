using System.Windows.Forms;
using System;

namespace CameraControl
{
    internal class CameraController
    {
        public CameraController()
        {

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
            CameraModel model = null;
            if (err == EDSDKLib.EDSDK.EDS_ERR_OK)
            {
                model = new CameraModel(camera);
            }

            if (err != EDSDKLib.EDSDK.EDS_ERR_OK)
            {
                return false;
            }

            return true;
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
    }
}