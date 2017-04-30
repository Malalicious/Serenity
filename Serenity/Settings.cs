using System.Drawing;

namespace Serenity
{
    internal class Settings
    {
        internal class Aimbot
        {
            public static Color TargetColor { get; set; }

            public static int AimKey { get; set; }

            public static bool ForceHeadshot { get; set; }

            public static bool AntiShake { get; set; }
        }

        internal class Widowbot
        {
            public static Color TargetColor { get; set; }

            public static int AimKey { get; set; }
        }

        internal class Anabot
        {
            public static bool IsEnabled { get; set; }

            public static Color TargetColor { get; set; }

            public static int AimKey { get; set; }
        }

        internal class Triggerbot
        {
            public static Color TargetColor { get; set; }
            
            public static int AimKey { get; set; }
        }
    }
}
