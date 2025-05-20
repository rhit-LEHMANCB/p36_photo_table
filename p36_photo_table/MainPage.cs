using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace p36_photo_table
{
    public partial class MainPage : Form
    {
        private const int maxAngleIncrement = 45;
        private const int minAngleIncrement = 5;
        private const int angleMultiple = 5;
        private const float partMinSizeExclusiveCm = 0.0f;
        private const float partMaxSizeExclusiveCm = 100.0f;
        private const float partMinSizeExclusiveIn = 0.0f;
        private const float partMaxSizeExclusiveIn = 39.37f;
        private const float domeMinSizeExclusiveCm = 0.0f;
        private const float domeMaxSizeExclusiveCm = 25.0f;
        private const float domeMinSizeExclusiveIn = 0.0f;
        private const float domeMaxSizeExclusiveIn = 9.84f;
        private string incrementValidationText = $"Must be a multiple of {angleMultiple} between {minAngleIncrement} and {maxAngleIncrement}.";
        private string partSizeValidationTextCm = $"Must be a decimal between\n{partMinSizeExclusiveCm} and {partMaxSizeExclusiveCm}, exclusive.";
        private string partSizeValidationTextIn = $"Must be a decimal between\n{partMinSizeExclusiveIn} and {partMaxSizeExclusiveIn}, exclusive.";
        private string filePrefixValidationText = $"Must be a valid prefix. Please use alphabet characters.";
        private string offsetValidationTextCm = $"Must be a decimal between\n{domeMinSizeExclusiveCm} and {domeMaxSizeExclusiveCm}, exclusive.";
        private string offsetValidationTextIn = $"Must be a decimal between\n{domeMinSizeExclusiveIn} and {domeMaxSizeExclusiveIn}, exclusive.";
        private string delayValidationText = $"Must be a positive integer.";


        private int horizontalIncrementValue;
        private int verticalIncrementValue;
        private float partHeight;
        private float partLength;
        private float partWidth;
        private string fileLocation;
        private string filePrefix;
        private float domeOffset;
        private int settleDelaySeconds;

        private bool isHorizontalIncrementValid;
        private bool isVerticalIncrementValid;
        private bool isPartHeightValid;
        private bool isPartLengthValid;
        private bool isPartWidthValid;
        private bool isFileLocationValid;
        private bool isFilePrefixValid;
        private bool isDelayValid;
        private bool isDomeOffsetValid;

        private bool isSDKLoaded = false;

        private MeasuringUnit unit = MeasuringUnit.CM;

        public MainPage()
        {
            InitializeComponent();
            horizontalIncrementValue = 45;
            verticalIncrementValue = 45;
            partHeight = 0.0f;
            partWidth = 0.0f;
            partLength = 0.0f;
            domeOffset = 3.0f;
            settleDelaySeconds = 2;
            fileLocation = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            folderBrowserDialog.SelectedPath = fileLocation;
            fileLocationInput.Text = $"Current: {fileLocation}";
            filePrefix = "";
            isHorizontalIncrementValid = true;
            isVerticalIncrementValid = true;
            isPartHeightValid = false;
            isPartLengthValid = false;
            isPartWidthValid = false;
            isFileLocationValid = true;
            isFilePrefixValid = false;
            isDelayValid = true;
            isDomeOffsetValid = true;
            isSDKLoaded = CameraControl.CameraController.InitializeSDK();
        }

        private void MainPage_Load(object sender, EventArgs e)
        {
            GenerateTickMarks(horizontalIncrement);
            GenerateTickMarks(verticalIncrement);
        }

        private void GenerateTickMarks(TrackBar trackBar)
        {
            int x = trackBar.Location.X;
            int y = trackBar.Location.Y;
            for (int i = trackBar.Minimum; i <= trackBar.Maximum; i += trackBar.TickFrequency)
            {
                Label label = new Label();
                label.Text = (i * angleMultiple).ToString();
                label.Location = new Point(x - 23 + i * 30, y + 40);
                label.AutoSize = true;
                this.Controls.Add(label);
                label.BringToFront();

            }
        }

        private void horizontalIncrement_Scroll(object sender, EventArgs e)
        {
            this.horizontalIncrementValue = horizontalIncrement.Value * angleMultiple;
            UpdateHorizontalTrackbarView();
            ValidateForm();
        }

        private void horizontalIncrementValueBox_TextChanged(object sender, EventArgs e)
        {
            int value = minAngleIncrement;
            if (Int32.TryParse(horizontalIncrementValueBox.Text, out value))
            {
                if (value >= minAngleIncrement && value <= maxAngleIncrement && value % angleMultiple == 0)
                {
                    isHorizontalIncrementValid = true;
                    horizontalValidationLabel.Text = "";
                    this.horizontalIncrementValue = value;
                    UpdateHorizontalTrackbarView();
                }
                else
                {
                    isHorizontalIncrementValid = false;
                    horizontalValidationLabel.Text = incrementValidationText;
                }
            }
            else
            {
                isVerticalIncrementValid = false;
                horizontalValidationLabel.Text = incrementValidationText;
            }
            ValidateForm();
        }

        private void verticalIncrement_Scroll(object sender, EventArgs e)
        {
            this.verticalIncrementValue = verticalIncrement.Value * angleMultiple;
            UpdateVerticalTrackbarView();
            ValidateForm();
        }

        private void verticalIncrementValueBox_TextChanged(object sender, EventArgs e)
        {
            int value = minAngleIncrement;
            if (Int32.TryParse(verticalIncrementValueBox.Text, out value))
            {
                if (value >= minAngleIncrement && value <= maxAngleIncrement && value % angleMultiple == 0)
                {
                    isVerticalIncrementValid = true;
                    verticalValidationLabel.Text = "";
                    this.verticalIncrementValue = value;
                    UpdateVerticalTrackbarView();
                }
                else
                {
                    isVerticalIncrementValid = false;
                    verticalValidationLabel.Text = incrementValidationText;
                }
            }
            else
            {
                isVerticalIncrementValid = false;
                verticalValidationLabel.Text = incrementValidationText;
            }
            ValidateForm();
        }

        private void UpdateHorizontalTrackbarView()
        {
            UpdateTrackbarView(this.horizontalIncrementValue, horizontalIncrement, horizontalIncrementValueBox);
        }

        private void UpdateVerticalTrackbarView()
        {
            UpdateTrackbarView(this.verticalIncrementValue, verticalIncrement, verticalIncrementValueBox);
        }

        private void UpdateTrackbarView(int value, TrackBar increment, TextBox textBox)
        {
            textBox.Text = value.ToString();
            increment.Value = value / angleMultiple;
        }

        private void partWidthInput_TextChanged(object sender, EventArgs e)
        {
            float min;
            float max;
            string validationText;
            if (unit == MeasuringUnit.CM)
            {
                min = partMinSizeExclusiveCm;
                max = partMaxSizeExclusiveCm;
                validationText = partSizeValidationTextCm;
            }
            else
            {
                min = partMinSizeExclusiveIn;
                max = partMaxSizeExclusiveIn;
                validationText = partSizeValidationTextIn;
            }
            float value = 0.0f;
            if (float.TryParse(partWidthInput.Text, out value))
            {
                if (value > min && value < max)
                {
                    isPartWidthValid = true;
                    partWidthValidationLabel.Text = "";
                    this.partWidth = value;
                }
                else
                {
                    isPartWidthValid = false;
                    partWidthValidationLabel.Text = validationText;
                }
            }
            else
            {
                isPartWidthValid = false;
                partWidthValidationLabel.Text = validationText;
            }
            ValidateForm();
        }

        private void partLengthInput_TextChanged(object sender, EventArgs e)
        {
            float min;
            float max;
            string validationText;
            if (unit == MeasuringUnit.CM)
            {
                min = partMinSizeExclusiveCm;
                max = partMaxSizeExclusiveCm;
                validationText = partSizeValidationTextCm;
            }
            else
            {
                min = partMinSizeExclusiveIn;
                max = partMaxSizeExclusiveIn;
                validationText = partSizeValidationTextIn;
            }
            float value = 0.0f;
            if (float.TryParse(partLengthInput.Text, out value))
            {
                if (value > min && value < max)
                {
                    isPartLengthValid = true;
                    partLengthValidationLabel.Text = "";
                    this.partLength = value;
                }
                else
                {
                    isPartLengthValid = false;
                    partLengthValidationLabel.Text = validationText;
                }
            }
            else
            {
                isPartLengthValid = false;
                partLengthValidationLabel.Text = validationText;
            }
            ValidateForm();
        }

        private void partHeightInput_TextChanged(object sender, EventArgs e)
        {
            float min;
            float max;
            string validationText;
            if (unit == MeasuringUnit.CM)
            {
                min = partMinSizeExclusiveCm;
                max = partMaxSizeExclusiveCm;
                validationText = partSizeValidationTextCm;
            }
            else
            {
                min = partMinSizeExclusiveIn;
                max = partMaxSizeExclusiveIn;
                validationText = partSizeValidationTextIn;
            }
            float value = 0.0f;
            if (float.TryParse(partHeightInput.Text, out value))
            {
                if (value > min && value < max)
                {
                    isPartHeightValid = true;
                    partHeightValidationLabel.Text = "";
                    this.partHeight = value;
                }
                else
                {
                    isPartHeightValid = false;
                    partHeightValidationLabel.Text = validationText;
                }
            }
            else
            {
                isPartHeightValid = false;
                partHeightValidationLabel.Text = validationText;
            }
            ValidateForm();
        }

        private void offsetInput_TextChanged(object sender, EventArgs e)
        {
            float min;
            float max;
            string validationText;
            if (unit == MeasuringUnit.CM)
            {
                min = domeMinSizeExclusiveCm;
                max = domeMaxSizeExclusiveCm;
                validationText = offsetValidationTextCm;
            }
            else
            {
                min = domeMinSizeExclusiveIn;
                max = domeMaxSizeExclusiveIn;
                validationText = offsetValidationTextIn;
            }
            float value = 0.0f;
            if (float.TryParse(offsetInput.Text, out value))
            {
                if (value > min && value < max)
                {
                    isDomeOffsetValid = true;
                    offsetValidationLabel.Text = "";
                    this.domeOffset = value;
                }
                else
                {
                    isDomeOffsetValid = false;
                    offsetValidationLabel.Text = validationText;
                }
            }
            else
            {
                isDomeOffsetValid = false;
                offsetValidationLabel.Text = validationText;
            }
            ValidateForm();
        }

        private void preset1515_Click(object sender, EventArgs e)
        {
            this.horizontalIncrementValue = 15;
            this.verticalIncrementValue = 15;
            UpdateHorizontalTrackbarView();
            UpdateVerticalTrackbarView();
            ValidateForm();
        }

        private void preset4545_Click(object sender, EventArgs e)
        {
            this.horizontalIncrementValue = 45;
            this.verticalIncrementValue = 45;
            UpdateHorizontalTrackbarView();
            UpdateVerticalTrackbarView();
            ValidateForm();
        }

        private void ValidateForm()
        {
            if (isHorizontalIncrementValid &&
                isVerticalIncrementValid &&
                isPartHeightValid &&
                isPartLengthValid &&
                isPartWidthValid &&
                isFileLocationValid &&
                isFilePrefixValid &&
                isDomeOffsetValid &&
                isDelayValid)
            {
                startButton.BackColor = Color.LimeGreen;
                startButton.Enabled = true;
                onePictureButton.Enabled = true;
            }
            else
            {
                startButton.BackColor = Color.Gray;
                startButton.Enabled = false;
                onePictureButton.Enabled = false;
            }
        }

        private void fileLocationButton_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                fileLocation = folderBrowserDialog.SelectedPath;
                fileLocationInput.Text = $"Current: {fileLocation}";
            }
            ValidateForm();

        }

        private void filePrefixInput_TextChanged(object sender, EventArgs e)
        {
            string fileName = filePrefixInput.Text;
            if (fileName.Length != 0 && fileName.IndexOfAny(Path.GetInvalidFileNameChars()) < 0)
            {
                filePrefix = fileName;
                filePrefixValidationLabel.Text = "";
                isFilePrefixValid = true;

            }
            else
            {
                isFilePrefixValid = false;
                filePrefixValidationLabel.Text = filePrefixValidationText;
            }
            ValidateForm();
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            float width = partWidth;
            float length = partLength;
            float height = partHeight;
            float offset = this.domeOffset;
            if (unit == MeasuringUnit.INCHES)
            {
                width = width * 2.54f;
                length = length * 2.54f;
                height = height * 2.54f;
                offset = offset * 2.54f;
            }
            InProgressPage popup = new InProgressPage(horizontalIncrementValue, verticalIncrementValue, height, length, width, fileLocation, filePrefix, offset, settleDelaySeconds);

            if (isSDKLoaded)
            {
                if (!popup.IsDisposed)
                {
                    popup.ShowDialog(this);
                }
            }
            else
            {
                MessageBox.Show("SDK not loaded.");
            }
        }

        private void delayInput_TextChanged(object sender, EventArgs e)
        {
            int value = 0;
            if (int.TryParse(delayInput.Text, out value))
            {
                if (value >= 0)
                {
                    isDelayValid = true;
                    delayValidationLabel.Text = "";
                    this.settleDelaySeconds = value;
                }
                else
                {
                    isDelayValid = false;
                    delayValidationLabel.Text = delayValidationText;
                }
            }
            else
            {
                isDelayValid = false;
                delayValidationLabel.Text = delayValidationText;
            }
            ValidateForm();
        }

        private void cmButton_CheckedChanged(object sender, EventArgs e)
        {
            if (cmButton.Checked && unit == MeasuringUnit.INCHES)
            {
                unit = MeasuringUnit.CM;
                partWidthInput.Text = (partWidth * 2.54).ToString("0.##");
                partLengthInput.Text = (partLength * 2.54).ToString("0.##");
                partHeightInput.Text = (partHeight * 2.54).ToString("0.##");
                offsetInput.Text = (domeOffset * 2.54).ToString("0.##");
            }
        }

        private void inchesButton_CheckedChanged(object sender, EventArgs e)
        {
            if (inchesButton.Checked && unit == MeasuringUnit.CM)
            {
                unit = MeasuringUnit.INCHES;
                partWidthInput.Text = (partWidth / 2.54f).ToString("0.##");
                partLengthInput.Text = (partLength / 2.54f).ToString("0.##");
                partHeightInput.Text = (partHeight / 2.54f).ToString("0.##");
                offsetInput.Text = (domeOffset / 2.54f).ToString("0.##");
            }
        }

        private void homeButton_Click(object sender, EventArgs e)
        {
            try
            {
                ArduinoController controller = new ArduinoController();
                controller.Home();
                controller.CloseSession();
                MessageBox.Show("Homing complete");
            }
            catch (ArduinoNotFoundException)
            {
                MessageBox.Show("No arduino found. Please make sure it is plugged in.");
                this.Close();
            }
        }

        private void storeButton_Click(object sender, EventArgs e)
        {
            try
            {
                ArduinoController controller = new ArduinoController();
                controller.Home();
                controller.MoveMotors(TableController.MAX_VERTICAL_STEPS / 2, TableController.MAX_HORIZONTAL_STEPS / 2, 0, 0);
                controller.CloseSession();
                MessageBox.Show("System is stored");
            }
            catch (ArduinoNotFoundException)
            {
                MessageBox.Show("No arduino found. Please make sure it is plugged in.");
                this.Close();
            }
        }

        private void onePictureButton_Click(object sender, EventArgs e)
        {
            try
            {
                TableController controller = new TableController(horizontalIncrementValue, verticalIncrementValue, partHeight, partLength, partWidth, fileLocation, filePrefix, domeOffset, settleDelaySeconds);
                controller.TakeTopViewPicture();
            }
            catch (CameraNotFoundException)
            {
                MessageBox.Show("No camera found. Please make sure it is plugged in.");
            }
            catch (ArduinoNotFoundException)
            {
                MessageBox.Show("No arduino found. Please make sure it is plugged in.");
            }
        }
    }

    internal enum MeasuringUnit
    {
        CM,
        INCHES
    }
}
