using CameraControl;
using System.Windows.Forms;

namespace p36_photo_table
{
    public class TableController
    {
        private CameraController cameraController;
        private ArduinoController arduinoController;

        private int horizontalIncrementValue;
        private int verticalIncrementValue;
        private float partHeight;
        private float partLength;
        private float partWidth;

        private float currentCameraPos; // degrees
        private float currentTablePos; // degrees
        private float currentVerticalPos; // cm
        private float currentHorizontalPos; // cm

        public TableController(int horizontalIncrementValue, int verticalIncrementValue, float partHeight, float partLength, float partWidth, string fileLocation, string filePrefix)
        {
            this.horizontalIncrementValue = horizontalIncrementValue;
            this.verticalIncrementValue = verticalIncrementValue;
            this.partHeight = partHeight;
            this.partLength = partLength;
            this.partWidth = partWidth;
            this.cameraController = new CameraController(fileLocation, filePrefix);
            this.arduinoController = new ArduinoController();
        }

        public void Start(ProgressBar statusBar, Label statusNumberLabel, Label statusLabel)
        {
            this.arduinoController.Home();
            
            this.arduinoController.MoveMotors(-1000, 0, 1000, 0);

            //this.cameraController.TakePicture();
        }

        internal void CloseSession()
        {
            this.cameraController.CloseSession();
        }
    }
}