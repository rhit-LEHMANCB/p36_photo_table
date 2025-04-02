﻿using CameraControl;
using System.ComponentModel;
using System.IO;
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
        private float currentVerticalPos; // degrees
        private float currentHorizontalPos; // degrees

        public TableController(int horizontalIncrementValue, int verticalIncrementValue, float partHeight, float partLength, float partWidth, string fileLocation, string filePrefix)
        {
            this.horizontalIncrementValue = horizontalIncrementValue;
            this.verticalIncrementValue = verticalIncrementValue;
            this.partHeight = partHeight;
            this.partLength = partLength;
            this.partWidth = partWidth;
            this.cameraController = new CameraController(fileLocation, filePrefix);

            this.arduinoController = new ArduinoController();

            this.cameraController.InitializeCamera();
        }

        public void Start(BackgroundWorker backgroundWorker, ProgressBar statusBar, Label statusNumberLabel, Label statusLabel)
        {
            this.arduinoController.Home();

            for (int i = 0; i < 2; i += 2)
            {
                if (backgroundWorker.CancellationPending)
                {
                    return;
                }

                this.arduinoController.MoveMotors(-1000, 0, 1000, 0);

                this.cameraController.TakePicture(i, i);

                this.arduinoController.MoveMotors(1000, 0, -1000, 0);

                this.cameraController.TakePicture(i + 1, i + 1);
            }
        }

        internal void CloseSession()
        {
            this.cameraController.CloseSession();
        }
    }
}