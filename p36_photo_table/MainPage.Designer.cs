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
            this.horizontalIncrement = new System.Windows.Forms.TrackBar();
            this.verticalIncrement = new System.Windows.Forms.TrackBar();
            this.horizontalIncrementLabel = new System.Windows.Forms.Label();
            this.verticalIncrementLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.horizontalIncrement)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.verticalIncrement)).BeginInit();
            this.SuspendLayout();
            // 
            // horizontalIncrement
            // 
            this.horizontalIncrement.Location = new System.Drawing.Point(110, 102);
            this.horizontalIncrement.Maximum = 45;
            this.horizontalIncrement.Minimum = 5;
            this.horizontalIncrement.Name = "horizontalIncrement";
            this.horizontalIncrement.Size = new System.Drawing.Size(405, 69);
            this.horizontalIncrement.SmallChange = 5;
            this.horizontalIncrement.TabIndex = 1;
            this.horizontalIncrement.TickFrequency = 5;
            this.horizontalIncrement.Value = 5;
            // 
            // verticalIncrement
            // 
            this.verticalIncrement.Location = new System.Drawing.Point(801, 102);
            this.verticalIncrement.Maximum = 45;
            this.verticalIncrement.Minimum = 5;
            this.verticalIncrement.Name = "verticalIncrement";
            this.verticalIncrement.Size = new System.Drawing.Size(405, 69);
            this.verticalIncrement.SmallChange = 5;
            this.verticalIncrement.TabIndex = 2;
            this.verticalIncrement.TickFrequency = 5;
            this.verticalIncrement.Value = 5;
            // 
            // horizontalIncrementLabel
            // 
            this.horizontalIncrementLabel.AutoSize = true;
            this.horizontalIncrementLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.horizontalIncrementLabel.Location = new System.Drawing.Point(105, 59);
            this.horizontalIncrementLabel.Name = "horizontalIncrementLabel";
            this.horizontalIncrementLabel.Size = new System.Drawing.Size(233, 29);
            this.horizontalIncrementLabel.TabIndex = 3;
            this.horizontalIncrementLabel.Text = "Horizontal Increment";
            // 
            // verticalIncrementLabel
            // 
            this.verticalIncrementLabel.AutoSize = true;
            this.verticalIncrementLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.verticalIncrementLabel.Location = new System.Drawing.Point(796, 59);
            this.verticalIncrementLabel.Name = "verticalIncrementLabel";
            this.verticalIncrementLabel.Size = new System.Drawing.Size(205, 29);
            this.verticalIncrementLabel.TabIndex = 4;
            this.verticalIncrementLabel.Text = "Vertical Increment";
            // 
            // MainPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1347, 714);
            this.Controls.Add(this.verticalIncrementLabel);
            this.Controls.Add(this.horizontalIncrementLabel);
            this.Controls.Add(this.verticalIncrement);
            this.Controls.Add(this.horizontalIncrement);
            this.Name = "MainPage";
            this.Text = "Photo Table";
            this.Load += new System.EventHandler(this.MainPage_Load);
            ((System.ComponentModel.ISupportInitialize)(this.horizontalIncrement)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.verticalIncrement)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TrackBar horizontalIncrement;
        private System.Windows.Forms.TrackBar verticalIncrement;
        private System.Windows.Forms.Label horizontalIncrementLabel;
        private System.Windows.Forms.Label verticalIncrementLabel;
    }
}

