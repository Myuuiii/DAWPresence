using DAWPresence;

IHost host = Host.CreateDefaultBuilder(args)
	.UseWindowsService(options => { options.ServiceName = "DAW Rich Presence"; })
	.ConfigureLogging(logging => { logging.ClearProviders(); })
	.ConfigureServices(services => { services.AddHostedService<Worker>(); })
	.Build();

host.Run();