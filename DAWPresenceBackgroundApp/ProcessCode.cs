using System.Reflection;
using DAWPresence;
using DiscordRPC;
using Microsoft.Win32;

namespace DAWPresenceBackgroundApp;

public class ProcessCode
{
    private const string AppVersion = "beta-0.2.4";
    private const string CreditText = "DAWPresence by @myuuiii";
    private const string StartupRegistryPath = "Software\\Microsoft\\Windows\\CurrentVersion\\Run";
    private static DiscordRpcClient? _client;
    private static DateTime? _startTime;

    public async Task RunAsync()
    {
        if (ConfigurationManager.Configuration.CheckForUpdates) await CheckLatestVersionAsync();
        await ExecuteTaskAsync();
    }

    private static async Task CheckLatestVersionAsync()
    {
        try
        {
            using var httpClient = new HttpClient();
            var latestVersion =
                (await httpClient.GetStringAsync(
                    "https://raw.githubusercontent.com/Myuuiii/DAWPresence/master/VERSION.txt")).Trim();
            Console.WriteLine($"Latest version: {latestVersion}");

            if (latestVersion != AppVersion)
                MessageBox.Show(
                    $"A new version of DAW Presence is available: {latestVersion}. Please download it from the official GitHub page https://github.com/Myuuiii/DAWPresence",
                    "DAW Presence", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception e)
        {
            MessageBox.Show($"An error occurred while checking for updates: {e.Message}", "DAW Presence",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    protected static async Task ExecuteTaskAsync()
    {
        var dawInstances = Assembly.GetExecutingAssembly().GetTypes()
            .Where(t => t.IsSubclassOf(typeof(Daw)))
            .Select(t => (Daw?)Activator.CreateInstance(t));

        var registeredDawArray = dawInstances as Daw[] ?? dawInstances.ToArray();
        foreach (var daw in registeredDawArray)
            Console.WriteLine(
                $"{daw?.DisplayName ?? "A null DAW instance was found in registeredDawArray"} has been registered");

        while (true)
        {
            var runningDaw = registeredDawArray.FirstOrDefault(d => d != null && d.IsRunning);
            if (runningDaw is null)
            {
                _client?.ClearPresence();
                _client?.Dispose();
                _client = null;
                _startTime = null;
                Console.WriteLine("No DAW is running");
                await Task.Delay(ConfigurationManager.Configuration.UpdateInterval);
                continue;
            }

            _startTime ??= DateTime.UtcNow;
            Console.WriteLine("Detected: " + runningDaw.DisplayName);
            Console.WriteLine("Project: " + runningDaw.GetProjectNameFromProcessWindow());

            if (_client is null || _client.ApplicationID != runningDaw.ApplicationId)
            {
                _client?.ClearPresence();
                _client?.Dispose();
                _client = new DiscordRpcClient(runningDaw.ApplicationId);
                _client.Initialize();
            }

            _client.SetPresence(new RichPresence
            {
                Details = !runningDaw.HideDetails && !string.IsNullOrEmpty(runningDaw.GetProjectNameFromProcessWindow())
                    ? ConfigurationManager.Configuration.WorkingPrefixText +
                      runningDaw.GetProjectNameFromProcessWindow()
                    : runningDaw.HideDetails
                        ? null
                        : ConfigurationManager.Configuration.IdleText,
                State = string.Empty,
                Assets = new Assets
                {
                    LargeImageKey = ConfigurationManager.Configuration.UseCustomImage
                        ? ConfigurationManager.Configuration.CustomImageKey
                        : runningDaw.ImageKey,
                    LargeImageText = CreditText
                },
                Timestamps = new Timestamps
                {
                    Start = _startTime.Value.Add(-ConfigurationManager.Configuration.Offset)
                }
            });

            await Task.Delay(ConfigurationManager.Configuration.UpdateInterval);
        }
    }

    /// <summary>
    ///     Adds or removes this application from Windows startup for the current user.
    /// </summary>
    /// <param name="appName">The name to use for the registry entry.</param>
    /// <param name="exePath">The full path to the executable (for add).</param>
    /// <param name="add">True to add to startup, false to remove.</param>
    public static void SetStartup(string appName, string? exePath, bool add)
    {
        try
        {
            using var key = OpenStartupRegistryKey();
            if (key == null)
            {
                Console.WriteLine("Failed to open registry key for startup.");
                return;
            }

            if (add)
            {
                if (string.IsNullOrWhiteSpace(exePath))
                {
                    Console.WriteLine("Executable path is null or empty. Cannot add to startup.");
                    return;
                }

                key.SetValue(appName, $"\"{exePath}\"");
                Console.WriteLine($"Added {appName} to startup with path: {exePath}");
            }
            else
            {
                key.DeleteValue(appName, false);
                Console.WriteLine($"Removed {appName} from startup.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error modifying startup: {ex.Message}");
        }
    }

    [Obsolete("Use SetStartup instead.")]
    public static void AddToStartup(string appName, string exePath)
    {
        SetStartup(appName, exePath, true);
    }

    [Obsolete("Use SetStartup instead.")]
    public static void RemoveFromStartup(string appName)
    {
        SetStartup(appName, null, false);
    }

    /// <summary>
    ///     Opens the registry key for Windows startup (CurrentUser).
    /// </summary>
    /// <returns>The registry key, or null if it cannot be opened.</returns>
    private static RegistryKey? OpenStartupRegistryKey()
    {
        return Registry.CurrentUser.OpenSubKey(StartupRegistryPath, true);
    }
}