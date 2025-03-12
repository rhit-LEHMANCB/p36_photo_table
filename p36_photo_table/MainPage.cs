using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace p36_photo_table
{
    public partial class MainPage: Form
    {
        public MainPage()
        {
            InitializeComponent();
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
                label.Text = i.ToString();
                label.Location = new Point(x + (i - 3) * 6, y + 40);
                label.AutoSize = true;
                this.Controls.Add(label);
                label.BringToFront();

            }
        }

        private void horizontalIncrement_MouseUp(object sender, MouseEventArgs e)
        {
            int tickFrequency = horizontalIncrement.TickFrequency;
            // Calculate the nearest tick by rounding the current value divided by tick frequency.
            int snappedValue = (int)Math.Round((double)horizontalIncrement.Value / tickFrequency) * tickFrequency;
            horizontalIncrement.Value = snappedValue;
            //horizontalIncrementValueLabel.Text = snappedValue.ToString();
        }
    }
}
