using DAWPresence;
using YamlDotNet.Serialization;

namespace DAWPresenceBackgroundApp;

public class ConfigurationManager
{
    private const string ConfigFilePath = "./config.yml";
    public static AppConfiguration Configuration { get; private set; } = new();
    
    public static void LoadConfiguration()
    {
        if (!Directory.Exists(Path.GetDirectoryName(ConfigFilePath)))
            Directory.CreateDirectory(Path.GetDirectoryName(ConfigFilePath));

        if (File.Exists(ConfigFilePath))
        {
            Configuration = new Deserializer().Deserialize<AppConfiguration>(File.ReadAllText(ConfigFilePath));
            Console.WriteLine("Configuration Loaded");
        }
        else
        {
            Configuration = new AppConfiguration();
            File.WriteAllText(ConfigFilePath, new SerializerBuilder().Build().Serialize(Configuration));
            Console.WriteLine("Configuration Created");
        }
    }
}