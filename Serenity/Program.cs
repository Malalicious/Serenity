using System;
using System.Linq;
using System.Threading;
using Microsoft.Win32;
using Serenity.Helpers;
using Serenity.Modules;
using Serenity.Modules.Aimbot;
using Serenity.Modules.Anabot;
using Serenity.Modules.Triggerbot;
using Serenity.Modules.Widowbot;
using static Serenity.Helpers.PrettyLog;

namespace Serenity
{
    internal class Program
    {
        /// <summary>
        /// Entry point.
        /// </summary>
        /// <param name="args"></param>
        private static void Main(string[] args)
        {
            // Set the title of the window.
            Console.Title = "Dropbox";

            LogInfo("Initializing settings.");
            var settingsManager = new SettingsManager();
            settingsManager.LoadSettingsFromDefaultPath();
            LogInfo("Settings loaded successfully.");

            // Start the aimbot.
            var aimbot = new Aimbot();
            var triggerbot = new Triggerbot();
            var widowbot = new Widowbot();
            var anabot = new Anabot();

            new Thread(delegate ()
            {
                while (true)
                {
                    var commandArgs = Console.ReadLine()?.ToLower().Split(' ').ToList();

                    if (commandArgs == null) continue;

                    switch (commandArgs[0])
                    {
                        case "aimbot":
                            aimbot.HandleCommand(commandArgs);
                            break;
                        case "anabot":
                            aimbot.HandleCommand(commandArgs);
                            break;
                        case "widowbot":
                            aimbot.HandleCommand(commandArgs);
                            break;
                        case "triggerbot":
                            aimbot.HandleCommand(commandArgs);
                            break;
                        default:
                            LogError($"No command matching '{commandArgs[0]}', please enter a valid command.");
                            break;
                    }
                }
            }).Start();

            while (true)
            {
                // Register keypresses here.
                if (MouseHelper.GetAsyncKeyState(0x61) < 0) // Numpad1
                {
                    SettingsManager.Aimbot.ForceHeadshot = !SettingsManager.Aimbot.ForceHeadshot;
                    LogInfo($"Force headshot: { SettingsManager.Aimbot.ForceHeadshot}");

                    MouseHelper.keybd_event(0x61, 0, 0x2, 0);
                    Thread.Sleep(200);
                }

                Thread.Sleep(1);
            }
        }
    }
}
