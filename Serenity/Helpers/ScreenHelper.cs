using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace Serenity.Helpers
{
    internal class ScreenHelper
    {
        [DllImport("gdi32.dll")]
        private static extern bool BitBlt(IntPtr hdcDest, int nXDest, int nYDest, int nWidth, int nHeight, IntPtr hdcSrc, int nXSrc, int nYSrc, int dwRop);

        /// <summary>
        /// Returns a screen capture of the given Fov.
        /// </summary>
        /// <param name="fov"></param>
        /// <returns></returns>
        public static Bitmap GetScreenCapture(Rectangle fov)
        {
            // Define the size of the screencopy.
            var screenCopy = new Bitmap(fov.Width, fov.Height, PixelFormat.Format24bppRgb);

            using (var gdest = Graphics.FromImage(screenCopy))

            using (var gsrc = Graphics.FromHwnd(IntPtr.Zero))
            {
                var hSrcDc = gsrc.GetHdc();
                var hDc = gdest.GetHdc();
                var retval = BitBlt(hDc, 0, 0, fov.Width, fov.Height, hSrcDc, fov.X, fov.Y, (int)CopyPixelOperation.SourceCopy);

                gdest.ReleaseHdc();
                gsrc.ReleaseHdc();
            }

            return screenCopy;
        }

        /// <summary>
        /// Returns coordinates absolute to the screen.
        /// </summary>
        /// <param name="relative"></param>
        /// <param name="fov"></param>
        /// <returns></returns>
        public static Point GetAbsoluteCoordinates(Point relative, Rectangle fov)
        {
            return new Point { X = relative.X + fov.X, Y = relative.Y + fov.Y };
        }
    }
}
