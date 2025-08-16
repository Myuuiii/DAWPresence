using System.Diagnostics;

namespace DAWPresenceBackgroundApp;

public class TrayIcon : IDisposable
{
    private readonly string _configPath;
    private readonly Icon? _customIcon;
    private readonly NotifyIcon _trayIcon;

    public TrayIcon(string configPath)
    {
        _configPath = configPath;

        var exeDirectory = AppDomain.CurrentDomain.BaseDirectory;
        var iconPath =
            Path.Combine(exeDirectory,
                "appicon.ico"); // Make sure you have an icon file named appicon.ico in the same directory :D

        // Load custom icon IF it's available
        if (File.Exists(iconPath))
        {
            _customIcon = new Icon(iconPath);
            _trayIcon = new NotifyIcon
            {
                Icon = _customIcon,
                Text = "DAWPresence",
                Visible = true
            };
        }
        else
        {
            _trayIcon = new NotifyIcon
            {
                Icon = SystemIcons.Application,
                Text = "DAWPresence",
                Visible = true
            };
        }


        var startupMenuItem = new ToolStripMenuItem
        {
            Text = "Open on Startup",
            CheckOnClick = true,
            Checked = ConfigurationManager.Configuration.OpenOnStartup
        };
        startupMenuItem.Click += ToggleStartup;

        // Create context menu for the tray icon
        var contextMenu = new ContextMenuStrip();
        contextMenu.Items.Add("Restart", null, RestartPresence);
        contextMenu.Items.Add("Settings", null, OpenConfig);
        contextMenu.Items.Add(startupMenuItem);
        contextMenu.Items.Add("Exit", null, ExitApp);

        _trayIcon.ContextMenuStrip = contextMenu;
    }

    public void Dispose()
    {
        _trayIcon.Visible = false;
        _trayIcon.Dispose();
        _customIcon?.Dispose();
    }

    private void RestartPresence(object? sender, EventArgs e)
    {
        Application.Restart();
    }

    private void OpenConfig(object? sender, EventArgs e)
    {
        try
        {
            if (File.Exists(_configPath))
                Process.Start(new ProcessStartInfo(_configPath) { UseShellExecute = true });
            else
                MessageBox.Show("Config file not found!", "DAWPresence", MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error opening config: {ex.Message}", "DAWPresence", MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
    }

    private void ToggleStartup(object? sender, EventArgs e)
    {
        var menuItem = sender as ToolStripMenuItem;
        if (menuItem == null) return;

        var isChecked = menuItem.Checked;

        ConfigurationManager.SetStartup(isChecked); // Update registry, config, and save
        if (isChecked)
            MessageBox.Show("DAWPresence will now open on startup.", "DAWPresence", MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        else
            MessageBox.Show("DAWPresence will no longer open on startup.", "DAWPresence", MessageBoxButtons.OK,
                MessageBoxIcon.Information);

        // ConfigurationManager.Configuration.OpenOnStartup = isChecked; // No longer needed
        // ConfigurationManager.SaveConfiguration(); // No longer needed
    }

    private void ExitApp(object? sender, EventArgs e)
    {
        Application.Exit();
    }
}