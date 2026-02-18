using System.Diagnostics;
using YamlDotNet.Serialization;

namespace DAWPresence.Services;

public static class SettingsManager
{
    private static readonly string SettingsDirectory = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
        Constants.SETTINGS_DIRECTORY_NAME);

    public static readonly string SettingsFilePath = Path.Combine(SettingsDirectory, Constants.SETTINGS_FILENAME);

    public static AppSettings Settings { get; private set; } = new();

    public static void LoadSettings()
    {
        if (!Directory.Exists(SettingsDirectory))
        {
            Directory.CreateDirectory(SettingsDirectory);
        }

        var deserializer = new DeserializerBuilder().IgnoreUnmatchedProperties().Build();
        var loadedSettings = new AppSettings();

        if (File.Exists(SettingsFilePath))
        {
            try
            {
                loadedSettings = deserializer.Deserialize<AppSettings>(File.ReadAllText(SettingsFilePath));
                Console.WriteLine($"Settings loaded from: {SettingsFilePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading settings, using defaults: {ex.Message}");
            }
        }

        var defaultSettings = new AppSettings();
        foreach (var prop in typeof(AppSettings).GetProperties())
        {
            var loadedValue = prop.GetValue(loadedSettings, null);
            if (loadedValue == null ||
                (prop.PropertyType == typeof(string) && string.IsNullOrEmpty((string)loadedValue)))
            {
                prop.SetValue(loadedSettings, prop.GetValue(defaultSettings, null));
            }
        }

        Settings = loadedSettings;

        var serializer = new SerializerBuilder().Build();
        File.WriteAllText(SettingsFilePath, serializer.Serialize(Settings));
    }

    public static void SaveSettings()
    {
        var serializer = new SerializerBuilder().Build();
        File.WriteAllText(SettingsFilePath, serializer.Serialize(Settings));
    }

    public static void SetStartup(bool enable)
    {
        var exePath = Process.GetCurrentProcess().MainModule?.FileName ?? string.Empty;
        StartupManager.SetStartup(Constants.APP_NAME, enable ? exePath : null, enable);
        Settings.OpenOnStartup = enable;
        SaveSettings();
    }
}