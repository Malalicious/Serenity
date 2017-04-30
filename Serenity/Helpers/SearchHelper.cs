using System.Drawing;
using System.Drawing.Imaging;
using System.Threading.Tasks;

namespace Serenity.Helpers
{
    internal class SearchHelper
    {
        public static Point SearchColor(ref Bitmap screenCapture, Color searchColor, int tolerance = 0)
        {
            unsafe
            {
                var output = Point.Empty;
                var bitmapData = screenCapture.LockBits(new Rectangle(0, 0, screenCapture.Width, screenCapture.Height), ImageLockMode.ReadWrite, screenCapture.PixelFormat);

                var bytesPerPixel = Image.GetPixelFormatSize(screenCapture.PixelFormat) / 8;
                var heightInPixels = bitmapData.Height;
                var widthInPixels = bitmapData.Width;
                var widthInBytes = bitmapData.Width * bytesPerPixel;
                var ptrFirstPixel = (byte*)bitmapData.Scan0;

                Parallel.For(0, heightInPixels, (y, loopState) =>
                {
                    var searchX = 0;
                    var currentLine = ptrFirstPixel + (y * bitmapData.Stride);
                    for (var x = 0; x < widthInBytes; x = x + bytesPerPixel)
                    {
                        int oldBlue = currentLine[x];
                        int oldGreen = currentLine[x + 1];
                        int oldRed = currentLine[x + 2];

                        var sum = 0;

                        var diff = oldRed - searchColor.R;
                        sum += (1 + diff * diff) * oldRed / 256;

                        diff = oldGreen - searchColor.G;
                        sum += (1 + diff * diff) * oldGreen / 256;

                        diff = oldBlue - searchColor.B;
                        sum += (1 + diff * diff) * oldBlue / 256;

                        if (sum <= tolerance * tolerance * 4)
                        {
                            output = new Point(searchX, y);
                            loopState.Break();
                            break;
                        }

                        currentLine[x] = (byte)oldBlue;
                        currentLine[x + 1] = (byte)oldGreen;
                        currentLine[x + 2] = (byte)oldRed;

                        searchX++;
                    }
                });

                screenCapture.UnlockBits(bitmapData);

                return output;
            }
        }
    }
}
