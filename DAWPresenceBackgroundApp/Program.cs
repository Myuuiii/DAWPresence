using System.Diagnostics;
using System.Net;

namespace DAWPresenceBackgroundApp;

static class Program
{
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
        ConfigurationManager.LoadConfiguration();

        // If the program is started again, shut down all instances and exit
        if (Process.GetProcessesByName(ProcessName).Length > 1)
        {
            if (!ConfigurationManager.Configuration.DisablePopup)
            {
                MessageBox.Show("DAW Presence will now shut down", "DAW Presence", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }

            foreach (var process in Process.GetProcessesByName(ProcessName))
            {
                process.Kill();
            }

            return;
        }

        if (!ConfigurationManager.Configuration.DisablePopup)
        {
            MessageBox.Show(
                "DAW Presence is now running in the background. Currently there is no tray icon, the software will run in the background. You can exit DAWPresence by running the executable again",
                "DAW Presence", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        processCode = new ProcessCode();

        Application.Run();
    }
}