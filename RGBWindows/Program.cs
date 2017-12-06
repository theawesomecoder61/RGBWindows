using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RGBWindows
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Form1 form1 = new Form1();
            About about = new About();

            using(NotifyIcon icon = new NotifyIcon())
            {
                ContextMenu cm = new ContextMenu();

                MenuItem openMI = new MenuItem("Open");
                openMI.Click += (object sender, EventArgs e) => {
                    if(!form1.Visible)
                    {
                        form1.Show();
                    }
                };
                cm.MenuItems.Add(openMI);

                MenuItem aboutMI = new MenuItem("About");
                aboutMI.Click += (object sender, EventArgs e) =>
                {
                    if(!about.Visible)
                    {
                        about.Show();
                    }
                };
                cm.MenuItems.Add(aboutMI);

                MenuItem quitMI = new MenuItem("Quit");
                quitMI.Click += (object sender, EventArgs e) =>
                {
                    Application.Exit();
                };
                cm.MenuItems.Add(quitMI);

                icon.Icon = Properties.Resources.icon;
                icon.Text = "RGB Windows";
                icon.ContextMenu = cm;
                icon.Visible = true;

                Application.Run();
            }
        }
    }
}