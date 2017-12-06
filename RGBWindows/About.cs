using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace RGBWindows
{
    public partial class About : Form
    {
        public About()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Process.Start("https://github.com/theawesomecoder61/RGBWindows");
        }
    }
}