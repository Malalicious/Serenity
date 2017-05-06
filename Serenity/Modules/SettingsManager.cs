using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Serenity.Helpers;

using static Serenity.Helpers.PrettyLog;

namespace Serenity.Modules
{
    internal class SettingsManager : IModule
    {
        public static AimbotSettings Aimbot;
        public static AnabotSettings Anabot;
        public static WidowbotSettings Widowbot;
        public static TriggerbotSettings Triggerbot;

        private const string SettingsFolderName = "settings";
        private readonly string SettingsFolderFullPath = $"{Environment.CurrentDirectory}\\settings";

        internal class AimbotSettings
        {
            public AimbotSettings()
            {
                AimKey = 0x02;
                AntiShake = false;
                ForceHeadshot = false;
                TargetColor = Color.FromArgb(255, 0, 19);
            }

            public Color TargetColor { get; set; }

            public byte AimKey { get; set; }

            public bool ForceHeadshot { get; set; }

            public bool AntiShake { get; set; }
        }

        internal class WidowbotSettings
        {
            public WidowbotSettings()
            {
                AimKey = 0xA4;
                TargetColor = Color.FromArgb(215, 40, 35);
            }

            public int AimKey { get; set; }

            public Color TargetColor { get; set; }
        }

        internal class AnabotSettings
        {
            public AnabotSettings()
            {
                AimKey = 0x05;
                IsEnabled = false;
                TargetColor = Color.FromArgb(202, 164, 63);
            }

            public byte AimKey { get; set; }

            public bool IsEnabled { get; set; }

            public Color TargetColor { get; set; }

        }

        internal class TriggerbotSettings
        {
            public TriggerbotSettings()
            {
                AimKey = 0xA4;
                IsEnabled = false;
                TargetColor = Color.FromArgb(254, 0, 0);
            }

            public byte AimKey { get; set; }

            public bool IsEnabled { get; set; }

            public Color TargetColor { get; set; }
        }

        public void SaveSettingsToFile()
        {
            SerializeObjectToFile(Aimbot, $"{SettingsFolderName}\\aimbot.json");
            SerializeObjectToFile(Anabot, $"{SettingsFolderName}\\anabot.json");
            SerializeObjectToFile(Widowbot, $"{SettingsFolderName}\\widowbot.json");
            SerializeObjectToFile(Triggerbot, $"{SettingsFolderName}\\triggerbot.json");
        }

        public void LoadSettingsFromDefaultPath()
        {
            if (!Directory.Exists(SettingsFolderFullPath))
            {
                LogWarning($"No settings folder found, creating it now at {SettingsFolderFullPath}.");
                Directory.CreateDirectory(SettingsFolderFullPath);
            }

            Aimbot = ReadJsonFromFile<AimbotSettings>($"{SettingsFolderFullPath}\\aimbot.json");
            Anabot = ReadJsonFromFile<AnabotSettings>($"{SettingsFolderFullPath}\\anabot.json");
            Triggerbot = ReadJsonFromFile<TriggerbotSettings>($"{SettingsFolderFullPath}\\triggerbot.json");
            Widowbot = ReadJsonFromFile<WidowbotSettings>($"{SettingsFolderFullPath}\\widowbot.json");
            SaveSettingsToFile();
        }

        private static T ReadJsonFromFile<T>(string filePath) where T : new()
        {
            if (!File.Exists(filePath))
            {
                LogWarning($"No file found for {typeof(T).Name}, creating new file from scratch.");
                return new T();
            }
            using (var file = File.OpenText(filePath))
            {
                return JsonConvert.DeserializeObject<T>(file.ReadToEnd());
            }
        }

        private static void SerializeObjectToFile(object obj, string filePath)
        {
            var serializer = new JsonSerializer();
            using (var file = File.CreateText(filePath))
            {
                serializer.Serialize(file, obj);
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
                case "save":
                    LogInfo("Attempting to save settings");
                    SaveSettingsToFile();
                    LogInfo("Settings saved successfully.");
                    break;
                case "load":
                    LogInfo("Attempting to load settings from file");
                    LoadSettingsFromDefaultPath();
                    LogInfo("Settings loaded successfully.");
                    break;
                case "help":
                    LogInfo("Commands available for Settings Manager:\n\n" +
                            "Save\t- Save Serenity's settings to file.\n" +
                            "Load\t- Load all settings from file.\n" +
                            "Help\t- Print this text again.\n");
                    break;
                default:
                    LogWarning($"Unrecognised command {command}.\nType 'settings help' to view all commands.\n");
                    break;
            }
        }
    }
}
