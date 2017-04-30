using System;
using System.Threading;
using Microsoft.Win32;
using Serenity.Helpers;

namespace Serenity
{
    class Program
    {
        /// <summary>
        /// Entry point.
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            // Set the title of the window.
            Console.Title = "Dropbox";

            // Start the aimbot.
            //var aimbot = new Aimbot();
            //Triggerbot _Triggerbot = new Triggerbot();
            //var widowbot = new Widowbot();
            //var anabot = new Anabot();

            var prettyLog = new PrettyLog();

            prettyLog.LogSuccess("THING DO A THING");
            prettyLog.LogInfo("I TELL YOU A THING");
            prettyLog.LogError("BAD THING HAPPEN");
            prettyLog.LogWarning("MAYBE BAD THING HAPPEN?");

            new Thread(delegate ()
            {
                while (true)
                {
                    var command = Console.ReadLine()?.Split(' ');

                    if (command != null)
                    {
                        switch (command[0])
                        {
                            case "aimbot":
                                break;
                            case "anabot":
                                break;
                            default:
                                Console.WriteLine($"No command matching {command[0]}, please enter a valid command.");
                                Console.ReadLine();
                                break;
                        }
                    }

                    if (command[0] == "aimbot.antishake")
                    {
                        if (!Settings.Aimbot.AntiShake)
                        {
                            Settings.Aimbot.AntiShake = true;
                        }
                        else
                        {
                            Settings.Aimbot.AntiShake = !Settings.Aimbot.AntiShake;
                        }
                        Console.WriteLine($"Anti shake: {0}", Settings.Aimbot.AntiShake);
                    }
                    if (command[0] == "anabot.toggle")
                    {
                        if (!Settings.Anabot.IsEnabled)
                        {
                            Settings.Anabot.IsEnabled = true;
                        }
                        else
                        {
                            Settings.Anabot.IsEnabled = !Settings.Anabot.IsEnabled;
                        }
                        Console.WriteLine("Anabot: {0}", Settings.Anabot.IsEnabled);
                    }
                }
            }).Start();

            while (true)
            {
                // Register keypresses here.
                if (MouseHelper.GetAsyncKeyState(0x61) < 0) // Numpad1
                {
                    Settings.Aimbot.ForceHeadshot = !Settings.Aimbot.ForceHeadshot;
                    Console.WriteLine("Force headshot: {0}", Settings.Aimbot.ForceHeadshot);

                    MouseHelper.keybd_event(0x61, 0, 0x2, 0);
                    Thread.Sleep(200);
                }

                Thread.Sleep(1);
            }
        }
    }
}
