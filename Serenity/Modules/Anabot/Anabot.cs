using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace Serenity
{
    class Anabot
    {
        /// <summary>
        /// Contains all FOVs.
        /// </summary>
        public List<Fov> Fovs { get; set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        public Anabot()
        {
            // Initialize Fovs.
            Fovs = new List<Fov>
            {
                new Fov { Resolution = new Point(1920, 1080), FieldOfView = new Rectangle(830, 430, 275, 130), RangeValues = new Point(0, 25), Tolerance = new Point(2, 2) },
                new Fov { Resolution = new Point(1280, 720), FieldOfView = new Rectangle(550, 300, 180, 110), RangeValues = new Point(0, 25), Tolerance = new Point(2, 2) }
            };

            // Set default settings.
            Settings.Anabot.AimKey = 0x05;
            Settings.Anabot.TargetColor = Color.FromArgb(202, 164, 63);

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
                if (Settings.Anabot.IsEnabled)
                {
                    if (MouseHelper.GetAsyncKeyState(Settings.Anabot.AimKey) < 0)
                    {
                        // Get the screen capture.
                        Bitmap ScreenCapture = ScreenHelper.GetScreenCapture(MyFov.FieldOfView);

                        // Search for a target.
                        Point Coordinates = SearchHelper.SearchColor(ref ScreenCapture, Settings.Anabot.TargetColor, 3);

                        // Only continue if a healthbar was found.
                        if (Coordinates.X != 0 || Coordinates.Y != 0)
                        {
                            Coordinates = ScreenHelper.GetAbsoluteCoordinates(Coordinates, MyFov.FieldOfView);

                            MouseHelper.Move(ref MyFov, Coordinates);
                        }

                        // Destroy the bitmap.
                        ScreenCapture.Dispose();
                        ScreenCapture = null;
                    }

                    Thread.Sleep(1);
                }
                else
                {
                    Thread.Sleep(1000);
                }
            }
        }
    }
}
