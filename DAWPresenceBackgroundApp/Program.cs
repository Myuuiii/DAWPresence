using System.Diagnostics;

namespace DAWPresenceBackgroundApp;

internal static class Program
{
    private const string ProcessName = "DAWPresenceBackgroundApp";
    private static ProcessCode? _processCode;
    private static TrayIcon? _trayIcon;

    /// <summary>
    ///     The main entry point for the application.
    /// </summary>
    [STAThread]
    private static void Main()
    {
        var args = Environment.GetCommandLineArgs();
        if (IsArgPresent(args, "/uninstall"))
        {
            ProcessCode.SetStartup("DAWPresence", null, false);
            return;
        }

        ApplicationConfiguration.Initialize();
        ConfigurationManager.LoadConfiguration();
        if (IsAnotherInstanceRunning())
        {
            HandleMultipleInstances();
            return;
        }

        ShowStartupMessage();
        _trayIcon = new TrayIcon(ConfigurationManager.ConfigFilePath);
        _processCode = new ProcessCode();
        Task.Run(() => _processCode.RunAsync());
        Application.Run();
    }

    // Checks if a specific argument is present in the command line args
    private static bool IsArgPresent(string[] args, string arg)
    {
        return args.Any(a => string.Equals(a, arg, StringComparison.OrdinalIgnoreCase));
    }

    private static bool IsAnotherInstanceRunning()
    {
        return Process.GetProcessesByName(ProcessName).Length > 1;
    }

    private static void HandleMultipleInstances()
    {
        if (!ConfigurationManager.Configuration.DisablePopup)
            MessageBox.Show("DAW Presence will now shut down", "DAW Presence", MessageBoxButtons.OK,
                MessageBoxIcon.Information);

        foreach (var process in Process.GetProcessesByName(ProcessName)) process.Kill();
    }

    private static void ShowStartupMessage()
    {
        if (!ConfigurationManager.Configuration.DisablePopup)
            MessageBox.Show(
                "DAW Presence is now running in the background. Currently there is no tray icon, the software will run in the background. You can exit DAWPresence by running the executable again",
                "DAW Presence", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    public static void SetStartup(bool enable)
    {
        var exePath = Process.GetCurrentProcess().MainModule?.FileName ?? string.Empty;
        ProcessCode.SetStartup("DAWPresence", enable ? exePath : null, enable);
        ConfigurationManager.Configuration.OpenOnStartup = enable;
        ConfigurationManager.SaveConfiguration();
    }
}