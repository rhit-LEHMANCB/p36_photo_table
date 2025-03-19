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
        public InProgressPage()
        {
            InitializeComponent();
            this.controller = new CameraController();
            bool foundCamera = controller.InitializeCamera();

            if (!foundCamera)
            {
                MessageBox.Show("No camera found.");
                this.Close();
            }
        }
    }
}
