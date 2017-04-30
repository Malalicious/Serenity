using System;

namespace Serenity.Helpers
{
    internal static class PrettyLog
    {
        private const ConsoleColor WarningColour = ConsoleColor.Yellow;
        private const ConsoleColor ErrorColour = ConsoleColor.Red;
        private const ConsoleColor SuccessColour = ConsoleColor.Green;
        private const ConsoleColor InfoColour = ConsoleColor.White;

        public static void LogWarning(string message)
        {
            LogMessage(WarningColour, message);
        }

        public static void LogInfo(string message)
        {
            LogMessage(InfoColour, message);
        }

        public static void LogError(string message)
        {
            LogMessage(ErrorColour, message);
        }

        public static void LogSuccess(string message)
        {
            LogMessage(SuccessColour, message);
        }

        private static void LogMessage(ConsoleColor color, string message)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ForegroundColor = InfoColour;
        }
    }
}
