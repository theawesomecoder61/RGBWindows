using System;
using System.Windows.Forms;

namespace RGBWindows
{
    public partial class Form1 : Form
    {
        private RGBThing rgbThing;

        public Form1()
        {
            InitializeComponent();

            rgbThing = new RGBThing();
            rgbThing.StartFade(true);

            trackBar1.Value = Properties.Settings.Default.interval;
            label1.Text = Properties.Settings.Default.interval + " ms";
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            rgbThing.StopFade();
            label1.Text = trackBar1.Value + " ms";
            rgbThing.SetInterval(trackBar1.Value);
            rgbThing.StartFade();
        }
    }
}
