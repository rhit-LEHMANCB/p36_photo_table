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
            this.controller = new TableController(horizontalIncrementValue, verticalIncrementValue, partHeight, partLength, partWidth, fileLocation, filePrefix);
            // TODO: setup status labels with inital positions
        }

        private void InProgressPage_Shown(object sender, EventArgs e)
        {
            this.controller.Start(statusBar, statusNumberLabel, statusLabel);
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            this.controller.CloseSession();

            base.OnFormClosing(e);
        }
    }
}
