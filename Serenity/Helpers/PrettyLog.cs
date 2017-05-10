using System;
using System.Collections.Generic;
using System.Text;

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
            LogSingleLine(WarningColour, message);
        }

        public static void LogInfo(string message)
        {
            LogSingleLine(InfoColour, message);
        }

        public static void LogError(string message)
        {
            LogSingleLine(ErrorColour, message);
        }

        public static void LogSuccess(string message)
        {
            LogSingleLine(SuccessColour, message);
        }

        private static void LogSingleLine(ConsoleColor color, string message)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        private static void LogMessage(IEnumerable<PrettyMessage> messages)
        {
            throw new NotImplementedException();
        }
    }

    internal class PrettyMessage
    {
        public ConsoleColor MessageColor { get; set; }
        public string Message { get; set; }
    }
}
