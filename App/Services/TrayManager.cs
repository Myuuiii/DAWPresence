using System.Diagnostics;

namespace DAWPresence.Services;

public class TrayManager : IDisposable
{
    private readonly string _settingsPath;
    private readonly Icon? _customIcon;
    private readonly NotifyIcon _trayIcon;

    public TrayManager(string settingsPath)
    {
        _settingsPath = settingsPath;

        _customIcon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);
        _trayIcon = new NotifyIcon
        {
            Icon = _customIcon,
            Text = Constants.APP_NAME,
            Visible = true
        };

        var startupMenuItem = new ToolStripMenuItem
        {
            Text = "Open on Startup",
            CheckOnClick = true,
            Checked = SettingsManager.Settings.OpenOnStartup
        };
        startupMenuItem.Click += ToggleStartup;

        var contextMenu = new ContextMenuStrip();
        contextMenu.Items.Add("Restart", null, RestartPresence);
        contextMenu.Items.Add("Settings", null, OpenSettings);
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

    private void OpenSettings(object? sender, EventArgs e)
    {
        try
        {
            if (File.Exists(_settingsPath))
            {
                Process.Start(new ProcessStartInfo(_settingsPath) { UseShellExecute = true });
            }
            else
            {
                MessageBox.Show("Settings file not found!", Constants.APP_NAME, MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error opening settings: {ex.Message}", Constants.APP_NAME, MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
    }

    private void ToggleStartup(object? sender, EventArgs e)
    {
        if (sender is not ToolStripMenuItem menuItem)
        {
            return;
        }

        SettingsManager.SetStartup(menuItem.Checked);

        if (menuItem.Checked)
        {
            MessageBox.Show($"{Constants.APP_NAME} will now open on startup.", Constants.APP_NAME,
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        else
        {
            MessageBox.Show($"{Constants.APP_NAME} will no longer open on startup.", Constants.APP_NAME,
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }

    private void ExitApp(object? sender, EventArgs e)
    {
        Application.Exit();
    }
}