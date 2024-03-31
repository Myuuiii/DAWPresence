using System.Reflection;
using DiscordRPC;
using YamlDotNet.Serialization;

namespace DAWPresence;

internal sealed class Worker : BackgroundService
{
    private const string CONFIG_FILE_NAME = "config.yml";
    private const string CREDIT = "DAWPresence by @Myuuiii#0001";

    private readonly ILogger<Worker> _logger;
    private readonly AppConfiguration _configuration;

    private DiscordRpcClient? _client;
    private DateTime? _startTime;

    public Worker(ILogger<Worker> logger)
    {
        _logger = logger;
        _configuration = File.Exists(CONFIG_FILE_NAME) ? LoadConfigurationFromFile() : CreateInitialConfiguration();
    }

    private AppConfiguration LoadConfigurationFromFile()
    {
        _logger.LogInformation("Loading configuration file...");
        return new Deserializer().Deserialize<AppConfiguration>(File.ReadAllText(CONFIG_FILE_NAME));
    }

    private AppConfiguration CreateInitialConfiguration()
    {
        AppConfiguration config = new();
        _logger.LogInformation("Creating configuration file...");
        File.WriteAllText(CONFIG_FILE_NAME, new SerializerBuilder().Build().Serialize(config));
        return config;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        IEnumerable<Daw> registeredDaws = GetRegisteredDaws();
        await ExecuteLoop(stoppingToken, registeredDaws);
    }

    private IEnumerable<Daw> GetRegisteredDaws()
    {
        IEnumerable<Daw?> dawTypes = Assembly.GetExecutingAssembly()
            .GetTypes()
            .Where(t => t.IsSubclassOf(typeof(Daw)))
            .Select(t => (Daw?) Activator.CreateInstance(t));

        return dawTypes as Daw[] ?? dawTypes.ToArray();
    }

    private async Task ExecuteLoop(CancellationToken stoppingToken, IEnumerable<Daw> registeredDaws)
    {
        foreach(Daw daw in registeredDaws)
            Console.WriteLine($"{daw.DisplayName} has been registered");

        while (!stoppingToken.IsCancellationRequested)
        {
            Daw? currentDaw = registeredDaws.FirstOrDefault(d => d.IsRunning);
            Console.Clear();

            if (currentDaw == null)
            {
                ResetClient();
                Console.WriteLine("No DAW is running");
                return;
            }

            _startTime ??= DateTime.UtcNow;
            UpdateClientForDaw(currentDaw);

            await Task.Delay(_configuration.UpdateInterval, stoppingToken);
        }
    }

    private void ResetClient() 
    {
        _client?.ClearPresence();
        _client?.Dispose();
        _startTime = null;
    }

    private void UpdateClientForDaw(Daw daw)
    {
        Console.WriteLine("Detected: " + daw.DisplayName);
        Console.WriteLine("Project: " + daw.GetProjectNameFromProcessWindow());

        if (_client == null || _client.ApplicationID != daw.ApplicationId)
        {
            ResetClient();
            _client = new DiscordRpcClient(daw.ApplicationId);
            _client.Initialize();
        }

        _client.SetPresence(new RichPresence
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
                Start = _startTime?.Add(-_configuration.Offset)
            }
        });
    }
}