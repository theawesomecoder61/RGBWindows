using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using System.Timers;
using System.Windows.Forms;

namespace RGBWindows
{
    public class RGBThing
    {
        private System.Timers.Timer timer1;
        private uint originalColor;
        private bool originalBlend;
        private double hue = 0;

        [DllImport("dwmapi.dll", PreserveSig = false)]
        static extern void DwmGetColorizationColor(out uint ColorizationColor, [MarshalAs(UnmanagedType.Bool)]out bool ColorizationOpaqueBlend);

        [DllImport("dwmapi.dll", EntryPoint = "#131", PreserveSig = false)]
        private static extern void DwmSetColorizationParameters(ref DWM_COLORIZATION_PARAMS parameters, bool unknown);

        private struct DWM_COLORIZATION_PARAMS
        {
            public uint clrColor;
            public uint clrAfterGlow;
            public uint nIntensity;
            public uint clrAfterGlowBalance;
            public uint clrBlurBalance;
            public uint clrGlassReflectionIntensity;
            public bool fOpaque;
        }

        public void SetParameters(uint c)
        {
            DWM_COLORIZATION_PARAMS temp = new DWM_COLORIZATION_PARAMS();
            temp.clrColor = c;
            temp.clrAfterGlow = 0;
            temp.nIntensity = 80;
            temp.clrAfterGlowBalance = 0;
            temp.clrBlurBalance = 0;
            temp.clrGlassReflectionIntensity = 0;
            temp.fOpaque = false;

            /*
            DWM_COLORIZATION_PARAMS temp = new DWM_COLORIZATION_PARAMS();
            temp.clrColor = c;
            temp.clrAfterGlow = c;
            temp.nIntensity = 80;
            temp.clrAfterGlowBalance = 43;
            temp.clrBlurBalance = 49;
            temp.clrGlassReflectionIntensity = 50;
            temp.fOpaque = false;
             */

            DwmSetColorizationParameters(ref temp, originalBlend);
        }

        //

        public RGBThing()
        {
            timer1 = new System.Timers.Timer();
            timer1.Elapsed += Timer_Elapsed;

            Application.ApplicationExit += (object sender, EventArgs e) =>
            {
                StopFade(true);
            };
        }

        public void StartFade(bool getOriginalInfo = false)
        {
            // get the original color & blend settings
            if(getOriginalInfo)
            {
                uint color;
                bool blend;
                DwmGetColorizationColor(out color, out blend);

                originalColor = color;
                originalBlend = blend;
            }

            timer1.Start();
        }

        public void StopFade(bool dispose = false)
        {
            timer1.Stop();
            if(dispose)
            {
                timer1.Dispose();
            }

            SetParameters(originalColor);
        }

        public void SetInterval(int i)
        {
            Properties.Settings.Default.interval = i;
            Properties.Settings.Default.Save();
            timer1.Interval = Properties.Settings.Default.interval;
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            HSLColor hc = new HSLColor(hue, 100.00, 100.00);
            SetParameters(ColorToUInt(hc.ToColor()));

            hue++;
            if(hue > 360)
            {
                hue = 0;
            }
        }

        private uint ColorToUInt(Color c)
        {
            return (uint)((c.A << 24) | (c.R << 16) | (c.G << 8) | (c.B << 0));
        }
    }
}