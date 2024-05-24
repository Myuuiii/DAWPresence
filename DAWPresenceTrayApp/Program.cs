using System.Net;

namespace DAWPresenceTrayApp;

static class Program
{
    private const string VERSION = "beta-0.1.5";
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
        ApplicationConfiguration.Initialize();
        
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
        
        // Add tray icon
        NotifyIcon trayIcon = new NotifyIcon();
        trayIcon.Text = "DAW Presence";
        trayIcon.Icon = new Icon("appicon.ico");
        trayIcon.Visible = true;
        
        // Make the application shut down through the tray icon when c licked
        trayIcon.MouseClick += (sender, args) =>
        {
            if (args.Button == MouseButtons.Left)
            {
                Application.Exit();
            }
        };
        
        
        // Hide the window
        DAWRichPresenceTrayApp form = new DAWRichPresenceTrayApp();
        form.WindowState = FormWindowState.Minimized;
        form.ShowInTaskbar = false;
        Application.Run(form);
    }
}