using DAWPresence;

IHost host = Host.CreateDefaultBuilder(args)
	.UseWindowsService(options => { options.ServiceName = "DAW Rich Presence"; })
	.ConfigureServices(services => { services.AddHostedService<Worker>(); })
	.Build();

host.Run();