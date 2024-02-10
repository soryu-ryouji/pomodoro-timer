using System.CommandLine;
using System.Diagnostics;

namespace PomodoroTimer;

public struct ConfigData
{
    public string WorkDuration;
    public string BreakDuration;

    public ConfigData()
    {
        WorkDuration = "25m";
        BreakDuration = "10m";
    }
}

class Program
{
    public static void Main(string[] args)
    {
        Config.LoadConfig();
        ParseArgs(args);
    }

    private static void ParseArgs(string[] args)
    {
        var rootCommand = new RootCommand("A Simple Pomodoro Timer");

        var duration = new Option<string?>(
            aliases: ["-d", "--duration"],
            description: "Timer Duration");
        var name = new Option<string?>(
            aliases: ["-n", "--name"],
            description: "Timer Name"
        );

        rootCommand.AddOption(duration);
        rootCommand.AddOption(name);

        rootCommand.SetHandler((time, name) =>
            {
                CallTimer(time ?? "25", name ?? "");
            }, duration, name);

        var workCommand = new Command(name: "work", "Start Pomodoro");
        var breakCommand = new Command(name: "break", "Break Time");

        workCommand.SetHandler(() => CallTimer(Config.Data.WorkDuration, "Work"));
        breakCommand.SetHandler(() => CallTimer(Config.Data.BreakDuration, "Break"));

        rootCommand.AddCommand(workCommand);
        rootCommand.AddCommand(breakCommand);

        rootCommand.Invoke(args);
    }

    static void CallTimer(string duration, string name)
    {
        var nameCommand = name != "" ? $" -n {name}" : "";
        using var ps = new Process();
        var psi = new ProcessStartInfo("timer", $"{duration}{nameCommand}");
        ps.StartInfo = psi;
        ps.Start();
        ps.WaitForExit();
    }
}