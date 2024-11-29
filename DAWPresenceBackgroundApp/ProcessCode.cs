using System.Net;
using System.Reflection;
using System.Runtime.InteropServices;
using DAWPresence;
using DiscordRPC;
using YamlDotNet.Serialization;

namespace DAWPresenceBackgroundApp;

public class ProcessCode
{
    private const string AppVersion = "beta-0.1.9";
    private const int SwHide = 0;
    private const string ConfigFilePath = "./config.yml";
    private const string CreditText = "DAWPresence by @myuuiii";
    private static AppConfiguration _configuration;
    private static DiscordRpcClient? _client;
    private static DateTime? _startTime;

    public ProcessCode()
    {
        // Program.trayIcon.ShowBalloonTip(2000, "DAW Presence", "DAW Presence is running in the background", ToolTipIcon.Info);
        ApplicationConfiguration.Initialize();

        CheckLatestVersion();
        LoadConfiguration();
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

    private static void LoadConfiguration()
    {
        if (!Directory.Exists(Path.GetDirectoryName(ConfigFilePath)))
            Directory.CreateDirectory(Path.GetDirectoryName(ConfigFilePath));

        if (File.Exists(ConfigFilePath))
        {
            _configuration = new Deserializer().Deserialize<AppConfiguration>(File.ReadAllText(ConfigFilePath));
            Console.WriteLine("Configuration Loaded");
        }
        else
        {
            _configuration = new AppConfiguration();
            File.WriteAllText(ConfigFilePath, new SerializerBuilder().Build().Serialize(_configuration));
            Console.WriteLine("Configuration Created");
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
            if (_configuration.Debug)
            {
                _configuration = new Deserializer().Deserialize<AppConfiguration>(File.ReadAllText(ConfigFilePath));
            }

            var runningDaw = registeredDawArray.FirstOrDefault(d => d.IsRunning);
            if (runningDaw is null)
            {
                _client?.ClearPresence();
                _client?.Dispose();
                _startTime = null;
                Console.WriteLine("No DAW is running");
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
                    ? _configuration.WorkingPrefixText + runningDaw.GetProjectNameFromProcessWindow()
                    : _configuration.IdleText,
                State = "",
                Assets = new Assets
                {
                    LargeImageKey = _configuration.UseCustomImage
                        ? _configuration.CustomImageKey
                        : runningDaw.ImageKey,
                    LargeImageText = CreditText
                },
                Timestamps = new Timestamps
                {
                    Start = _startTime?.Add(-_configuration.Offset)
                }
            });

            await Task.Delay(_configuration.UpdateInterval);
        }
    }
}