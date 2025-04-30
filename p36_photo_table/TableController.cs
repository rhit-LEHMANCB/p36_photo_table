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
        private const double HORIZONTAL_STEPS_PER_CM = 100.2550822d;
        private const int MAX_HORIZONTAL_STEPS = 5220;
        private const double VERTICAL_STEPS_PER_CM = 800.0d/0.3d;
        private const int MAX_VERTICAL_STEPS = 135467;
        private const double CAMERA_STEPS_PER_DEGREE = 1600.0d/360.0d;

        private const float CAMERA_HORIZONTAL_FOV = 70 * (float)(Math.PI / 180); // radians
        private const float CAMERA_VERTICAL_FOV = 50 * (float)(Math.PI / 180); // radians

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
            //this.arduinoController.Home();

            bool succeeded = this.arduinoController.MoveMotors(0, 0, 0, 400);
            if (!succeeded)
            {
                return;
            }

            for (int currentHorizontalAngle = 0; currentHorizontalAngle < 360; currentHorizontalAngle += horizontalIncrementValue)
            {
                if (backgroundWorker.CancellationPending)
                {
                    return;
                }

                int tableSteps = GetTableStepsFromAngle(horizontalIncrementValue);

                //bool succeeded = this.arduinoController.MoveMotors(0, 0, tableSteps, 0);
                //if (!succeeded)
                //{
                //    return;
                //}

                Thread.Sleep(2000);

                for (int currentCameraAngle = 90; currentCameraAngle >= 0; currentCameraAngle -= verticalIncrementValue)
                {
                    if (backgroundWorker.CancellationPending)
                    {
                        return;
                    }

                    Tuple<double, double> projectedArea = GetProjectedArea(currentHorizontalAngle, currentCameraAngle);
                    Console.WriteLine($"projected: {projectedArea}");
                    Tuple<double, double> horizontalAndVerticalPosition = GetHorizontalAndVerticalPosition(projectedArea, currentCameraAngle);
                    Console.WriteLine($"positions: {horizontalAndVerticalPosition}");

                    int expectedHorizontalSteps = GetHorizontalStepsFromPosition(horizontalAndVerticalPosition.Item1);
                    expectedHorizontalSteps = Math.Min(expectedHorizontalSteps, MAX_HORIZONTAL_STEPS);
                    expectedHorizontalSteps = Math.Max(expectedHorizontalSteps, 0);
                    int horizontalSteps = expectedHorizontalSteps - currentHorizontalSteps;
                    currentHorizontalSteps = expectedHorizontalSteps;

                    int expectedVerticalSteps = GetVerticalStepsFromPosition(horizontalAndVerticalPosition.Item2);
                    expectedVerticalSteps = Math.Min(expectedVerticalSteps, MAX_VERTICAL_STEPS);
                    expectedVerticalSteps = Math.Max(expectedVerticalSteps, 0);
                    int verticalSteps = expectedVerticalSteps - currentVerticalSteps;
                    currentVerticalSteps = expectedVerticalSteps;

                    int cameraSteps = currentCameraAngle == 90 ? 0 : GetCameraStepsFromAngle(verticalIncrementValue);
                    currentCameraSteps += cameraSteps;

                    Console.WriteLine($"expected: {expectedHorizontalSteps} {expectedVerticalSteps} {cameraSteps}");
                    Console.WriteLine($"increments: {horizontalSteps} {verticalSteps} {cameraSteps}");

                    

                    if (verticalSteps < 0)
                    {
                        // move vertical first then horizontal
                        succeeded = this.arduinoController.MoveMotors(verticalSteps, 0, 0, cameraSteps);
                        if (!succeeded)
                        {
                            return;
                        }

                        //succeeded = this.arduinoController.MoveMotors(0, horizontalSteps, 0, cameraSteps);
                        //if (!succeeded)
                        //{
                        //    return;
                        //}
                    }
                    else
                    {
                        // move in sync
                        //bool succeeded = this.arduinoController.MoveMotors(verticalSteps, horizontalSteps, 0, cameraSteps);
                        //if (!succeeded)
                        //{
                        //    return;
                        //}
                        succeeded = this.arduinoController.MoveMotors(verticalSteps, 0, 0, cameraSteps);
                        if (!succeeded)
                        {
                            return;
                        }
                    }

                    Thread.Sleep(2000);

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
            int cameraSteps = -currentCameraSteps;
            currentCameraSteps = 0;

            Console.WriteLine($"resetting: {cameraSteps}");
            this.arduinoController.MoveMotors(0, 0, 0, cameraSteps);
        }

        private int GetCameraStepsFromAngle(int currentCameraAngle)
        {
            return -(int)Math.Round(currentCameraAngle * CAMERA_STEPS_PER_DEGREE);
        }

        private int GetHorizontalStepsFromPosition(double x)
        {
            return (int)Math.Round(x * HORIZONTAL_STEPS_PER_CM);
        }

        private int GetVerticalStepsFromPosition(double y)
        {
            return MAX_VERTICAL_STEPS - (int)Math.Round(y * VERTICAL_STEPS_PER_CM);
        }

        public Tuple<double, double> GetProjectedArea(float tableAngleDegrees, float cameraAngleDegrees) 
        {
            float tableAngleRadians = tableAngleDegrees * (float)(Math.PI / 180);
            float cameraAngleRadians = cameraAngleDegrees * (float)(Math.PI / 180);
            double projectedWidth = partLength * Math.Sin(cameraAngleRadians) + partWidth * Math.Cos(cameraAngleRadians);
            double projectedHeight = (partLength * Math.Cos(cameraAngleRadians) + partWidth * Math.Sin(cameraAngleRadians)) * Math.Sin(tableAngleRadians) + partHeight * Math.Cos(tableAngleRadians);
            return new Tuple<double, double>(projectedWidth, projectedHeight);
        }

        public Tuple<double, double> GetHorizontalAndVerticalPosition(Tuple<double, double> projectedArea, float cameraAngleDegrees)
        {
            double projectedWidth = projectedArea.Item1;
            double projectedHeight = projectedArea.Item2;
            float cameraAngleRadians = cameraAngleDegrees * (float)(Math.PI / 180);
            double horizontalDistance = (projectedWidth / 2) / Math.Tan(CAMERA_HORIZONTAL_FOV / 2);
            double verticalDistance = (projectedHeight / 2) / Math.Tan(CAMERA_VERTICAL_FOV / 2);
            double distance = Math.Max(horizontalDistance, verticalDistance);
            double x = Math.Cos(cameraAngleRadians) * distance;
            double y = Math.Sin(cameraAngleRadians) * distance;
            return new Tuple<double, double>(x, y);
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