using CameraControl;
using System;
using System.ComponentModel;
using System.Threading;
using System.Windows.Forms;

namespace p36_photo_table
{ 
    public class TableController
    {
        private const bool useCamera = true;

        private CameraController cameraController;
        private ArduinoController arduinoController;

        private int horizontalIncrementValue;
        private int verticalIncrementValue;
        private float partHeight;
        private float partLength;
        private float partWidth;
        private const double TABLE_STEPS_PER_DEGREE = 32000.0d/360.0d;
        private const double HORIZONTAL_STEPS_PER_CM = 100.2550822d;
        private const int MAX_HORIZONTAL_STEPS = 5220;
        private const double VERTICAL_STEPS_PER_CM = 800.0d/0.3d;
        private const int MAX_VERTICAL_STEPS = 140467;
        private const double CAMERA_STEPS_PER_DEGREE = 1600.0d/360.0d;
        private const double Y_MIN = 7.62d; // 3 inches

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
            int pictureNumber = 1;
            int totalNumPictures = GetTotalNumPictures();

            this.arduinoController.Home();

            for (int currentHorizontalAngle = 0; currentHorizontalAngle < 360; currentHorizontalAngle += horizontalIncrementValue)
            {
                if (backgroundWorker.CancellationPending)
                {
                    return;
                }

                int tableSteps = currentHorizontalAngle == 0 ? 0 : GetTableStepsFromAngle(horizontalIncrementValue);
                Console.WriteLine($"table steps: {tableSteps}");

                bool succeeded = this.arduinoController.MoveMotors(0, 0, tableSteps, 0);
                if (!succeeded)
                {
                    return;
                }

                Thread.Sleep(2000);

                for (int currentCameraAngle = 90; currentCameraAngle >= 0; currentCameraAngle -= verticalIncrementValue)
                {
                    if (backgroundWorker.CancellationPending)
                    {
                        return;
                    }

                    statusLabel.Invoke((MethodInvoker)delegate {
                        statusLabel.Text = $"Taking picture with horizontal angle {currentHorizontalAngle} and vertical angle {currentCameraAngle}";
                    });
                    statusNumberLabel.Invoke((MethodInvoker)delegate {
                        statusNumberLabel.Text = $"Picture {pictureNumber}/{totalNumPictures}";
                    });
                    statusBar.Invoke((MethodInvoker)delegate {
                        statusBar.Value = (int)((float)pictureNumber / totalNumPictures * 100);
                    });
                    pictureNumber++;

                    Tuple<double, double> projectedArea = GetProjectedArea(currentHorizontalAngle, currentCameraAngle);
                    Console.WriteLine($"projected: {projectedArea}");
                    Tuple<double, double> horizontalAndVerticalPosition = GetHorizontalAndVerticalPosition(projectedArea, currentHorizontalAngle, currentCameraAngle);
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

                    int cameraSteps;
                    if (currentCameraAngle == 90)
                    {
                        cameraSteps = 400;
                    }
                    else
                    {
                        cameraSteps = GetCameraStepsFromAngle(verticalIncrementValue);
                    }
                    currentCameraSteps += cameraSteps;

                    Console.WriteLine($"expected: {expectedHorizontalSteps} {expectedVerticalSteps} {cameraSteps}");
                    Console.WriteLine($"increments: {horizontalSteps} {verticalSteps} {cameraSteps}");



                    if (verticalSteps < 0)
                    {
                        // move vertical first then horizontal
                        succeeded = this.arduinoController.MoveMotors(verticalSteps, 0, 0, 0);
                        if (!succeeded)
                        {
                            return;
                        }

                        succeeded = this.arduinoController.MoveMotors(0, horizontalSteps, 0, cameraSteps);
                        if (!succeeded)
                        {
                            return;
                        }
                    }
                    else
                    {
                        // move in sync
                        succeeded = this.arduinoController.MoveMotors(verticalSteps, horizontalSteps, 0, cameraSteps);
                        if (!succeeded)
                        {
                            return;
                        }
                    }

                    if (currentCameraAngle == 0)
                    {
                        cameraSteps = GetCameraStepsFromMinTheta(horizontalAndVerticalPosition.Item1);
                        succeeded = this.arduinoController.MoveMotors(0, 0, 0, cameraSteps);
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

                    if (currentCameraAngle == 0)
                    {
                        succeeded = this.arduinoController.MoveMotors(0, 0, 0, -cameraSteps);
                        if (!succeeded)
                        {
                            return;
                        }
                    }
                }
            }

            this.arduinoController.Home();
        }

        private int GetCameraStepsFromMinTheta(double x)
        {
            double yPrime = Y_MIN - (partHeight / 2);
            if (yPrime < 0)
            {
                return 0;
            }
            double thetaMinDegrees = Math.Tanh(yPrime / x);
            return (int)Math.Round(thetaMinDegrees * CAMERA_STEPS_PER_DEGREE);
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

        private int GetCameraStepsFromAngle(double currentCameraAngle)
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

        public Tuple<double, double> GetHorizontalAndVerticalPosition(Tuple<double, double> projectedArea, float currentHorizontalAngleDegrees, int cameraAngleDegrees)
        {
            double projectedWidth = projectedArea.Item1;
            double projectedHeight = projectedArea.Item2;
            float cameraAngleRadians = cameraAngleDegrees * (float)(Math.PI / 180);

            double horizontalDistance = (projectedWidth / 2) / Math.Tan(CAMERA_HORIZONTAL_FOV / 2);
            double verticalDistance = (projectedHeight / 2) / Math.Tan(CAMERA_VERTICAL_FOV / 2);

            double distance = Math.Max(horizontalDistance, verticalDistance);
            double effectiveDepth = GetEffectiveDepth(currentHorizontalAngleDegrees, cameraAngleDegrees);

            double halfPartWidth = effectiveDepth / 2;
            Console.WriteLine($"effective depth: {effectiveDepth}");

            double partOffset = cameraAngleDegrees == 90 ? partHeight : halfPartWidth / Math.Cos(cameraAngleRadians);
            double offsetX = Math.Cos(cameraAngleRadians) * partOffset;
            double offsetY = Math.Sin(cameraAngleRadians) * partOffset;
            Console.WriteLine($"offset: {offsetX} {offsetY}");

            double x = Math.Cos(cameraAngleRadians) * distance + offsetX;
            double y = Math.Sin(cameraAngleRadians) * distance + offsetY;
            return new Tuple<double, double>(x, y);
        }

        double GetEffectiveDepth(float turntableAngleDegrees, float cameraAngleDegrees)
        {
            double turntableAngleRadians = turntableAngleDegrees * (Math.PI / 180);
            double cameraAngleRadians = cameraAngleDegrees * (Math.PI / 180);

            double dx = partLength * Math.Cos(turntableAngleRadians);
            double dz = partWidth * Math.Sin(turntableAngleRadians);

            double effectiveDepth = Math.Abs(dx + dz);
            return effectiveDepth;
        }

        internal void CloseSession()
        {
            if (useCamera)
            {
                this.cameraController.CloseSession();
            }
            this.arduinoController.CloseSession();
        }

        internal int GetTotalNumPictures()
        {
            return (360 / horizontalIncrementValue) * (90 / verticalIncrementValue);
        }
    }
}