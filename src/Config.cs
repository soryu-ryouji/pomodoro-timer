using Tomlyn;
using Tomlyn.Model;

namespace PomodoroTimer;

class Config
{   
    public static ConfigData Data = new();

    public static void LoadConfig()
    {
        var osVersion = Environment.OSVersion.Platform;
        string? configPath = osVersion switch
        {
            PlatformID.Unix => Path.Combine(Environment.GetEnvironmentVariable("HOME"),".config","pomodoro-timer"),                
            // PlatformID.Win32NT => Environment.ExpandEnvironmentVariables("%HOMEDRIVE%%HOMEPATH%"),
            _ => throw new Exception("Just Support Unix OS"),
        };

        if (!Directory.Exists(configPath)) Directory.CreateDirectory(configPath);

        var configFilePath = Path.Combine(configPath,"config.toml");
        if (!File.Exists(configFilePath))
        {
            var model = new TomlTable
            {
                { "work_duration", "25m" },
                { "break_duration", "10m" }
            };

            var tempText = Toml.FromModel(model);
            File.WriteAllText(configFilePath, tempText);
        }

        var configText = File.ReadAllText(configFilePath);
        var configModel = Toml.ToModel(configText);

        Data.WorkDuration = (string)configModel["work_duration"];
        Data.BreakDuration = (string)configModel["break_duration"];
    }
}