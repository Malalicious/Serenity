using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace Serenity
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
            Fov MyFov = Fovs.First(x => x.Resolution == new Point(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height));

            // Run the main routine.
            while (true)
            {
                if (MouseHelper.GetAsyncKeyState(Settings.Aimbot.AimKey) < 0)
                {
                    // Get the screen capture.
                    Bitmap ScreenCapture = ScreenHelper.GetScreenCapture(MyFov.FieldOfView);

                    // Search for a target.
                    Point Coordinates = SearchHelper.SearchColor(ref ScreenCapture, Settings.Aimbot.TargetColor);

                    // Only continue if a healthbar was found.
                    if (Coordinates.X != 0 || Coordinates.Y != 0)
                    {
                        Coordinates = ScreenHelper.GetAbsoluteCoordinates(Coordinates, MyFov.FieldOfView);

                        MouseHelper.Move(ref MyFov, Coordinates, Settings.Aimbot.ForceHeadshot);
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
