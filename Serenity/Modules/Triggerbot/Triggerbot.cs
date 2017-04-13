using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace Serenity
{
    class Triggerbot
    {
        /// <summary>
        /// Contains all FOVs.
        /// </summary>
        public List<Fov> Fovs { get; set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        public Triggerbot()
        {
            // Initialize Fovs.
            Fovs = new List<Fov>
            {
                new Fov { Resolution = new Point(1920, 1080), FieldOfView = new Rectangle(960, 400, 1, 165) }
            };

            // Set default settings.
            Settings.Triggerbot.AimKey = 0xA4;
            Settings.Triggerbot.TargetColor = Color.FromArgb(254, 0, 0);

            // Run the aimbot.
            new Thread(new ThreadStart(Run)).Start();
        }

        /// <summary>
        /// Main trigger thread.
        /// </summary>
        public void Run()
        {
            // Retrieve the Fov.
            Fov MyFov = Fovs.First(x => x.Resolution == new Point(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height));

            while (true)
            {
                if (MouseHelper.GetAsyncKeyState(Settings.Triggerbot.AimKey) < 0)
                {
                    // Get the screen capture.
                    Bitmap ScreenCapture = ScreenHelper.GetScreenCapture(MyFov.FieldOfView);

                    // Search for a target.
                    Point Coordinates = SearchHelper.SearchColor(ref ScreenCapture, Settings.Triggerbot.TargetColor, 100);

                    if (Coordinates.X != 0 || Coordinates.Y != 0)
                    {
                        MouseHelper.Click();
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
