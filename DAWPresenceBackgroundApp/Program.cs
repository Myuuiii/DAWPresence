using System.Diagnostics;
using System.Net;

namespace DAWPresenceBackgroundApp;

static class Program
{
    private const string VERSION = "beta-0.1.5";
    private static ProcessCode? processCode;
    public static NotifyIcon trayIcon;
    private const string ProcessName = "DAWPresenceBackgroundApp";

    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
        ApplicationConfiguration.Initialize();
        
        // If the program is started again, shut down all instances and exit
        if (Process.GetProcessesByName(ProcessName).Length > 1)
        {
            MessageBox.Show("DAW Presence will now shut down", "DAW Presence", MessageBoxButtons.OK, MessageBoxIcon.Information);
            foreach (var process in Process.GetProcessesByName(ProcessName))
            {
                process.Kill();
            }
            return;
        }

        string? latestVersion = null;
        try
        {
            latestVersion = new WebClient().DownloadString("https://minio.myuuiii.com/mversion/dawpresence.txt");
            Console.WriteLine($"Latest version: {latestVersion}");
            if (latestVersion != VERSION)
            {
                MessageBox.Show($"A new version of DAW Presence is available: {latestVersion}. Please download it from the official GitHub page https://github.com/Myuuiii/DAWPresence", "DAW Presence", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        catch (WebException e)
        {
            MessageBox.Show($"An error occurred while checking for updates: {e.Message}", "DAW Presence", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        
        MessageBox.Show("DAW Presence is now running in the background. Currently there is no tray icon, the software will run in the background. You can exit DAWPresence by running the executable again", "DAW Presence", MessageBoxButtons.OK, MessageBoxIcon.Information);
        
        processCode = new ProcessCode();

        Application.Run();
    }
}