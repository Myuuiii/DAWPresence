using System.Net;
using System.Reflection;
using System.Runtime.InteropServices;
using DAWPresence;
using DiscordRPC;
using YamlDotNet.Serialization;

namespace DAWPresenceBackgroundApp;

public class ProcessCode
{
    private const string AppVersion = "beta-0.1.12";
    private const int SwHide = 0;
    private const string CreditText = "DAWPresence by @myuuiii";
    private static DiscordRpcClient? _client;
    private static DateTime? _startTime;

    public ProcessCode()
    {
        ApplicationConfiguration.Initialize();
        CheckLatestVersion();
        ExecuteTaskAsync().GetAwaiter().GetResult();
    }

    private static void CheckLatestVersion()
    {
        try
        {
            var latestVersion = new WebClient().DownloadString("https://cdn.myuu.moe/v/dawpresence.txt");
            Console.WriteLine($"Latest version: {latestVersion}");

            if (latestVersion != AppVersion)
            {
                MessageBox.Show(
                    $"A new version of DAW Presence is available: {latestVersion}. Please download it from the official GitHub page https://github.com/Myuuiii/DAWPresence",
                    "DAW Presence", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        catch (WebException e)
        {
            MessageBox.Show($"An error occurred while checking for updates: {e.Message}", "DAW Presence",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    protected static async Task ExecuteTaskAsync()
    {
        IEnumerable<Daw?> dawInstances = Assembly.GetExecutingAssembly().GetTypes()
            .Where(t => t.IsSubclassOf(typeof(Daw)))
            .Select(t => (Daw?)Activator.CreateInstance(t));

        var registeredDawArray = dawInstances as Daw[] ?? dawInstances.ToArray();
        foreach (var daw in registeredDawArray)
        {
            Console.WriteLine(
                $"{daw?.DisplayName ?? "A null DAW instance was found in registeredDawArray"} has been registered");
        }

        while (true)
        {
            var runningDaw = registeredDawArray.FirstOrDefault(d => d.IsRunning);
            if (runningDaw is null)
            {
                _client?.ClearPresence();
                _client?.Dispose();
                _startTime = null;
                Console.WriteLine("No DAW is running");
                await Task.Delay(ConfigurationManager.Configuration.UpdateInterval);
                return;
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
                Details = !string.IsNullOrEmpty(runningDaw.GetProjectNameFromProcessWindow())
                    ? ConfigurationManager.Configuration.WorkingPrefixText + runningDaw.GetProjectNameFromProcessWindow()
                    : ConfigurationManager.Configuration.IdleText,
                State = "",
                Assets = new Assets
                {
                    LargeImageKey = ConfigurationManager.Configuration.UseCustomImage
                        ? ConfigurationManager.Configuration.CustomImageKey
                        : runningDaw.ImageKey,
                    LargeImageText = CreditText
                },
                Timestamps = new Timestamps
                {
                    Start = _startTime?.Add(-ConfigurationManager.Configuration.Offset)
                }
            });

            await Task.Delay(ConfigurationManager.Configuration.UpdateInterval);
        }
    }
}