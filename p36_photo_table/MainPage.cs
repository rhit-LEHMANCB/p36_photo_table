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
        private const float partMinSizeExclusive = 0.0f;
        private const float partMaxSizeExclusive = 100.0f;
        private string incrementValidationText = $"Must be a multiple of {angleMultiple} between {minAngleIncrement} and {maxAngleIncrement}.";
        private string partSizeValidationText = $"Must be a decimal between\n{partMinSizeExclusive} and {partMaxSizeExclusive}, exclusive.";
        private string filePrefixValidationText = $"Must be a valid prefix. Please use alphabet characters.";


        private int horizontalIncrementValue;
        private int verticalIncrementValue;
        private float partHeight;
        private float partLength;
        private float partWidth;
        private string fileLocation;
        private string filePrefix;

        private bool isHorizontalIncrementValid;
        private bool isVerticalIncrementValid;
        private bool isPartHeightValid;
        private bool isPartLengthValid;
        private bool isPartWidthValid;
        private bool isFileLocationValid;
        private bool isFilePrefixValid;

        private bool isFormValid;

        private bool isShowingPopup;

        private bool isSDKLoaded = false;

        public MainPage()
        {
            InitializeComponent();
            horizontalIncrementValue = minAngleIncrement;
            verticalIncrementValue = minAngleIncrement;
            partHeight = 0.0f;
            partWidth = 0.0f;
            partLength = 0.0f;
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
            isFormValid = false;
            isShowingPopup = false;
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
            float value = 0.0f;
            if (float.TryParse(partWidthInput.Text, out value))
            {
                if (value > partMinSizeExclusive && value < partMaxSizeExclusive)
                {
                    isPartWidthValid = true;
                    partWidthValidationLabel.Text = "";
                    this.partWidth = value;
                }
                else
                {
                    isPartWidthValid = false;
                    partWidthValidationLabel.Text = partSizeValidationText;
                }
            }
            else
            {
                isPartWidthValid = false;
                partWidthValidationLabel.Text = partSizeValidationText;
            }
            ValidateForm();
        }

        private void partLengthInput_TextChanged(object sender, EventArgs e)
        {
            float value = 0.0f;
            if (float.TryParse(partLengthInput.Text, out value))
            {
                if (value > partMinSizeExclusive && value < partMaxSizeExclusive)
                {
                    isPartLengthValid = true;
                    partLengthValidationLabel.Text = "";
                    this.partLength = value;
                }
                else
                {
                    isPartLengthValid = false;
                    partLengthValidationLabel.Text = partSizeValidationText;
                }
            }
            else
            {
                isPartLengthValid = false;
                partLengthValidationLabel.Text = partSizeValidationText;
            }
            ValidateForm();
        }

        private void partHeightInput_TextChanged(object sender, EventArgs e)
        {
            float value = 0.0f;
            if (float.TryParse(partHeightInput.Text, out value))
            {
                if (value > partMinSizeExclusive && value < partMaxSizeExclusive)
                {
                    isPartHeightValid = true;
                    partHeightValidationLabel.Text = "";
                    this.partHeight = value;
                }
                else
                {
                    isPartHeightValid = false;
                    partHeightValidationLabel.Text = partSizeValidationText;
                }
            }
            else
            {
                isPartHeightValid = false;
                partHeightValidationLabel.Text = partSizeValidationText;
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
                isFilePrefixValid)
            {
                isFormValid = true;
                startButton.BackColor = Color.LimeGreen;
                startButton.Enabled = true;
            }
            else
            {
                isFormValid = false;
                startButton.BackColor = Color.Gray;
                startButton.Enabled = false;
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
            InProgressPage popup = new InProgressPage();

            if (isSDKLoaded)
            {
                isShowingPopup = true;
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
    }
}
