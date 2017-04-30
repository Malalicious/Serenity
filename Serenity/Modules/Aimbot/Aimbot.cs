using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using Serenity.Helpers;
using Serenity.Objects;

namespace Serenity.Modules.Aimbot
{
    class Aimbot
    {
        /// <summary>
        /// Contains all FOVs.
        /// </summary>
        public List<Fov> Fovs { get; set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        public Aimbot()
        {
            // Initialize Fovs.
            Fovs = new List<Fov>
            {
                new Fov { Resolution = new Point(1920, 1080), FieldOfView = new Rectangle(775, 410, 370, 185), RangeValues = new Point(45, 56), Tolerance = new Point(2, 2) },
                new Fov { Resolution = new Point(1280, 720), FieldOfView = new Rectangle(500, 300, 245, 120), RangeValues = new Point(30, 42), Tolerance = new Point(2, 2) }
            };

            // Set default settings.
            Settings.Aimbot.AimKey = 0x06;
            Settings.Aimbot.ForceHeadshot = false;
            Settings.Aimbot.TargetColor = Color.FromArgb(255, 0, 19);

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
                if (MouseHelper.GetAsyncKeyState(Settings.Aimbot.AimKey) < 0)
                {
                    // Get the screen capture.
                    var screenCapture = ScreenHelper.GetScreenCapture(myFov.FieldOfView);

                    // Search for a target.
                    var coordinates = SearchHelper.SearchColor(ref screenCapture, Settings.Aimbot.TargetColor);

                    // Only continue if a healthbar was found.
                    if (coordinates.X != 0 || coordinates.Y != 0)
                    {
                        coordinates = ScreenHelper.GetAbsoluteCoordinates(coordinates, myFov.FieldOfView);

                        MouseHelper.Move(ref myFov, coordinates, Settings.Aimbot.ForceHeadshot);
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
