using DiscordRPC;
using System.Reflection;

namespace DAWPresence.Services;

public class DiscordManager
{
    private static readonly string AppVersion = GetAppVersion();

    private static DiscordRpcClient? _client;
    private static DateTime? _startTime;

    public async Task RunAsync()
    {
        if (SettingsManager.Settings.CheckForUpdates)
        {
            await CheckLatestVersionAsync();
        }

        await ExecuteTaskAsync();
    }

    private static async Task CheckLatestVersionAsync()
    {
        try
        {
            using var httpClient = new HttpClient();
            var latestVersion =
                (await httpClient.GetStringAsync(Constants.GITHUB_VERSION_URL)).Trim();

            Console.WriteLine($"Latest version: {latestVersion}");

            if (latestVersion != AppVersion)
            {
                MessageBox.Show(
                    $"A new version of {Constants.APP_NAME} is available: {latestVersion}. " +
                    $"Please download it from the official GitHub page {Constants.GITHUB_PAGE_URL}",
                    Constants.APP_NAME, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        catch (Exception e)
        {
            MessageBox.Show($"An error occurred while checking for updates: {e.Message}",
                Constants.APP_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    protected static async Task ExecuteTaskAsync()
    {
        var dawInstances = Assembly.GetExecutingAssembly().GetTypes()
            .Where(t => t.IsSubclassOf(typeof(Daw)))
            .Select(t => (Daw?)Activator.CreateInstance(t));

        var registeredDaws = dawInstances as Daw[] ?? dawInstances.ToArray();

        foreach (var daw in registeredDaws)
        {
            Console.WriteLine(
                $"{daw?.DisplayName ?? "A null DAW instance was found in registeredDaws"} has been registered");
        }

        while (true)
        {
            var runningDaw = registeredDaws.FirstOrDefault(d => d != null && d.IsRunning);

            if (runningDaw is null)
            {
                _client?.ClearPresence();
                _client?.Dispose();
                _client = null;
                _startTime = null;
                Console.WriteLine("No DAW is running");
                await Task.Delay(SettingsManager.Settings.UpdateInterval);
                continue;
            }

            _startTime ??= DateTime.UtcNow;
            Console.WriteLine("Detected: " + runningDaw.DisplayName);

            var projectNameRaw = runningDaw.GetProjectNameFromProcessWindow();
            var projectName = projectNameRaw.TrimEnd('-', ' ', '\t', '\r', '\n');

            Console.WriteLine("Project: " + (string.IsNullOrEmpty(projectName) ? "(none)" : projectName));

            if (_client is null || _client.ApplicationID != runningDaw.ApplicationId)
            {
                _client?.ClearPresence();
                _client?.Dispose();
                _client = new DiscordRpcClient(runningDaw.ApplicationId);
                _client.Initialize();
            }

            var startTimestamp = _startTime.Value.Add(-SettingsManager.Settings.Offset);

            _client.SetPresence(new RichPresence
            {
                Details = SettingsManager.Settings.SecretMode
                    ? SettingsManager.Settings.SecretModeText
                    : !runningDaw.HideDetails && !string.IsNullOrEmpty(projectName)
                        ? SettingsManager.Settings.WorkingPrefixText + projectName
                        : runningDaw.HideDetails
                            ? null
                            : SettingsManager.Settings.IdleText,
                State = string.Empty,
                Assets = new Assets
                {
                    LargeImageKey = SettingsManager.Settings.UseCustomImage
                        ? SettingsManager.Settings.CustomImageKey
                        : runningDaw.ImageKey,
                    LargeImageText = Constants.APP_CREDITS
                },
                Timestamps = new Timestamps
                {
                    Start = startTimestamp
                }
            });

            await Task.Delay(SettingsManager.Settings.UpdateInterval);
        }
    }

    private static string GetAppVersion()
    {
        try
        {
            var versionFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Constants.APP_VERSION_PATH);
            if (File.Exists(versionFilePath))
            {
                return File.ReadAllText(versionFilePath).Trim();
            }
        }
        catch (Exception)
        {
        }

        return "0.0.0";
    }
}