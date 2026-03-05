using Microsoft.Win32;

namespace DAWPresence.Services;

public static class StartupManager
{
    /// <summary>
    ///     Adds or removes this application from Windows startup for the current user.
    /// </summary>
    /// <param name="appName">The name to use for the registry entry.</param>
    /// <param name="exePath">The full path to the executable (for add).</param>
    /// <param name="add">True to add to startup, false to remove.</param>
    public static void SetStartup(string appName, string? exePath, bool add)
    {
        try
        {
            using var key = OpenStartupRegistryKey();
            if (key == null)
            {
                Console.WriteLine("Failed to open registry key for startup.");
                return;
            }

            if (add)
            {
                if (string.IsNullOrWhiteSpace(exePath))
                {
                    Console.WriteLine("Executable path is null or empty. Cannot add to startup.");
                    return;
                }

                key.SetValue(appName, $"\"{exePath}\"");
                Console.WriteLine($"Added {appName} to startup with path: {exePath}");
            }
            else
            {
                key.DeleteValue(appName, false);
                Console.WriteLine($"Removed {appName} from startup.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error modifying startup: {ex.Message}");
        }
    }

    /// <summary>
    ///     Opens the registry key for Windows startup (CurrentUser).
    /// </summary>
    /// <returns>The registry key, or null if it cannot be opened.</returns>
    private static RegistryKey? OpenStartupRegistryKey()
    {
        return Registry.CurrentUser.OpenSubKey(Constants.STARTUP_REGISTRY_PATH, true);
    }
}