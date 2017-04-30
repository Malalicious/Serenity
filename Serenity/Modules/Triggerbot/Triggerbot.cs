using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using Serenity.Helpers;
using Serenity.Objects;

namespace Serenity.Modules.Triggerbot
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
            var myFov = Fovs.First(x => x.Resolution == new Point(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height));

            while (true)
            {
                if (MouseHelper.GetAsyncKeyState(Settings.Triggerbot.AimKey) < 0)
                {
                    // Get the screen capture.
                    var screenCapture = ScreenHelper.GetScreenCapture(myFov.FieldOfView);

                    // Search for a target.
                    var coordinates = SearchHelper.SearchColor(ref screenCapture, Settings.Triggerbot.TargetColor, 100);

                    if (coordinates.X != 0 || coordinates.Y != 0)
                    {
                        MouseHelper.Click();
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
