using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CameraControl;

namespace p36_photo_table
{
    public partial class InProgressPage: Form
    {
        private TableController controller;

        public InProgressPage(int horizontalIncrementValue, int verticalIncrementValue, float partHeight, float partLength, float partWidth, string fileLocation, string filePrefix)
        {
            InitializeComponent();
            try
            {
                this.controller = new TableController(horizontalIncrementValue, verticalIncrementValue, partHeight, partLength, partWidth, fileLocation, filePrefix);
            }
            catch (CameraNotFoundException)
            {
                MessageBox.Show("No camera found. Please make sure it is plugged in.");
                this.Close();
            }
            catch (ArduinoNotFoundException)
            {
                MessageBox.Show("No arduino found. Please make sure it is plugged in.");
                this.Close();
            }
        }

        private void InProgressPage_Shown(object sender, EventArgs e)
        {
            tableWorker.RunWorkerAsync();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            this.controller.CloseSession();

            base.OnFormClosing(e);
        }

        private void tableWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker bw = sender as BackgroundWorker;

            this.controller.Start(bw, statusBar, statusNumberLabel, statusLabel);

            if (bw.CancellationPending)
            {
                e.Cancel = true;
            }
        }

        private void tableWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                MessageBox.Show("Operation successfully cancelled");
                this.Close();
            }
            else if (e.Error != null)
            {
                MessageBox.Show("An error occured during operation");
                this.Close();
            }
            else
            {
                // close window and tell user it's done
                MessageBox.Show("Operation complete");
                this.Close();
            }
        }

        private void stopButton_Click(object sender, EventArgs e)
        {
            tableWorker.CancelAsync();
        }
    }
}
