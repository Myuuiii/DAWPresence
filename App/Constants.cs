namespace DAWPresence;

/* Some things to note:
 * Use verbatim strings (@ for paths)
 * Any file that gets copied over to be included in the installer MUST also be changed in Package.wxs
*/

internal static class Constants
{
    public const string APP_NAME = "DAWPresence";
    public const string APP_CREDITS = "DAWPresence by @myuuiii";
    public const string APP_VERSION_PATH = @"App\version.txt";

    public const string GITHUB_VERSION_URL = "https://raw.githubusercontent.com/Myuuiii/DAWPresence/master/App/version.txt";
    public const string GITHUB_PAGE_URL = "https://github.com/Myuuiii/DAWPresence";

    public const string SETTINGS_DIRECTORY_NAME = "DAWPresence";
    public const string SETTINGS_FILENAME = "settings.yml";

    public const string STARTUP_REGISTRY_PATH = "Software\\Microsoft\\Windows\\CurrentVersion\\Run";
}