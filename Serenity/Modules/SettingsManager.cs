using System;
using System.Drawing;
using System.IO;
using Newtonsoft.Json;
using Serenity.Helpers;

namespace Serenity.Modules
{
    internal class SettingsManager
    {
        public static AimbotSettings Aimbot;
        public static AnabotSettings Anabot;
        public static WidowbotSettings Widowbot;
        public static TriggerbotSettings Triggerbot;

        private const string SettingsFolderPath = "settings";

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

            public Color TargetColor { get; set; }

            public int AimKey { get; set; }
        }

        internal class AnabotSettings
        {
            public AnabotSettings()
            {
                AimKey = 0x05;
                TargetColor = Color.FromArgb(202, 164, 63);
            }

            public bool IsEnabled { get; set; }

            public Color TargetColor { get; set; }

            public byte AimKey { get; set; }
        }

        internal class TriggerbotSettings
        {
            public TriggerbotSettings()
            {
                AimKey = 0xA4;
                TargetColor = Color.FromArgb(254, 0, 0);
            }

            public Color TargetColor { get; set; }

            public byte AimKey { get; set; }
        }

        public void SaveSettingsToFile()
        {
            var serializer = new JsonSerializer();
            using (var file = File.CreateText($"{SettingsFolderPath}\\aimbot.json"))
            {
                serializer.Serialize(file, Aimbot);
            }
            using (var file = File.CreateText($"{SettingsFolderPath}\\anabot.json"))
            {
                serializer.Serialize(file, Anabot);
            }
            using (var file = File.CreateText($"{SettingsFolderPath}\\widowbot.json"))
            {
                serializer.Serialize(file, Widowbot);
            }
            using (var file = File.CreateText($"{SettingsFolderPath}\\triggerbot.json"))
            {
                serializer.Serialize(file, Triggerbot);
            }
        }

        public void LoadSettingsFromDefaultPath()
        {
            var settingsFolder = $"{Environment.CurrentDirectory}\\settings";
            if (!Directory.Exists(settingsFolder))
            {
                PrettyLog.LogWarning($"No settings folder found, creating it now at {settingsFolder}.");
                Directory.CreateDirectory(settingsFolder);
            }

            if (!File.Exists($"{settingsFolder}\\aimbot.json"))
            {
                PrettyLog.LogWarning("No aimbot settings found, initializing with default values.");
                Aimbot = new AimbotSettings();
            }
            else
            {
                var file = File.ReadAllText($"{settingsFolder}/aimbot.json");
                Aimbot = JsonConvert.DeserializeObject<AimbotSettings>(file);
            }

            if (!File.Exists($"{settingsFolder}\\anabot.json"))
            {
                PrettyLog.LogWarning("No anabot settings found, initializing with default values.");
                Anabot = new AnabotSettings();
            }
            else
            {
                var file = File.ReadAllText($"{settingsFolder}/anabot.json");
                Anabot = JsonConvert.DeserializeObject<AnabotSettings>(file);
            }

            if (!File.Exists($"{settingsFolder}\\widowbot.json"))
            {
                PrettyLog.LogWarning("No widowbot settings found, initializing with default values.");
                Widowbot = new WidowbotSettings();
            }
            else
            {
                var file = File.ReadAllText($"{settingsFolder}/anabot.json");
                Widowbot = JsonConvert.DeserializeObject<WidowbotSettings>(file);
            }

            if (!File.Exists($"{settingsFolder}\\triggerbot.json"))
            {
                PrettyLog.LogWarning("No triggerbot settings found, initializing with default values.");
                Triggerbot = new TriggerbotSettings();
            }
            else
            {
                var file = File.ReadAllText($"{settingsFolder}/triggerbot.json");
                Triggerbot = JsonConvert.DeserializeObject<TriggerbotSettings>(file);
            }
            SaveSettingsToFile();
        }
    }
}
