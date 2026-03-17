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
                var settingsContent = File.ReadAllText(SettingsFilePath);
                if (string.IsNullOrWhiteSpace(settingsContent))
                {
                    Console.WriteLine("Settings file is empty, recreating defaults.");
                    BackupSettingsFileAsError();
                }
                else
                {
                    loadedSettings = deserializer.Deserialize<AppSettings>(settingsContent) ?? throw new InvalidDataException("Settings file parsed to null.");
                    Console.WriteLine($"Settings loaded from: {SettingsFilePath}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading settings, recreating defaults: {ex.Message}");
                BackupSettingsFileAsError();
                loadedSettings = new AppSettings();
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

    private static void BackupSettingsFileAsError()
    {
        if (!File.Exists(SettingsFilePath))
        {
            return;
        }

        var errorFilePath = $"{SettingsFilePath}.error";
        if (File.Exists(errorFilePath))
        {
            errorFilePath = $"{SettingsFilePath}.{DateTime.UtcNow:yyyyMMddHHmmss}.error";
        }

        File.Move(SettingsFilePath, errorFilePath);
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