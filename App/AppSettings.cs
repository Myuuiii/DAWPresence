namespace DAWPresence;

public class AppSettings
{
    /// <summary>
    ///     Interval at which the app will check if the DAW is (still) running and update the presence accordingly.
    /// </summary>
    public TimeSpan UpdateInterval { get; set; } = new(0, 0, 3);

    /// <summary>
    ///     Time offset to add to the current time when displaying the elapsed time.
    /// </summary>
    public TimeSpan Offset { get; set; } = new(0, 0, 0);

    /// <summary>
    ///     Text to show when no project is open.
    /// </summary>
    public string IdleText { get; set; } = "Not working on a project";

    /// <summary>
    ///     Text to show before the project name when a project is open.
    /// </summary>
    public string WorkingPrefixText { get; set; } = "Working on ";

    /// <summary>
    ///     Overwrite the image key (for custom images).
    /// </summary>
    public bool UseCustomImage { get; set; } = false;

    /// <summary>
    ///     Custom image key to use.
    /// </summary>
    public string CustomImageKey { get; set; } = "custom";

    /// <summary>
    ///     Enable debug logging.
    /// </summary>
    public bool Debug { get; set; } = false;

    /// <summary>
    ///     Disable the popup when the app starts.
    /// </summary>
    public bool DisablePopup { get; set; } = false;

    /// <summary>
    ///     Check for updates on startup.
    /// </summary>
    public bool CheckForUpdates { get; set; } = true;

    /// <summary>
    ///     Whether the app should add itself to Windows startup.
    /// </summary>
    public bool OpenOnStartup { get; set; } = true;

    /// <summary>
    ///     Enable secret mode to hide project details.
    /// </summary>
    public bool SecretMode { get; set; } = false;

    /// <summary>
    ///     Text to show when secret mode is enabled.
    /// </summary>
    public string SecretModeText { get; set; } = "Working on a secret project";
}