using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using Serenity.Helpers;
using Serenity.Objects;

namespace Serenity.Modules.Widowbot
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
            var myFov = Fovs.First(x => x.Resolution == new Point(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height));

            // Run the main routine.
            while (true)
            {
                if (MouseHelper.GetAsyncKeyState(Settings.Widowbot.AimKey) < 0)
                {
                    // Get the screen capture.
                    var screenCapture = ScreenHelper.GetScreenCapture(myFov.FieldOfView);

                    // Search for a target.
                    var coordinates = SearchHelper.SearchColor(ref screenCapture, Settings.Widowbot.TargetColor, 12);

                    // Only continue if a healthbar was found.
                    if (coordinates.X != 0 || coordinates.Y != 0)
                    {
                        coordinates = ScreenHelper.GetAbsoluteCoordinates(coordinates, myFov.FieldOfView);

                        MouseHelper.Move(ref myFov, coordinates, true);
                    }

                    // Destroy the bitmap.
                    screenCapture.Dispose();
                    screenCapture = null;
                }

                Thread.Sleep(1);
            }
        }
    }
}
