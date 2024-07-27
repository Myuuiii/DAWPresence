using System.Net;
using System.Reflection;
using System.Runtime.InteropServices;
using DAWPresence;
using DiscordRPC;
using YamlDotNet.Serialization;

namespace DAWPresenceBackgroundApp;

public class ProcessCode
{
    private const string VERSION = "beta-0.1.5";

    [DllImport("user32.dll")]
    static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

    const int SW_HIDE = 0;
    
    // config in the appdata folder
    private const string CONFIG_FILE_NAME = "./config.yml";
    private const string CREDIT = "DAWPresence by @myuuiii";
    private static AppConfiguration _configuration;
    private static DiscordRpcClient? client;
    private static DateTime? startTime;

    public ProcessCode()
    {
        // Program.trayIcon.ShowBalloonTip(2000, "DAW Presence", "DAW Presence is running in the background", ToolTipIcon.Info);
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
        
        // Make sure the entire directory exists
        if (!Directory.Exists(Path.GetDirectoryName(CONFIG_FILE_NAME)))
            Directory.CreateDirectory(Path.GetDirectoryName(CONFIG_FILE_NAME));

        if (File.Exists(CONFIG_FILE_NAME))
        {
            _configuration =
                new Deserializer().Deserialize<AppConfiguration>(
                    File.ReadAllText(CONFIG_FILE_NAME));
            Console.WriteLine("Configuration Loaded");
        }
        else
        {
            _configuration = new AppConfiguration();
            File.WriteAllText(CONFIG_FILE_NAME,
                new SerializerBuilder().Build().Serialize(_configuration));
            Console.WriteLine("Configuration Created");
        }
        
        ExecuteTaskAsync().GetAwaiter().GetResult();

    }
    
    protected static async Task ExecuteTaskAsync()
    {
        IEnumerable<Daw?> registeredDaws = Assembly.GetExecutingAssembly().GetTypes()
            .Where(t => t.IsSubclassOf(typeof(Daw)))
            .Select(t => (Daw?)Activator.CreateInstance(t));
        IEnumerable<Daw?> regDawArray = registeredDaws as Daw[] ?? registeredDaws.ToArray();

        foreach (Daw? r in regDawArray)
        {
            if (r != null)
            {
                Console.WriteLine($"{r.DisplayName} has been registered");
            }
            else
            {
                Console.WriteLine("A null DAW instance was found in regDawArray");
            }
        }

        while (true)
        {
            if (_configuration.Debug)
            {
                _configuration =
                    new Deserializer().Deserialize<AppConfiguration>(
                        File.ReadAllText(CONFIG_FILE_NAME)); 
            }
           
            Daw? daw = regDawArray.FirstOrDefault(d => d.IsRunning);
            if (daw is null)
            {
                client?.ClearPresence();
                client?.Dispose();
                startTime = null;
                Console.WriteLine("No DAW is running");
                return;
            }

            startTime ??= DateTime.UtcNow;

            Console.WriteLine("Detected: " + daw.DisplayName);
            Console.WriteLine("Project: " + daw.GetProjectNameFromProcessWindow());

            if (client is null || client.ApplicationID != daw.ApplicationId)
            {
                client?.ClearPresence();
                client?.Dispose();
                client = new DiscordRpcClient(daw.ApplicationId);
                client.Initialize();
            }

            client.SetPresence(new RichPresence
            {
                Details = daw.GetProjectNameFromProcessWindow() != ""
                    ? _configuration.WorkingPrefixText + daw.GetProjectNameFromProcessWindow()
                    : _configuration.IdleText,
                State = "",
                Assets = new Assets
                {
                    LargeImageKey = _configuration.UseCustomImage
                        ? _configuration.CustomImageKey
                        : daw.ImageKey,
                    LargeImageText = CREDIT
                },
                Timestamps = new Timestamps
                {
                    Start = startTime?.Add(-_configuration.Offset)
                }
            });

            await Task.Delay(_configuration.UpdateInterval);
        }
    }
}