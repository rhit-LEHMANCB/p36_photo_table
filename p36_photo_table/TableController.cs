using CameraControl;
using System;
using System.ComponentModel;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace p36_photo_table
{ 
    public class TableController
    {
        private const bool useCamera = false;

        private CameraController cameraController;
        private ArduinoController arduinoController;

        private int horizontalIncrementValue;
        private int verticalIncrementValue;
        private float partHeight;
        private float partLength;
        private float partWidth;

        private const double TABLE_STEPS_PER_DEGREE = 4000.0d/360.0d;
        private const double HORIZONTAL_STEPS_PER_CM = 100.0d;
        private const double VERTICAL_STEPS_PER_CM = 800.0d/0.3d;
        private const double CAMERA_STEPS_PER_DEGREE = 1600.0d/360.0d;

        private const double LENS_OFFSET_WIDTH = 6.0f; // cm
        private const double LENS_OFFSET_HEIGHT = 6.0f; // cm
        private const double TILT_HEAD_HEIGHT = 8.0f; // cm

        private int currentVerticalSteps;
        private int currentHorizontalSteps;
        private int currentCameraSteps;

        public TableController(int horizontalIncrementValue, int verticalIncrementValue, float partHeight, float partLength, float partWidth, string fileLocation, string filePrefix)
        {
            this.horizontalIncrementValue = horizontalIncrementValue;
            this.verticalIncrementValue = verticalIncrementValue;
            this.partHeight = partHeight;
            this.partLength = partLength;
            this.partWidth = partWidth;
            if (useCamera)
            {
                this.cameraController = new CameraController(fileLocation, filePrefix);
            }

            this.arduinoController = new ArduinoController();

            if (useCamera)
            {
                this.cameraController.InitializeCamera();
            }
        }

        public void Start(BackgroundWorker backgroundWorker, ProgressBar statusBar, Label statusNumberLabel, Label statusLabel)
        {
            this.arduinoController.Home();

            for (int currentHorizontalAngle = 0; currentHorizontalAngle < 360; currentHorizontalAngle += horizontalIncrementValue)
            {
                if (backgroundWorker.CancellationPending)
                {
                    return;
                }

                int tableSteps = GetTableStepsFromAngle(horizontalIncrementValue);

                bool succeeded = this.arduinoController.MoveMotors(0, 0, tableSteps, 0);
                if (!succeeded)
                {
                    return;
                }

                Thread.Sleep(1000);

                for (int currentCameraAngle = 90; currentCameraAngle >= 0; currentCameraAngle -= verticalIncrementValue)
                {
                    if (backgroundWorker.CancellationPending)
                    {
                        return;
                    }

                    Tuple<float, float> projectedArea = GetProjectedArea(currentHorizontalAngle, currentCameraAngle);
                    Tuple<float, float> horizontalAndVerticalPosition = GetHorizontalAndVerticalPosition(projectedArea, currentCameraAngle);

                    int verticalSteps = GetVerticalStepsFromPosition(horizontalAndVerticalPosition.Item1);
                    currentVerticalSteps += verticalSteps;
                    int horizontalSteps = GetHorizontalStepsFromPosition(horizontalAndVerticalPosition.Item2);
                    currentHorizontalSteps += horizontalSteps;
                    int cameraSteps = GetCameraStepsFromAngle(verticalIncrementValue);
                    currentCameraSteps += cameraSteps;

                    succeeded = this.arduinoController.MoveMotors(verticalSteps, horizontalSteps, 0, cameraSteps);
                    if (!succeeded)
                    {
                        return;
                    }

                    Thread.Sleep(1000);

                    if (useCamera)
                    {
                        this.cameraController.TakePicture(currentHorizontalAngle, currentCameraAngle);
                    }
                }

                ResetCameraPosition();
            }
        }

        private int GetTableStepsFromAngle(int horizontalIncrementValue)
        {
            return (int)Math.Round(horizontalIncrementValue * TABLE_STEPS_PER_DEGREE);
        }

        private void ResetCameraPosition()
        {
            int verticalSteps = GetVerticalStepsFromPosition(0);
            currentVerticalSteps = 0;
            int horizontalSteps = GetHorizontalStepsFromPosition(0);
            currentHorizontalSteps = 0;
            int cameraSteps = -currentCameraSteps;
            currentCameraSteps = 0;

            this.arduinoController.MoveMotors(verticalSteps, horizontalSteps, 0, cameraSteps);
        }

        private int GetCameraStepsFromAngle(int currentCameraAngle)
        {
            return -(int)Math.Round(currentCameraAngle * CAMERA_STEPS_PER_DEGREE);
        }

        private int GetHorizontalStepsFromPosition(float x)
        {
            int expectedSteps = (int)Math.Round(x * HORIZONTAL_STEPS_PER_CM);
            return currentHorizontalSteps - expectedSteps;
        }

        private int GetVerticalStepsFromPosition(float y)
        {
            int expectedSteps = (int)Math.Round(y * VERTICAL_STEPS_PER_CM);
            return currentVerticalSteps - expectedSteps;
        }

        public Tuple<float, float> GetProjectedArea(float tableAngleDegrees, float cameraAngleDegrees) 
        {
            float tableAngleRadians = tableAngleDegrees * (float)(Math.PI / 180);
            float cameraAngleRadians = cameraAngleDegrees * (float)(Math.PI / 180);
            double projectedWidth = partLength * Math.Sin(cameraAngleRadians) + partWidth * Math.Cos(cameraAngleRadians);
            double projectedHeight = (partLength * Math.Cos(cameraAngleRadians) + partWidth * Math.Sin(cameraAngleRadians)) * Math.Sin(tableAngleRadians) + partHeight * Math.Cos(tableAngleRadians);
            return new Tuple<float, float>((float)projectedWidth, (float)projectedHeight);
        }

        public Tuple<float, float> GetHorizontalAndVerticalPosition(Tuple<float, float> projectedArea, float cameraAngleDegrees)
        {
            float projectedWidth = projectedArea.Item1;
            float projectedHeight = projectedArea.Item2;
            float cameraAngleRadians = cameraAngleDegrees * (float)(Math.PI / 180);
            float distance = Math.Max(2.0f / 3.0f * projectedWidth, projectedHeight);
            double totalDistance = (projectedHeight / 2.0f * Math.Tan(cameraAngleRadians)) + distance + LENS_OFFSET_WIDTH;
            double x = Math.Sin(cameraAngleRadians) * totalDistance - Math.Cos(cameraAngleRadians) * LENS_OFFSET_HEIGHT - TILT_HEAD_HEIGHT;
            double y = Math.Cos(cameraAngleRadians) * totalDistance + Math.Sin(cameraAngleRadians) * LENS_OFFSET_HEIGHT;
            return new Tuple<float, float>((float)x, (float)y);
        }

        internal void CloseSession()
        {
            if (useCamera)
            {
                this.cameraController.CloseSession();
            }
            this.arduinoController.CloseSession();
        }
    }
}