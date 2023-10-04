namespace DAWPresence.TrayApp;

public class UpdateChecker
{
    private const string CurrentVersion = "1.0";
    
    private const string LatestVersionUrl = "https://minio.myuuiii.com/myuuiii/daw-presence/latest-version.txt";
    
    private readonly HttpClient _client;

    public UpdateChecker()
    {
        _client = new HttpClient();
    }
    
    public async Task<bool> IsUpdateAvailable()
    {
        string? latestVersion = await _client.GetStringAsync(LatestVersionUrl);
        Console.WriteLine($"Latest version: {latestVersion}");
        Console.WriteLine($"Current version: {CurrentVersion}");
        return latestVersion != CurrentVersion;
    }
}