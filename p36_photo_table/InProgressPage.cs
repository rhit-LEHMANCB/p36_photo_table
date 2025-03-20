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

        public InProgressPage(int horizontalIncrementValue)
        {
            
        }

        public InProgressPage(int horizontalIncrementValue, int verticalIncrementValue, float partHeight, float partLength, float partWidth, string fileLocation, string filePrefix) : this(horizontalIncrementValue)
        {
            InitializeComponent();
            this.controller = new CameraController(horizontalIncrementValue, verticalIncrementValue, partHeight, partLength, partWidth, fileLocation, filePrefix);
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
    }
}
