using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using Serenity.Helpers;
using Serenity.Objects;

using static Serenity.Helpers.PrettyLog;

namespace Serenity.Modules.Aimbot
{
    internal class Aimbot : IModule
    {
        /// <summary>
        /// Contains all FOVs.
        /// </summary>
        public List<Fov> Fovs;

        private Fov MyFov;

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

            MyFov = Fovs.FirstOrDefault(x => x.Resolution == new Point(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height));

            if (MyFov != null)
            {
                // Run the aimbot.
                new Thread(Run).Start();
                LogInfo("Aimbot initialized");
            }
            else
            {
                LogError("Could not initialize Aimbot as screen does not match resolutions available.\n" +
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
                if (MouseHelper.GetAsyncKeyState(SettingsManager.Aimbot.AimKey) < 0)
                {
                    // Get the screen capture.
                    var screenCapture = ScreenHelper.GetScreenCapture(MyFov.FieldOfView);

                    // Search for a target.
                    var coordinates = SearchHelper.SearchColor(ref screenCapture, SettingsManager.Aimbot.TargetColor);

                    // Only continue if a healthbar was found.
                    if (coordinates.X != 0 || coordinates.Y != 0)
                    {
                        coordinates = ScreenHelper.GetAbsoluteCoordinates(coordinates, MyFov.FieldOfView);

                        MouseHelper.Move(ref MyFov, coordinates, SettingsManager.Aimbot.ForceHeadshot);
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
            var argsArray = args.ToArray();
            if (!argsArray.Any())
            {
                LogError("You must specify a command, type 'settings help' for help.");
                return;
            }
            var command = argsArray[0];
            switch (command)
            {
                case "forcehs":
                case "hs":
                case "headshot":
                    SettingsManager.Aimbot.ForceHeadshot = !SettingsManager.Aimbot.ForceHeadshot;
                    LogInfo($"Force headshots: {SettingsManager.Aimbot.ForceHeadshot}");
                    break;
                case "antishake":
                case "noshake":
                    SettingsManager.Aimbot.ForceHeadshot = !SettingsManager.Aimbot.ForceHeadshot;
                    LogInfo($"Force headshots: {SettingsManager.Aimbot.ForceHeadshot}");
                    break;
                case "help":
                    LogInfo("Commands available for Aimbot:\n\n" +
                            "Headshot, forcehs, hs\t- Force aimbot to aim for heads only.\n" +
                            "Antishake, noshake\t- I don't really understand what this does lmao.\n" +
                            "Help\t\t\t- Print this text again.\n");
                    break;
                default:
                    LogWarning($"Unrecognised command {command}.\nType 'aimbot help' to view all commands.\n");
                    break;
            }
        }

        public Fov GetFov()
        {
            return MyFov;
        }
    }
}
