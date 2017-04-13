using System;
using System.Threading;

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
            Aimbot _Aimbot = new Aimbot();
            //Triggerbot _Triggerbot = new Triggerbot();
            Widowbot _Widowbot = new Widowbot();
            Anabot _Anabot = new Anabot();

            new Thread(delegate ()
            {
                while (true)
                {
                    string cmd = Console.ReadLine();

                    if (cmd == "aimbot.antishake")
                    {
                        if (!Settings.Aimbot.AntiShake)
                        {
                            Settings.Aimbot.AntiShake = true;
                        }
                        else
                        {
                            Settings.Aimbot.AntiShake = !Settings.Aimbot.AntiShake;
                        }
                        Console.WriteLine("Anti shake: {0}", Settings.Aimbot.AntiShake);
                    }
                    if (cmd == "anabot.toggle")
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
