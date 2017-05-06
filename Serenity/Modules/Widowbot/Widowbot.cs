using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using Serenity.Helpers;
using Serenity.Objects;

using static Serenity.Helpers.PrettyLog;

namespace Serenity.Modules.Widowbot
{
    internal class Widowbot : IModule
    {
        /// <summary>
        /// Contains all FOVs.
        /// </summary>
        public List<Fov> Fovs { get; set; }

        private Fov MyFov;

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

            MyFov = Fovs.FirstOrDefault(x => x.Resolution == new Point(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height));

            if (MyFov != null)
            {
                // Run the aimbot.
                new Thread(Run).Start();
                LogInfo("Widowbot initialized");
            }
            else
            {
                LogError("Could not initialize Widowbot as screen does not match resolution.\n" +
                         "This will be fixed later, for now make your screen resolution 1920x1080\nor 1280x720.\n");
            }
        }

        /// <summary>
        /// Main aimbot thread.
        /// </summary>
        public void Run()
        {
            // Run the main routine.
            while (true)
            {
                if (MouseHelper.GetAsyncKeyState(SettingsManager.Widowbot.AimKey) < 0)
                {
                    // Get the screen capture.
                    var screenCapture = ScreenHelper.GetScreenCapture(MyFov.FieldOfView);

                    // Search for a target.
                    var coordinates = SearchHelper.SearchColor(ref screenCapture, SettingsManager.Widowbot.TargetColor, 12);

                    // Only continue if a healthbar was found.
                    if (coordinates.X != 0 || coordinates.Y != 0)
                    {
                        coordinates = ScreenHelper.GetAbsoluteCoordinates(coordinates, MyFov.FieldOfView);

                        MouseHelper.Move(ref MyFov, coordinates, true);
                    }

                    // Destroy the bitmap.
                    screenCapture.Dispose();
                    screenCapture = null;
                }

                Thread.Sleep(1);
            }
        }

        public void HandleCommand(IEnumerable<string> args)
        {
            throw new System.NotImplementedException();
        }
    }
}
