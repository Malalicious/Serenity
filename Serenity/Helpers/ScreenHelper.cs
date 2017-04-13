using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace Serenity
{
    class ScreenHelper
    {
        [DllImport("gdi32.dll")]
        static extern bool BitBlt(IntPtr hdcDest, int nXDest, int nYDest, int nWidth, int nHeight, IntPtr hdcSrc, int nXSrc, int nYSrc, int dwRop);

        /// <summary>
        /// Returns a screen capture of the given Fov.
        /// </summary>
        /// <param name="Fov"></param>
        /// <returns></returns>
        public static Bitmap GetScreenCapture(Rectangle Fov)
        {
            // Define the size of the screencopy.
            Bitmap ScreenCopy = new Bitmap(Fov.Width, Fov.Height, PixelFormat.Format24bppRgb);

            using (Graphics gdest = Graphics.FromImage(ScreenCopy))

            using (Graphics gsrc = Graphics.FromHwnd(IntPtr.Zero))
            {
                IntPtr hSrcDC = gsrc.GetHdc();
                IntPtr hDC = gdest.GetHdc();
                bool retval = BitBlt(hDC, 0, 0, Fov.Width, Fov.Height, hSrcDC, Fov.X, Fov.Y, (int)CopyPixelOperation.SourceCopy);

                gdest.ReleaseHdc();
                gsrc.ReleaseHdc();
            }

            return ScreenCopy;
        }

        /// <summary>
        /// Returns coordinates absolute to the screen.
        /// </summary>
        /// <param name="Relative"></param>
        /// <param name="Fov"></param>
        /// <returns></returns>
        public static Point GetAbsoluteCoordinates(Point Relative, Rectangle Fov)
        {
            return new Point { X = Relative.X + Fov.X, Y = Relative.Y + Fov.Y };
        }
    }
}
