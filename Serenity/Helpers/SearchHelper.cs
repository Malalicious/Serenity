using System.Drawing;
using System.Drawing.Imaging;
using System.Threading.Tasks;

namespace Serenity
{
    class SearchHelper
    {
        public static Point SearchColor(ref Bitmap ScreenCapture, Color SearchColor, int Tolerance = 0)
        {
            unsafe
            {
                Point Output = Point.Empty;
                BitmapData bitmapData = ScreenCapture.LockBits(new Rectangle(0, 0, ScreenCapture.Width, ScreenCapture.Height), ImageLockMode.ReadWrite, ScreenCapture.PixelFormat);

                int bytesPerPixel = Image.GetPixelFormatSize(ScreenCapture.PixelFormat) / 8;
                int heightInPixels = bitmapData.Height;
                int widthInPixels = bitmapData.Width;
                int widthInBytes = bitmapData.Width * bytesPerPixel;
                byte* PtrFirstPixel = (byte*)bitmapData.Scan0;

                Parallel.For(0, heightInPixels, (y, loopState) =>
                {
                    int searchX = 0;
                    byte* currentLine = PtrFirstPixel + (y * bitmapData.Stride);
                    for (int x = 0; x < widthInBytes; x = x + bytesPerPixel)
                    {
                        int oldBlue = currentLine[x];
                        int oldGreen = currentLine[x + 1];
                        int oldRed = currentLine[x + 2];

                        int diff;
                        int sum = 0;

                        diff = oldRed - SearchColor.R;
                        sum += (1 + diff * diff) * oldRed / 256;

                        diff = oldGreen - SearchColor.G;
                        sum += (1 + diff * diff) * oldGreen / 256;

                        diff = oldBlue - SearchColor.B;
                        sum += (1 + diff * diff) * oldBlue / 256;

                        if (sum <= Tolerance * Tolerance * 4)
                        {
                            Output = new Point(searchX, y);
                            loopState.Break();
                            break;
                        }

                        currentLine[x] = (byte)oldBlue;
                        currentLine[x + 1] = (byte)oldGreen;
                        currentLine[x + 2] = (byte)oldRed;

                        searchX++;
                    }
                });

                ScreenCapture.UnlockBits(bitmapData);

                return Output;
            }
        }
    }
}
