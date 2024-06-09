using DAWPresence;

HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);

// Require admin rights
// if (!new WindowsPrincipal(WindowsIdentity.GetCurrent()).IsInRole(WindowsBuiltInRole.Administrator))
// {
//     Console.WriteLine("Please run this program as an administrator");
//     Console.ReadKey();
//     return;
// }

builder.Services.AddHostedService<Worker>();

IHost host = builder.Build();
host.Run();