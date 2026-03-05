using System.Diagnostics;

namespace DAWPresence;

public abstract class Daw
{
    /// <summary>
    ///     Pretty name that will be shown in the presence
    /// </summary>
    public required string DisplayName { get; init; }

    /// <summary>
    ///     Name of the DAW's process as seen in Task Manager on Windows
    /// </summary>
    public required string ProcessName { get; init; }

    /// <summary>
    ///     The text that needs to be trimmed from the window title in order to get the project name
    /// </summary>
    public required string WindowTrim { get; init; }

    /// <summary>
    ///     The number of characters that should be trimmed to get the project name
    /// </summary>
    public int TitleOffset { get; protected init; }

    /// <summary>
    ///     Discord Rich Presence image key
    /// </summary>
    public required string ImageKey { get; init; }

    /// <summary>
    ///     Discord Rich Presence application ID
    /// </summary>
    public required string ApplicationId { get; init; }

    /// <summary>
    ///     If true, hides the details field in Discord Rich Presence
    /// </summary>
    public bool HideDetails { get; protected init; } = false;

    /// <summary>
    ///     Returns whether there is a running instance of the DAW
    /// </summary>
    public bool IsRunning => Process.GetProcessesByName(ProcessName).Length > 0;

    /// <summary>
    ///     Retrieves the name of the currently open project, or an empty string if no project is open
    /// </summary>
    public abstract string GetProjectNameFromProcessWindow();

    /// <summary>
    ///     Gets the first running process matching the DAW's process name
    /// </summary>
    protected Process GetProcess()
    {
        return Process.GetProcessesByName(ProcessName).First();
    }
}