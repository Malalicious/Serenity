using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading;

namespace Serenity
{
    class MouseHelper
    {
        [DllImport("User32.dll")]
        public static extern short GetAsyncKeyState(int vKey);

        [DllImport("user32.dll")]
        static extern void mouse_event(int dwFlags, int dx, int dy, int dwData, int dwExtraInfo);

        [DllImport("user32.dll")]
        public static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, int dwExtraInfo);

        /// <summary>
        /// Clicks the mouse.
        /// </summary>
        public static void Click()
        {
            mouse_event(0x02, 0, 0, 0, 0);
            mouse_event(0x04, 0, 0, 0, 0);
        }

        /// <summary>
        /// Moves the mouse.
        /// </summary>
        /// <param name="MyFov"></param>
        /// <param name="Coordinates"></param>
        public static void Move(ref Fov MyFov, Point Coordinates, bool ForceHeadshot = false)
        {
            // Get the crosshair position.
            Point Crosshair = new Point(0, 0);
            Crosshair.X = MyFov.Resolution.X / 2;
            Crosshair.Y = MyFov.Resolution.Y / 2;

            // Determine the stepcounts.
            Point StepCount = new Point(0, 0);
            Point Step = new Point(0, 0);

            // Calculate the destination.
            Point Destination = new Point(0, 0);
            Destination.X = Coordinates.X + MyFov.RangeValues.X;
            Destination.Y = Coordinates.Y + MyFov.RangeValues.Y;

            // Calculate the difference from the crosshair.
            Point Difference = new Point(0, 0);
            Difference.X = Math.Abs(Crosshair.X - Destination.X);
            Difference.Y = Math.Abs(Crosshair.Y - Destination.Y);
            
            StepCount.X = 5;
            StepCount.Y = 3;

            // X-axis.
            if (Difference.X < 10)
            {
                StepCount.X = 1;

                if (Settings.Aimbot.AntiShake)
                {
                    Thread.Sleep(1);
                }
            }
            else if (Difference.X < 40)
            {
                StepCount.X = 5;
                Thread.Sleep(1);
            }

            Step.X = StepCount.X;

            if (Crosshair.X > Destination.X)
            {
                Step.X = -StepCount.X;
            }

            // Y-axis.
            if (Difference.Y < 10)
            {
                StepCount.Y = 1;
            }

            Step.Y = StepCount.Y;

            if (Crosshair.Y > Destination.Y)
            {
                Step.Y = -StepCount.Y;
            }

            if (Crosshair.X > Destination.X + MyFov.Tolerance.X || Crosshair.X < Destination.X - MyFov.Tolerance.X)
            {
                ExecuteMove(Step.X, 0);

                if (ForceHeadshot)
                {
                    if (Crosshair.Y > Destination.Y + MyFov.Tolerance.Y || Crosshair.Y < Destination.Y - MyFov.Tolerance.Y)
                    {
                        ExecuteMove(0, Step.Y);
                    }
                }
            }
        }

        /// <summary>
        /// Executes the mouse moves.
        /// </summary>
        /// <param name="Step"></param>
        /// <param name="Async"></param>
        private static void ExecuteMove(Point Step, bool Async = false)
        {
            if (!Async)
            {
                mouse_event(0x1, Step.X, Step.Y, 0, 0);
            }
            else
            {
                // Move X.
                new Thread(() =>
                {
                    mouse_event(0x1, Step.X, 0, 0, 0);
                }).Start();

                // Move Y.
                new Thread(() =>
                {
                    mouse_event(0x1, 0, Step.Y, 0, 0);
                }).Start();
            }
        }

        /// <summary>
        /// Moves the mouse.
        /// </summary>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        private static void ExecuteMove(int X, int Y)
        {
            ExecuteMove(new Point(X, Y));
        }
    }
}
