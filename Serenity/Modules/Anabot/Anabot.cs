using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using Serenity.Helpers;
using Serenity.Objects;

namespace Serenity.Modules.Anabot
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
            var myFov = Fovs.First(x => x.Resolution == new Point(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height));

            // Run the main routine.
            while (true)
            {
                if (Settings.Anabot.IsEnabled)
                {
                    if (MouseHelper.GetAsyncKeyState(Settings.Anabot.AimKey) < 0)
                    {
                        // Get the screen capture.
                        var screenCapture = ScreenHelper.GetScreenCapture(myFov.FieldOfView);

                        // Search for a target.
                        var coordinates = SearchHelper.SearchColor(ref screenCapture, Settings.Anabot.TargetColor, 3);

                        // Only continue if a healthbar was found.
                        if (coordinates.X != 0 || coordinates.Y != 0)
                        {
                            coordinates = ScreenHelper.GetAbsoluteCoordinates(coordinates, myFov.FieldOfView);

                            MouseHelper.Move(ref myFov, coordinates);
                        }

                        // Destroy the bitmap.
                        screenCapture.Dispose();
                        screenCapture = null;
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
