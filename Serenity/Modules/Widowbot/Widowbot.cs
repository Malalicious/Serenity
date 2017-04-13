using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace Serenity
{
    class Widowbot
    {
        /// <summary>
        /// Contains all FOVs.
        /// </summary>
        public List<Fov> Fovs { get; set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        public Widowbot()
        {
            // Initialize Fovs.
            Fovs = new List<Fov>
            {
                new Fov { Resolution = new Point(1920, 1080), FieldOfView = new Rectangle(880, 510, 160, 50), RangeValues = new Point(0, 28), Tolerance = new Point(2, 2) },
                new Fov { Resolution = new Point(1280, 720), FieldOfView = new Rectangle(580, 335, 120, 40), RangeValues = new Point(0, 18), Tolerance = new Point(2, 2) }
            };

            // Set default settings.
            Settings.Widowbot.AimKey = 0xA4;
            Settings.Widowbot.TargetColor = Color.FromArgb(215, 40, 35);

            // Run the aimbot.
            new Thread(new ThreadStart(Run)).Start();
        }

        /// <summary>
        /// Main aimbot thread.
        /// </summary>
        public void Run()
        {
            // Retrieve the Fov.
            Fov MyFov = Fovs.First(x => x.Resolution == new Point(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height));

            // Run the main routine.
            while (true)
            {
                if (MouseHelper.GetAsyncKeyState(Settings.Widowbot.AimKey) < 0)
                {
                    // Get the screen capture.
                    Bitmap ScreenCapture = ScreenHelper.GetScreenCapture(MyFov.FieldOfView);

                    // Search for a target.
                    Point Coordinates = SearchHelper.SearchColor(ref ScreenCapture, Settings.Widowbot.TargetColor, 12);

                    // Only continue if a healthbar was found.
                    if (Coordinates.X != 0 || Coordinates.Y != 0)
                    {
                        Coordinates = ScreenHelper.GetAbsoluteCoordinates(Coordinates, MyFov.FieldOfView);

                        MouseHelper.Move(ref MyFov, Coordinates, true);
                    }

                    // Destroy the bitmap.
                    ScreenCapture.Dispose();
                    ScreenCapture = null;
                }

                Thread.Sleep(1);
            }
        }
    }
}
