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
        private CameraController controller;
        private ArduinoController arduinoController;

        public InProgressPage(int horizontalIncrementValue, int verticalIncrementValue, float partHeight, float partLength, float partWidth, string fileLocation, string filePrefix, ArduinoController arduinoController)
        {
            InitializeComponent();
            this.controller = new CameraController(horizontalIncrementValue, verticalIncrementValue, partHeight, partLength, partWidth, fileLocation, filePrefix);
            this.arduinoController = arduinoController;
            bool foundCamera = controller.InitializeCamera();

            if (!foundCamera)
            {
                MessageBox.Show("No camera found.");
                this.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.controller.TakePicture();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            this.controller.CloseSession();

            base.OnFormClosing(e);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            arduinoController.SendCommand("1");
            string response = arduinoController.WaitForResponse();
            Console.WriteLine("Command completed: " + response);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            arduinoController.SendCommand("0");
            string response = arduinoController.WaitForResponse();
            Console.WriteLine("Command completed: " + response);
        }
    }
}
