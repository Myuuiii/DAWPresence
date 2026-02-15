using System.Diagnostics;
using DAWPresence;
using YamlDotNet.Serialization;

namespace DAWPresenceBackgroundApp;

public class ConfigurationManager
{
    private static readonly string ConfigDirectory =
        Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "DAWPresence");

    public static readonly string ConfigFilePath = Path.Combine(ConfigDirectory, "config.yml");
    public static AppConfiguration Configuration { get; private set; } = new();

    public static void LoadConfiguration()
    {
        if (!Directory.Exists(ConfigDirectory))
            Directory.CreateDirectory(ConfigDirectory);

        var serializer = new SerializerBuilder().Build();
        var deserializer = new DeserializerBuilder().IgnoreUnmatchedProperties().Build();
        var loadedConfig = new AppConfiguration();

        if (File.Exists(ConfigFilePath))
            try
            {
                loadedConfig = deserializer.Deserialize<AppConfiguration>(File.ReadAllText(ConfigFilePath));
                Console.WriteLine($"Configuration Loaded from {ConfigFilePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading config, using defaults: {ex.Message}");
            }

        var defaultConfig = new AppConfiguration();
        foreach (var prop in typeof(AppConfiguration).GetProperties())
        {
            var loadedValue = prop.GetValue(loadedConfig, null);
            if (loadedValue == null ||
                (prop.PropertyType == typeof(string) && string.IsNullOrEmpty((string)loadedValue)))
                prop.SetValue(loadedConfig, prop.GetValue(defaultConfig, null));
        }

        Configuration = loadedConfig;
        File.WriteAllText(ConfigFilePath, serializer.Serialize(Configuration));
    }

    public static void SaveConfiguration()
    {
        var serializer = new SerializerBuilder().Build();
        File.WriteAllText(ConfigFilePath, serializer.Serialize(Configuration));
    }

    public static void SetStartup(bool enable)
    {
        var exePath = Process.GetCurrentProcess().MainModule?.FileName ?? string.Empty;
        ProcessCode.SetStartup("DAWPresence", enable ? exePath : null, enable);
        Configuration.OpenOnStartup = enable;
        SaveConfiguration();
    }
}