namespace p36_photo_table
{
    partial class InProgressPage
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InProgressPage));
            this.inProgressLabel = new System.Windows.Forms.Label();
            this.statusBar = new System.Windows.Forms.ProgressBar();
            this.stopButton = new System.Windows.Forms.Button();
            this.statusStaticLabel = new System.Windows.Forms.Label();
            this.statusLabel = new System.Windows.Forms.Label();
            this.statusNumberLabel = new System.Windows.Forms.Label();
            this.tableWorker = new System.ComponentModel.BackgroundWorker();
            this.SuspendLayout();
            // 
            // inProgressLabel
            // 
            this.inProgressLabel.AutoSize = true;
            this.inProgressLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.inProgressLabel.Location = new System.Drawing.Point(494, 214);
            this.inProgressLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.inProgressLabel.Name = "inProgressLabel";
            this.inProgressLabel.Size = new System.Drawing.Size(181, 32);
            this.inProgressLabel.TabIndex = 0;
            this.inProgressLabel.Text = "In Progress...";
            // 
            // statusBar
            // 
            this.statusBar.Location = new System.Drawing.Point(322, 275);
            this.statusBar.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.statusBar.Name = "statusBar";
            this.statusBar.Size = new System.Drawing.Size(534, 35);
            this.statusBar.TabIndex = 1;
            // 
            // stopButton
            // 
            this.stopButton.BackColor = System.Drawing.Color.Red;
            this.stopButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.stopButton.Location = new System.Drawing.Point(922, 554);
            this.stopButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.stopButton.Name = "stopButton";
            this.stopButton.Size = new System.Drawing.Size(220, 95);
            this.stopButton.TabIndex = 20;
            this.stopButton.Text = "Stop";
            this.stopButton.UseVisualStyleBackColor = false;
            this.stopButton.Click += new System.EventHandler(this.stopButton_Click);
            // 
            // statusStaticLabel
            // 
            this.statusStaticLabel.AutoSize = true;
            this.statusStaticLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.statusStaticLabel.Location = new System.Drawing.Point(318, 337);
            this.statusStaticLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.statusStaticLabel.Name = "statusStaticLabel";
            this.statusStaticLabel.Size = new System.Drawing.Size(74, 25);
            this.statusStaticLabel.TabIndex = 21;
            this.statusStaticLabel.Text = "Status:";
            // 
            // statusLabel
            // 
            this.statusLabel.AutoSize = true;
            this.statusLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.statusLabel.Location = new System.Drawing.Point(318, 363);
            this.statusLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(527, 25);
            this.statusLabel.TabIndex = 22;
            this.statusLabel.Text = "Taking picture with horizontal angle 15 and vertical angle 15";
            // 
            // statusNumberLabel
            // 
            this.statusNumberLabel.AutoSize = true;
            this.statusNumberLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.statusNumberLabel.Location = new System.Drawing.Point(318, 405);
            this.statusNumberLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.statusNumberLabel.Name = "statusNumberLabel";
            this.statusNumberLabel.Size = new System.Drawing.Size(116, 25);
            this.statusNumberLabel.TabIndex = 23;
            this.statusNumberLabel.Text = "Picture 1/36";
            // 
            // tableWorker
            // 
            this.tableWorker.WorkerSupportsCancellation = true;
            this.tableWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.tableWorker_DoWork);
            this.tableWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.tableWorker_RunWorkerCompleted);
            // 
            // InProgressPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 692);
            this.Controls.Add(this.statusNumberLabel);
            this.Controls.Add(this.statusLabel);
            this.Controls.Add(this.statusStaticLabel);
            this.Controls.Add(this.stopButton);
            this.Controls.Add(this.statusBar);
            this.Controls.Add(this.inProgressLabel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "InProgressPage";
            this.Shown += new System.EventHandler(this.InProgressPage_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label inProgressLabel;
        private System.Windows.Forms.ProgressBar statusBar;
        private System.Windows.Forms.Button stopButton;
        private System.Windows.Forms.Label statusStaticLabel;
        private System.Windows.Forms.Label statusLabel;
        private System.Windows.Forms.Label statusNumberLabel;
        private System.ComponentModel.BackgroundWorker tableWorker;
    }
}