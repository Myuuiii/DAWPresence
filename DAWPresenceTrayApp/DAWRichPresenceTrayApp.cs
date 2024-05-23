using System.Reflection;
using DAWPresence;
using DiscordRPC;
using YamlDotNet.Serialization;

namespace DAWPresenceTrayApp;

public partial class DAWRichPresenceTrayApp : Form
{
    // config in the appdata folder
    private const string CONFIG_FILE_NAME = "C:\\Users\\%USERNAME%\\AppData\\Roaming\\DAWPresence\\config.yml";

    private const string CREDIT = "DAWPresence by @Myuuiii#0001";
    private readonly AppConfiguration _configuration;
    private DiscordRpcClient? client;
    private DateTime? startTime;

    public DAWRichPresenceTrayApp()
    {
        InitializeComponent();

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

    protected async Task ExecuteTaskAsync()
    {
        IEnumerable<Daw?> registeredDaws = Assembly.GetExecutingAssembly().GetTypes()
            .Where(t => t.IsSubclassOf(typeof(Daw)))
            .Select(t => (Daw?)Activator.CreateInstance(t));
        IEnumerable<Daw?> regDawArray = registeredDaws as Daw[] ?? registeredDaws.ToArray();

        foreach (Daw? r in regDawArray)
            Console.WriteLine($"{r.DisplayName} has been registered");

        while (true)
        {
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