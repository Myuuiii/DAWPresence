using DAWPresence.Services;

namespace DAWPresence;

internal static class Program
{
    private static DiscordManager? _discordManager;

    /// <summary>
    ///     The main entry point for the application.
    /// </summary>
    [STAThread]
    private static void Main()
    {
        var args = Environment.GetCommandLineArgs();

        if (IsArgPresent(args, "/uninstall"))
        {
            StartupManager.SetStartup(Constants.APP_NAME, null, false);
            return;
        }

        ApplicationConfiguration.Initialize();
        SettingsManager.LoadSettings();

        if (IsAnotherInstanceRunning())
        {
            HandleMultipleInstances();
            return;
        }

        ShowStartupMessage();

        using var trayManager = new TrayManager(SettingsManager.SettingsFilePath);

        _discordManager = new DiscordManager();
        Task.Run(() => _discordManager.RunAsync());

        Application.Run();
    }

    private static bool IsArgPresent(string[] args, string arg)
    {
        return args.Any(a => string.Equals(a, arg, StringComparison.OrdinalIgnoreCase));
    }

    private static bool IsAnotherInstanceRunning()
    {
        return System.Diagnostics.Process.GetProcessesByName(Constants.APP_NAME).Length > 1;
    }

    private static void HandleMultipleInstances()
    {
        if (!SettingsManager.Settings.DisablePopup)
        {
            ToastManager.ShowNotification(Constants.APP_NAME, $"{Constants.APP_NAME} will now shut down!");
        }

        foreach (var process in System.Diagnostics.Process.GetProcessesByName(Constants.APP_NAME)
                     .Where(p => p.Id != Environment.ProcessId))
        {
            process.Kill();
        }
    }

    private static void ShowStartupMessage()
    {
        if (!SettingsManager.Settings.DisablePopup)
        {
            ToastManager.ShowNotification(
                Constants.APP_NAME,
                $"{Constants.APP_NAME} is now running in the background. You can quit the app using the tray icon by right-clicking it and selecting Exit.");
        }
    }
}