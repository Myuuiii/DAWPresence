namespace DAWPresenceTrayApp;

static class Program
{
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
        ApplicationConfiguration.Initialize();
        
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