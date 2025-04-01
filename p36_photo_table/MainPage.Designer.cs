namespace p36_photo_table
{
    partial class MainPage
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainPage));
            this.horizontalIncrement = new System.Windows.Forms.TrackBar();
            this.verticalIncrement = new System.Windows.Forms.TrackBar();
            this.horizontalIncrementLabel = new System.Windows.Forms.Label();
            this.verticalIncrementLabel = new System.Windows.Forms.Label();
            this.horizontalCurrentValueLabel = new System.Windows.Forms.Label();
            this.verticalCurrentValueLabel = new System.Windows.Forms.Label();
            this.horizontalIncrementValueBox = new System.Windows.Forms.TextBox();
            this.verticalIncrementValueBox = new System.Windows.Forms.TextBox();
            this.horizontalValidationLabel = new System.Windows.Forms.Label();
            this.tooltip = new System.Windows.Forms.ToolTip(this.components);
            this.filePrefixInfo = new System.Windows.Forms.PictureBox();
            this.fileLocationInfo = new System.Windows.Forms.PictureBox();
            this.verticalIncrementInfo = new System.Windows.Forms.PictureBox();
            this.horizontalIncrementInfo = new System.Windows.Forms.PictureBox();
            this.verticalValidationLabel = new System.Windows.Forms.Label();
            this.parthWidthLabel = new System.Windows.Forms.Label();
            this.partLengthLabel = new System.Windows.Forms.Label();
            this.partHeightLabel = new System.Windows.Forms.Label();
            this.partWidthInput = new System.Windows.Forms.TextBox();
            this.partLengthInput = new System.Windows.Forms.TextBox();
            this.partHeightInput = new System.Windows.Forms.TextBox();
            this.startButton = new System.Windows.Forms.Button();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.presetsLabel = new System.Windows.Forms.Label();
            this.preset1515 = new System.Windows.Forms.Button();
            this.preset4545 = new System.Windows.Forms.Button();
            this.partWidthValidationLabel = new System.Windows.Forms.Label();
            this.partLengthValidationLabel = new System.Windows.Forms.Label();
            this.partHeightValidationLabel = new System.Windows.Forms.Label();
            this.fileLocationButton = new System.Windows.Forms.Button();
            this.fileLocationLabel = new System.Windows.Forms.Label();
            this.fileLocationInput = new System.Windows.Forms.Label();
            this.filePrefixLabel = new System.Windows.Forms.Label();
            this.filePrefixInput = new System.Windows.Forms.TextBox();
            this.filePrefixValidationLabel = new System.Windows.Forms.Label();
            this.verticalIncrementImage = new System.Windows.Forms.PictureBox();
            this.horizontalIncrementImage = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.horizontalIncrement)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.verticalIncrement)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.filePrefixInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fileLocationInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.verticalIncrementInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.horizontalIncrementInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.verticalIncrementImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.horizontalIncrementImage)).BeginInit();
            this.SuspendLayout();
            // 
            // horizontalIncrement
            // 
            this.horizontalIncrement.BackColor = System.Drawing.SystemColors.Window;
            this.horizontalIncrement.LargeChange = 1;
            this.horizontalIncrement.Location = new System.Drawing.Point(106, 208);
            this.horizontalIncrement.Maximum = 9;
            this.horizontalIncrement.Minimum = 1;
            this.horizontalIncrement.Name = "horizontalIncrement";
            this.horizontalIncrement.Size = new System.Drawing.Size(405, 69);
            this.horizontalIncrement.TabIndex = 2;
            this.horizontalIncrement.Value = 1;
            this.horizontalIncrement.Scroll += new System.EventHandler(this.horizontalIncrement_Scroll);
            // 
            // verticalIncrement
            // 
            this.verticalIncrement.BackColor = System.Drawing.SystemColors.Window;
            this.verticalIncrement.LargeChange = 1;
            this.verticalIncrement.Location = new System.Drawing.Point(798, 208);
            this.verticalIncrement.Maximum = 9;
            this.verticalIncrement.Minimum = 1;
            this.verticalIncrement.Name = "verticalIncrement";
            this.verticalIncrement.Size = new System.Drawing.Size(405, 69);
            this.verticalIncrement.TabIndex = 3;
            this.verticalIncrement.Value = 1;
            this.verticalIncrement.Scroll += new System.EventHandler(this.verticalIncrement_Scroll);
            // 
            // horizontalIncrementLabel
            // 
            this.horizontalIncrementLabel.AutoSize = true;
            this.horizontalIncrementLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.horizontalIncrementLabel.Location = new System.Drawing.Point(144, 165);
            this.horizontalIncrementLabel.Name = "horizontalIncrementLabel";
            this.horizontalIncrementLabel.Size = new System.Drawing.Size(275, 32);
            this.horizontalIncrementLabel.TabIndex = 30;
            this.horizontalIncrementLabel.Text = "Horizontal Increment";
            // 
            // verticalIncrementLabel
            // 
            this.verticalIncrementLabel.AutoSize = true;
            this.verticalIncrementLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.verticalIncrementLabel.Location = new System.Drawing.Point(836, 165);
            this.verticalIncrementLabel.Name = "verticalIncrementLabel";
            this.verticalIncrementLabel.Size = new System.Drawing.Size(242, 32);
            this.verticalIncrementLabel.TabIndex = 31;
            this.verticalIncrementLabel.Text = "Vertical Increment";
            // 
            // horizontalCurrentValueLabel
            // 
            this.horizontalCurrentValueLabel.AutoSize = true;
            this.horizontalCurrentValueLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.horizontalCurrentValueLabel.Location = new System.Drawing.Point(104, 305);
            this.horizontalCurrentValueLabel.Name = "horizontalCurrentValueLabel";
            this.horizontalCurrentValueLabel.Size = new System.Drawing.Size(139, 25);
            this.horizontalCurrentValueLabel.TabIndex = 33;
            this.horizontalCurrentValueLabel.Text = "Current Value:";
            // 
            // verticalCurrentValueLabel
            // 
            this.verticalCurrentValueLabel.AutoSize = true;
            this.verticalCurrentValueLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.verticalCurrentValueLabel.Location = new System.Drawing.Point(794, 305);
            this.verticalCurrentValueLabel.Name = "verticalCurrentValueLabel";
            this.verticalCurrentValueLabel.Size = new System.Drawing.Size(139, 25);
            this.verticalCurrentValueLabel.TabIndex = 34;
            this.verticalCurrentValueLabel.Text = "Current Value:";
            // 
            // horizontalIncrementValueBox
            // 
            this.horizontalIncrementValueBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.horizontalIncrementValueBox.Location = new System.Drawing.Point(248, 302);
            this.horizontalIncrementValueBox.Name = "horizontalIncrementValueBox";
            this.horizontalIncrementValueBox.Size = new System.Drawing.Size(100, 30);
            this.horizontalIncrementValueBox.TabIndex = 4;
            this.horizontalIncrementValueBox.Text = "5";
            this.horizontalIncrementValueBox.TextChanged += new System.EventHandler(this.horizontalIncrementValueBox_TextChanged);
            // 
            // verticalIncrementValueBox
            // 
            this.verticalIncrementValueBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.verticalIncrementValueBox.Location = new System.Drawing.Point(938, 302);
            this.verticalIncrementValueBox.Name = "verticalIncrementValueBox";
            this.verticalIncrementValueBox.Size = new System.Drawing.Size(100, 30);
            this.verticalIncrementValueBox.TabIndex = 5;
            this.verticalIncrementValueBox.Text = "5";
            this.verticalIncrementValueBox.TextChanged += new System.EventHandler(this.verticalIncrementValueBox_TextChanged);
            // 
            // horizontalValidationLabel
            // 
            this.horizontalValidationLabel.AutoSize = true;
            this.horizontalValidationLabel.ForeColor = System.Drawing.Color.Red;
            this.horizontalValidationLabel.Location = new System.Drawing.Point(104, 340);
            this.horizontalValidationLabel.Name = "horizontalValidationLabel";
            this.horizontalValidationLabel.Size = new System.Drawing.Size(0, 20);
            this.horizontalValidationLabel.TabIndex = 9;
            // 
            // filePrefixInfo
            // 
            this.filePrefixInfo.Image = global::p36_photo_table.Properties.Resources.info;
            this.filePrefixInfo.Location = new System.Drawing.Point(106, 525);
            this.filePrefixInfo.Name = "filePrefixInfo";
            this.filePrefixInfo.Size = new System.Drawing.Size(32, 42);
            this.filePrefixInfo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.filePrefixInfo.TabIndex = 33;
            this.filePrefixInfo.TabStop = false;
            this.tooltip.SetToolTip(this.filePrefixInfo, "This prefix will be appended to the beginning \r\nof the name of all images saved.");
            // 
            // fileLocationInfo
            // 
            this.fileLocationInfo.Image = global::p36_photo_table.Properties.Resources.info;
            this.fileLocationInfo.Location = new System.Drawing.Point(106, 665);
            this.fileLocationInfo.Name = "fileLocationInfo";
            this.fileLocationInfo.Size = new System.Drawing.Size(32, 42);
            this.fileLocationInfo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.fileLocationInfo.TabIndex = 30;
            this.fileLocationInfo.TabStop = false;
            this.tooltip.SetToolTip(this.fileLocationInfo, "This is the location of where the images\r\nwill be saved after completion.");
            // 
            // verticalIncrementInfo
            // 
            this.verticalIncrementInfo.Image = global::p36_photo_table.Properties.Resources.info;
            this.verticalIncrementInfo.Location = new System.Drawing.Point(798, 162);
            this.verticalIncrementInfo.Name = "verticalIncrementInfo";
            this.verticalIncrementInfo.Size = new System.Drawing.Size(32, 42);
            this.verticalIncrementInfo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.verticalIncrementInfo.TabIndex = 11;
            this.verticalIncrementInfo.TabStop = false;
            this.tooltip.SetToolTip(this.verticalIncrementInfo, "The increment of the angular displacement \r\nrelative to the horizontal plane.");
            // 
            // horizontalIncrementInfo
            // 
            this.horizontalIncrementInfo.Image = global::p36_photo_table.Properties.Resources.info;
            this.horizontalIncrementInfo.Location = new System.Drawing.Point(106, 162);
            this.horizontalIncrementInfo.Name = "horizontalIncrementInfo";
            this.horizontalIncrementInfo.Size = new System.Drawing.Size(32, 42);
            this.horizontalIncrementInfo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.horizontalIncrementInfo.TabIndex = 10;
            this.horizontalIncrementInfo.TabStop = false;
            this.tooltip.SetToolTip(this.horizontalIncrementInfo, "The increment at which the turntable\r\nwill rotate for each image.");
            // 
            // verticalValidationLabel
            // 
            this.verticalValidationLabel.AutoSize = true;
            this.verticalValidationLabel.ForeColor = System.Drawing.Color.Red;
            this.verticalValidationLabel.Location = new System.Drawing.Point(794, 340);
            this.verticalValidationLabel.Name = "verticalValidationLabel";
            this.verticalValidationLabel.Size = new System.Drawing.Size(0, 20);
            this.verticalValidationLabel.TabIndex = 12;
            // 
            // parthWidthLabel
            // 
            this.parthWidthLabel.AutoSize = true;
            this.parthWidthLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.parthWidthLabel.Location = new System.Drawing.Point(212, 377);
            this.parthWidthLabel.Name = "parthWidthLabel";
            this.parthWidthLabel.Size = new System.Drawing.Size(208, 32);
            this.parthWidthLabel.TabIndex = 35;
            this.parthWidthLabel.Text = "Part Width (cm)";
            // 
            // partLengthLabel
            // 
            this.partLengthLabel.AutoSize = true;
            this.partLengthLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.partLengthLabel.Location = new System.Drawing.Point(554, 377);
            this.partLengthLabel.Name = "partLengthLabel";
            this.partLengthLabel.Size = new System.Drawing.Size(223, 32);
            this.partLengthLabel.TabIndex = 36;
            this.partLengthLabel.Text = "Part Length (cm)";
            // 
            // partHeightLabel
            // 
            this.partHeightLabel.AutoSize = true;
            this.partHeightLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.partHeightLabel.Location = new System.Drawing.Point(908, 377);
            this.partHeightLabel.Name = "partHeightLabel";
            this.partHeightLabel.Size = new System.Drawing.Size(218, 32);
            this.partHeightLabel.TabIndex = 37;
            this.partHeightLabel.Text = "Part Height (cm)";
            // 
            // partWidthInput
            // 
            this.partWidthInput.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.partWidthInput.Location = new System.Drawing.Point(240, 422);
            this.partWidthInput.Name = "partWidthInput";
            this.partWidthInput.Size = new System.Drawing.Size(148, 30);
            this.partWidthInput.TabIndex = 6;
            this.partWidthInput.TextChanged += new System.EventHandler(this.partWidthInput_TextChanged);
            // 
            // partLengthInput
            // 
            this.partLengthInput.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.partLengthInput.Location = new System.Drawing.Point(590, 422);
            this.partLengthInput.Name = "partLengthInput";
            this.partLengthInput.Size = new System.Drawing.Size(148, 30);
            this.partLengthInput.TabIndex = 7;
            this.partLengthInput.TextChanged += new System.EventHandler(this.partLengthInput_TextChanged);
            // 
            // partHeightInput
            // 
            this.partHeightInput.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.partHeightInput.Location = new System.Drawing.Point(938, 422);
            this.partHeightInput.Name = "partHeightInput";
            this.partHeightInput.Size = new System.Drawing.Size(148, 30);
            this.partHeightInput.TabIndex = 8;
            this.partHeightInput.TextChanged += new System.EventHandler(this.partHeightInput_TextChanged);
            // 
            // startButton
            // 
            this.startButton.BackColor = System.Drawing.Color.Gray;
            this.startButton.Enabled = false;
            this.startButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.startButton.Location = new System.Drawing.Point(1100, 680);
            this.startButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(220, 95);
            this.startButton.TabIndex = 11;
            this.startButton.Text = "Start";
            this.startButton.UseVisualStyleBackColor = false;
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // folderBrowserDialog
            // 
            this.folderBrowserDialog.RootFolder = System.Environment.SpecialFolder.MyComputer;
            // 
            // presetsLabel
            // 
            this.presetsLabel.AutoSize = true;
            this.presetsLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.presetsLabel.Location = new System.Drawing.Point(105, 48);
            this.presetsLabel.Name = "presetsLabel";
            this.presetsLabel.Size = new System.Drawing.Size(118, 32);
            this.presetsLabel.TabIndex = 21;
            this.presetsLabel.Text = "Presets:";
            // 
            // preset1515
            // 
            this.preset1515.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.preset1515.Location = new System.Drawing.Point(111, 89);
            this.preset1515.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.preset1515.Name = "preset1515";
            this.preset1515.Size = new System.Drawing.Size(240, 35);
            this.preset1515.TabIndex = 0;
            this.preset1515.Text = "15-15";
            this.preset1515.UseVisualStyleBackColor = true;
            this.preset1515.Click += new System.EventHandler(this.preset1515_Click);
            // 
            // preset4545
            // 
            this.preset4545.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.preset4545.Location = new System.Drawing.Point(360, 89);
            this.preset4545.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.preset4545.Name = "preset4545";
            this.preset4545.Size = new System.Drawing.Size(240, 35);
            this.preset4545.TabIndex = 1;
            this.preset4545.Text = "45-45";
            this.preset4545.UseVisualStyleBackColor = true;
            this.preset4545.Click += new System.EventHandler(this.preset4545_Click);
            // 
            // partWidthValidationLabel
            // 
            this.partWidthValidationLabel.AutoSize = true;
            this.partWidthValidationLabel.ForeColor = System.Drawing.Color.Red;
            this.partWidthValidationLabel.Location = new System.Drawing.Point(213, 472);
            this.partWidthValidationLabel.Name = "partWidthValidationLabel";
            this.partWidthValidationLabel.Size = new System.Drawing.Size(0, 20);
            this.partWidthValidationLabel.TabIndex = 24;
            // 
            // partLengthValidationLabel
            // 
            this.partLengthValidationLabel.AutoSize = true;
            this.partLengthValidationLabel.ForeColor = System.Drawing.Color.Red;
            this.partLengthValidationLabel.Location = new System.Drawing.Point(555, 472);
            this.partLengthValidationLabel.Name = "partLengthValidationLabel";
            this.partLengthValidationLabel.Size = new System.Drawing.Size(0, 20);
            this.partLengthValidationLabel.TabIndex = 25;
            // 
            // partHeightValidationLabel
            // 
            this.partHeightValidationLabel.AutoSize = true;
            this.partHeightValidationLabel.ForeColor = System.Drawing.Color.Red;
            this.partHeightValidationLabel.Location = new System.Drawing.Point(909, 472);
            this.partHeightValidationLabel.Name = "partHeightValidationLabel";
            this.partHeightValidationLabel.Size = new System.Drawing.Size(0, 20);
            this.partHeightValidationLabel.TabIndex = 26;
            // 
            // fileLocationButton
            // 
            this.fileLocationButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.fileLocationButton.Location = new System.Drawing.Point(106, 709);
            this.fileLocationButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.fileLocationButton.Name = "fileLocationButton";
            this.fileLocationButton.Size = new System.Drawing.Size(82, 35);
            this.fileLocationButton.TabIndex = 10;
            this.fileLocationButton.Text = "Select";
            this.fileLocationButton.UseVisualStyleBackColor = true;
            this.fileLocationButton.Click += new System.EventHandler(this.fileLocationButton_Click);
            // 
            // fileLocationLabel
            // 
            this.fileLocationLabel.AutoSize = true;
            this.fileLocationLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.fileLocationLabel.Location = new System.Drawing.Point(144, 668);
            this.fileLocationLabel.Name = "fileLocationLabel";
            this.fileLocationLabel.Size = new System.Drawing.Size(177, 32);
            this.fileLocationLabel.TabIndex = 39;
            this.fileLocationLabel.Text = "File Location";
            // 
            // fileLocationInput
            // 
            this.fileLocationInput.AutoSize = true;
            this.fileLocationInput.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.fileLocationInput.Location = new System.Drawing.Point(106, 749);
            this.fileLocationInput.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.fileLocationInput.Name = "fileLocationInput";
            this.fileLocationInput.Size = new System.Drawing.Size(0, 25);
            this.fileLocationInput.TabIndex = 31;
            // 
            // filePrefixLabel
            // 
            this.filePrefixLabel.AutoSize = true;
            this.filePrefixLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.filePrefixLabel.Location = new System.Drawing.Point(144, 528);
            this.filePrefixLabel.Name = "filePrefixLabel";
            this.filePrefixLabel.Size = new System.Drawing.Size(141, 32);
            this.filePrefixLabel.TabIndex = 38;
            this.filePrefixLabel.Text = "File Prefix";
            // 
            // filePrefixInput
            // 
            this.filePrefixInput.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.filePrefixInput.Location = new System.Drawing.Point(106, 572);
            this.filePrefixInput.Name = "filePrefixInput";
            this.filePrefixInput.Size = new System.Drawing.Size(282, 30);
            this.filePrefixInput.TabIndex = 9;
            this.filePrefixInput.TextChanged += new System.EventHandler(this.filePrefixInput_TextChanged);
            // 
            // filePrefixValidationLabel
            // 
            this.filePrefixValidationLabel.AutoSize = true;
            this.filePrefixValidationLabel.ForeColor = System.Drawing.Color.Red;
            this.filePrefixValidationLabel.Location = new System.Drawing.Point(102, 620);
            this.filePrefixValidationLabel.Name = "filePrefixValidationLabel";
            this.filePrefixValidationLabel.Size = new System.Drawing.Size(0, 20);
            this.filePrefixValidationLabel.TabIndex = 35;
            // 
            // verticalIncrementImage
            // 
            this.verticalIncrementImage.Image = global::p36_photo_table.Properties.Resources.Vertical_Increment;
            this.verticalIncrementImage.Location = new System.Drawing.Point(1210, 162);
            this.verticalIncrementImage.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.verticalIncrementImage.Name = "verticalIncrementImage";
            this.verticalIncrementImage.Size = new System.Drawing.Size(110, 115);
            this.verticalIncrementImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.verticalIncrementImage.TabIndex = 37;
            this.verticalIncrementImage.TabStop = false;
            // 
            // horizontalIncrementImage
            // 
            this.horizontalIncrementImage.Image = global::p36_photo_table.Properties.Resources.Horizontal_Increment;
            this.horizontalIncrementImage.Location = new System.Drawing.Point(507, 162);
            this.horizontalIncrementImage.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.horizontalIncrementImage.Name = "horizontalIncrementImage";
            this.horizontalIncrementImage.Size = new System.Drawing.Size(110, 115);
            this.horizontalIncrementImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.horizontalIncrementImage.TabIndex = 36;
            this.horizontalIncrementImage.TabStop = false;
            // 
            // MainPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(1425, 871);
            this.Controls.Add(this.verticalIncrementImage);
            this.Controls.Add(this.horizontalIncrementImage);
            this.Controls.Add(this.filePrefixValidationLabel);
            this.Controls.Add(this.filePrefixInput);
            this.Controls.Add(this.filePrefixInfo);
            this.Controls.Add(this.filePrefixLabel);
            this.Controls.Add(this.fileLocationInput);
            this.Controls.Add(this.fileLocationInfo);
            this.Controls.Add(this.fileLocationLabel);
            this.Controls.Add(this.fileLocationButton);
            this.Controls.Add(this.partHeightValidationLabel);
            this.Controls.Add(this.partLengthValidationLabel);
            this.Controls.Add(this.partWidthValidationLabel);
            this.Controls.Add(this.preset4545);
            this.Controls.Add(this.preset1515);
            this.Controls.Add(this.presetsLabel);
            this.Controls.Add(this.startButton);
            this.Controls.Add(this.partHeightInput);
            this.Controls.Add(this.partLengthInput);
            this.Controls.Add(this.partWidthInput);
            this.Controls.Add(this.partHeightLabel);
            this.Controls.Add(this.partLengthLabel);
            this.Controls.Add(this.parthWidthLabel);
            this.Controls.Add(this.verticalValidationLabel);
            this.Controls.Add(this.verticalIncrementInfo);
            this.Controls.Add(this.horizontalIncrementInfo);
            this.Controls.Add(this.horizontalValidationLabel);
            this.Controls.Add(this.verticalIncrementValueBox);
            this.Controls.Add(this.horizontalIncrementValueBox);
            this.Controls.Add(this.verticalCurrentValueLabel);
            this.Controls.Add(this.horizontalCurrentValueLabel);
            this.Controls.Add(this.verticalIncrementLabel);
            this.Controls.Add(this.horizontalIncrementLabel);
            this.Controls.Add(this.verticalIncrement);
            this.Controls.Add(this.horizontalIncrement);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainPage";
            this.Text = "Photo Table";
            this.Load += new System.EventHandler(this.MainPage_Load);
            ((System.ComponentModel.ISupportInitialize)(this.horizontalIncrement)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.verticalIncrement)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.filePrefixInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fileLocationInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.verticalIncrementInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.horizontalIncrementInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.verticalIncrementImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.horizontalIncrementImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TrackBar horizontalIncrement;
        private System.Windows.Forms.TrackBar verticalIncrement;
        private System.Windows.Forms.Label horizontalIncrementLabel;
        private System.Windows.Forms.Label verticalIncrementLabel;
        private System.Windows.Forms.Label horizontalCurrentValueLabel;
        private System.Windows.Forms.Label verticalCurrentValueLabel;
        private System.Windows.Forms.TextBox horizontalIncrementValueBox;
        private System.Windows.Forms.TextBox verticalIncrementValueBox;
        private System.Windows.Forms.Label horizontalValidationLabel;
        private System.Windows.Forms.PictureBox horizontalIncrementInfo;
        private System.Windows.Forms.PictureBox verticalIncrementInfo;
        private System.Windows.Forms.ToolTip tooltip;
        private System.Windows.Forms.Label verticalValidationLabel;
        private System.Windows.Forms.Label parthWidthLabel;
        private System.Windows.Forms.Label partLengthLabel;
        private System.Windows.Forms.Label partHeightLabel;
        private System.Windows.Forms.TextBox partWidthInput;
        private System.Windows.Forms.TextBox partLengthInput;
        private System.Windows.Forms.TextBox partHeightInput;
        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.Label presetsLabel;
        private System.Windows.Forms.Button preset1515;
        private System.Windows.Forms.Button preset4545;
        private System.Windows.Forms.Label partWidthValidationLabel;
        private System.Windows.Forms.Label partLengthValidationLabel;
        private System.Windows.Forms.Label partHeightValidationLabel;
        private System.Windows.Forms.Button fileLocationButton;
        private System.Windows.Forms.PictureBox fileLocationInfo;
        private System.Windows.Forms.Label fileLocationLabel;
        private System.Windows.Forms.Label fileLocationInput;
        private System.Windows.Forms.PictureBox filePrefixInfo;
        private System.Windows.Forms.Label filePrefixLabel;
        private System.Windows.Forms.TextBox filePrefixInput;
        private System.Windows.Forms.Label filePrefixValidationLabel;
        private System.Windows.Forms.PictureBox horizontalIncrementImage;
        private System.Windows.Forms.PictureBox verticalIncrementImage;
    }
}

