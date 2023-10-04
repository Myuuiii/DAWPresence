using DAWPresence.Common;
using DiscordRPC;
using YamlDotNet.Serialization;

namespace DAWPresence.TrayApp
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new DawPresenceApplicationContext());
        }
    }

    public class DawPresenceApplicationContext : ApplicationContext
    {
        private readonly NotifyIcon _trayIcon;
        private DiscordRpcClient _client;
        private DateTime? _startTime;
        private CancellationTokenSource _tokenSource;
        private AppConfiguration _configuration;

        public DawPresenceApplicationContext()
        {
            // Initialize Tray Icon
            try
            {
                if (new UpdateChecker().IsUpdateAvailable().Result)
                {
                    MessageBox.Show("An update is available. Please download the latest version from GitHub",
                        "DAWPresence", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            catch
            {
                MessageBox.Show("An error occurred while checking for updates.",
                    "DAWPresence", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
            string executingPath = System.Reflection.Assembly.GetExecutingAssembly().Location;
            _trayIcon = new NotifyIcon()
            {
                Icon = new Icon(executingPath[..executingPath.LastIndexOf('\\')] + "\\appicon.ico"),
                Visible = true
            };

            _trayIcon.DoubleClick += TrayIcon_DoubleClick;
            InitializeDiscordRpc();
            
            
            MessageBox.Show("DAWPresence started. Double click the tray icon to exit.", "DAWPresence", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void InitializeDiscordRpc()
        {
            // Load configuration from the file
            string configFile = "config.yml";

            if (File.Exists(configFile))
            {
                Deserializer deserializer = new();
                _configuration = deserializer.Deserialize<AppConfiguration>(File.ReadAllText(configFile));
            }
            else
            {
                _configuration = new AppConfiguration();
                ISerializer serializer = new SerializerBuilder().Build();
                File.WriteAllText(configFile, serializer.Serialize(_configuration));
            }

            // Start a background task to update presence
            _tokenSource = new CancellationTokenSource();
            Task.Run(() => UpdatePresence(_tokenSource.Token));
        }

        private async Task UpdatePresence(CancellationToken cancellationToken)
        {
            IEnumerable<Daw?> registeredDaws = typeof(Daw).Assembly.GetTypes()
                .Where(t => t.IsSubclassOf(typeof(Daw)))
                .Select(t => (Daw?)Activator.CreateInstance(t));
            
            IEnumerable<Daw?> regDawArray = registeredDaws as Daw[] ?? registeredDaws.ToArray();
            Daw? previousDaw = null; // Track the previously detected DAW

            while (!cancellationToken.IsCancellationRequested)
            {
                Console.WriteLine("Updating presence...");
                Daw? currentDaw = regDawArray.FirstOrDefault(d => d.IsRunning);

                if (currentDaw != previousDaw)
                {
                    // Only create a new client if the DAW has changed
                    if (currentDaw is null)
                    {
                        _client?.ClearPresence();
                        _startTime = null;
                    }
                    else
                    {
                        _startTime ??= DateTime.UtcNow;
                        _client?.Dispose(); // Dispose of the previous client if it exists
                        _client = new DiscordRpcClient(currentDaw.ApplicationId);
                        _client.Initialize();
                    }

                    previousDaw = currentDaw; // Update the previous DAW
                }

                _client?.SetPresence(new RichPresence
                {
                    Details = currentDaw.GetProjectNameFromProcessWindow() != ""
                        ? _configuration.WorkingPrefixText + currentDaw.GetProjectNameFromProcessWindow()
                        : _configuration.IdleText,
                    State = "",
                    Assets = new Assets
                    {
                        LargeImageKey = _configuration.UseCustomImage
                            ? _configuration.CustomImageKey
                            : currentDaw.ImageKey,
                        LargeImageText = "Made by Myuuiii"
                    },
                    Timestamps = new Timestamps
                    {
                        Start = _startTime?.Add(-_configuration.Offset)
                    }
                });

                await Task.Delay(_configuration.UpdateInterval, cancellationToken);
            }
        }

        private void TrayIcon_DoubleClick(object sender, EventArgs e)
        {
            Exit();
        }

        private void Exit()
        {
            try
            {
                _client.Dispose();
            }
            catch
            {
                // ignored
            }

            _trayIcon.Visible = false;
            _tokenSource.Cancel();
            Application.Exit();
        }
    }
}