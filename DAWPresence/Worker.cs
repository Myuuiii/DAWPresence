using System.Reflection;
using DiscordRPC;
using YamlDotNet.Serialization;

namespace DAWPresence;

public class Worker : BackgroundService
{
	private const string CONFIG_FILE_NAME = "config.yml";
	private readonly ILogger<Worker> _logger;
	private AppConfiguration _configuration;
	private DiscordRpcClient? client;
	private static DateTime? startTime;

	private const string CREDIT = "DAWPresence by @Myuuiii#0001";

	public Worker(ILogger<Worker> logger)
	{
		_logger = logger;
	}

	protected override async Task ExecuteAsync(CancellationToken stoppingToken)
	{
		if (File.Exists(CONFIG_FILE_NAME))
		{
			_logger.LogInformation("Loading configuration file...");
			_configuration =
				new Deserializer().Deserialize<AppConfiguration>(
					await File.ReadAllTextAsync(CONFIG_FILE_NAME, stoppingToken));
		}
		else
		{
			_configuration = new AppConfiguration();
			_logger.LogInformation("Creating configuration file...");
			await File.WriteAllTextAsync(CONFIG_FILE_NAME,
				new SerializerBuilder().Build().Serialize(_configuration), stoppingToken);
		}

		IEnumerable<DAW?> registeredDaws = Assembly.GetExecutingAssembly().GetTypes()
			.Where(t => t.IsSubclassOf(typeof(DAW)))
			.Select(t => (DAW?)Activator.CreateInstance(t));

		IEnumerable<DAW?> regDawArray = registeredDaws as DAW[] ?? registeredDaws.ToArray();

		foreach (DAW? r in regDawArray)
			Console.WriteLine($"{r.DisplayName} has been registered");

		while (!stoppingToken.IsCancellationRequested)
		{
			DAW? daw = regDawArray.FirstOrDefault(d => d.IsRunning);
			Console.Clear();
			if (daw is null)
			{
				client?.ClearPresence();
				client?.Dispose();
				Console.WriteLine("No DAW is running");
				return;
			}

			Console.WriteLine("Detected: " + daw.DisplayName);
			Console.WriteLine("Project: " + daw.GetProjectNameFromProcessWindow());

			if (client is null || client.ApplicationID != daw.ApplicationId)
			{
				client?.ClearPresence();
				client?.Dispose();
				client = new DiscordRpcClient(daw.ApplicationId);
				client.Initialize();
			}

			client.SetPresence(new RichPresence()
			{
				Details = daw.GetProjectNameFromProcessWindow() != ""
					? _configuration.WorkingPrefixText + daw.GetProjectNameFromProcessWindow()
					: _configuration.IdleText,
				State = "",
				Assets = new Assets
				{
					LargeImageKey = _configuration.UseCustomImage ? _configuration.CustomImageKey : daw.ImageKey,
					LargeImageText = CREDIT
				},
				Timestamps = new Timestamps()
				{
					Start = DateTime.UtcNow.Add(-_configuration.Offset)
				}
			});

			await Task.Delay(_configuration.UpdateInterval, stoppingToken);
		}
	}
}