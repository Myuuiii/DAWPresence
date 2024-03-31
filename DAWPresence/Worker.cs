using System.Net;
using System.Reflection;
using DiscordRPC;
using Spectre.Console;
using YamlDotNet.Serialization;

namespace DAWPresence;

internal sealed class Worker : BackgroundService
{
    private const string CONFIG_FILE_NAME = "config.yml";

    private const string CREDIT = "DAWPresence by @Myuuiii#0001";
    private readonly AppConfiguration _configuration;
    private DiscordRpcClient? client;
    private DateTime? startTime;

    private const string RELEASE_VERSION = "debug-0.1.4";

    public Worker(ILogger<Worker> logger)
    {
        VersionCheck();

        if (File.Exists(CONFIG_FILE_NAME))
        {
            AnsiConsole.MarkupLine(UI.Messages.LoadedConfig);
            _configuration =
                new Deserializer().Deserialize<AppConfiguration>(
                    File.ReadAllText(CONFIG_FILE_NAME));
        }
        else
        {
            _configuration = new AppConfiguration();
            AnsiConsole.MarkupLine(UI.Messages.CreatingConfig);
            File.WriteAllText(CONFIG_FILE_NAME,
                new SerializerBuilder().Build().Serialize(_configuration));
        }
    }

    private static void VersionCheck()
    {
        if (!RELEASE_VERSION.StartsWith("beta") && !RELEASE_VERSION.Contains("debug"))
        {
            using (WebClient client = new())
            {
                try
                {
                    var webLatest = client.DownloadString(
                        "https://minio.myuuiii.com/mversion/dawpresence.txt");
                    if (webLatest != RELEASE_VERSION)
                    {
                        AnsiConsole.MarkupLine(string.Format(UI.Messages.f_NewVersionAvailable, RELEASE_VERSION,
                            webLatest));
                    }
                }
                catch (Exception e)
                {
                    AnsiConsole.MarkupLine(UI.Messages.FailedToCheckForUpdates);
                }
            }
        }
        if (RELEASE_VERSION.StartsWith("beta"))
        {
            AnsiConsole.MarkupLine(UI.Messages.BetaReleaseWarning);
        }
        if (RELEASE_VERSION.Contains("debug"))
        {
            AnsiConsole.MarkupLine(UI.Messages.DebugReleaseWarning);
        }
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        IEnumerable<Daw?> registeredDaws = Assembly.GetExecutingAssembly().GetTypes()
            .Where(t => t.IsSubclassOf(typeof(Daw)))
            .Select(t => (Daw?)Activator.CreateInstance(t));
        IEnumerable<Daw?> regDawArray = registeredDaws as Daw[] ?? registeredDaws.ToArray();

        // foreach (Daw? r in regDawArray)
        // 	AnsiConsole.MarkupLine(string.Format(UI.Messages.f_DawLoaded, r?.DisplayName));

        while (!stoppingToken.IsCancellationRequested)
        {
            Daw? daw = regDawArray.FirstOrDefault(d => d.IsRunning);
            if (daw is null)
            {
                client?.ClearPresence();
                client?.Dispose();
                startTime = null;
                AnsiConsole.MarkupLine(UI.Messages.NoDawFound);
                return;
            }

            startTime ??= DateTime.UtcNow;

            AnsiConsole.MarkupLine(string.Format(UI.Messages.f_DawDetected, daw.DisplayName));
            AnsiConsole.MarkupLine(string.Format(UI.Messages.f_ProjectDetected, daw.GetProjectNameFromProcessWindow()));

            try
            {
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
            }
            catch
            {
                AnsiConsole.MarkupLine(UI.Messages.DiscordRpcError);
            }
            

            await Task.Delay(_configuration.UpdateInterval, stoppingToken);
        }
    }
}