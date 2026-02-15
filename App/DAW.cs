using System.Diagnostics;

namespace DAWPresence;

public abstract class Daw
{
    /// <summary>
    ///     Pretty name that will be shown in the presence
    /// </summary>
    public required string DisplayName { get; init; }

    /// <summary>
    ///     Name of the DAWs process as seen in Task Manager on Windows
    /// </summary>
    public required string ProcessName { get; init; }

    /// <summary>
    ///     The text that needs to be trimmed from the Window title in order to get the project name (if it works that way for
    ///     the DAW)
    /// </summary>
    public required string WindowTrim { get; init; }

    /// <summary>
    ///     The amount of characters that should be trimmed to get the project name
    /// </summary>
    public int TitleOffset { get; protected init; }

    /// <summary>
    ///     Discord Rich Presence Image Key
    /// </summary>
    public required string ImageKey { get; init; }

    /// <summary>
    ///     Discord Rich Presence Application Id
    /// </summary>
    public required string ApplicationId { get; init; }

    /// <summary>
    ///     If true, hides the details field in Discord Rich Presence
    /// </summary>
    public bool HideDetails { get; protected init; } = false;

    /// <summary>
    ///     Return the amount of processes with the name of the DAW
    /// </summary>
    public int ProcessCount => Process.GetProcessesByName(ProcessName).Length;

    /// <summary>
    ///     Returns whether there is a running instance of the DAW or not
    /// </summary>
    public bool IsRunning => ProcessCount > 0;

    /// <summary>
    ///     Retrieves the name of the currently open project or an empty string if no project is open
    /// </summary>
    /// <returns></returns>
    public abstract string GetProjectNameFromProcessWindow();

    /// <summary>
    ///     Get the first process with the name of the DAW
    /// </summary>
    /// <returns></returns>
    protected Process GetProcess()
    {
        return Process.GetProcessesByName(ProcessName).First();
    }
}