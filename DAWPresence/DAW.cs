using System.Diagnostics;

namespace DAWPresence;

public abstract class DAW
{
	public string DisplayName { get; protected init; }
	public string ProcessName { get; protected init; }
	public string WindowTrim { get; protected init; }
	public int TitleOffset { get; protected init; }
	public string ImageKey { get; protected init; }
	public string ApplicationId { get; protected init; }

	public int ProcessCount => Process.GetProcessesByName(ProcessName).Length;
	public bool IsRunning => ProcessCount > 0;

	public abstract string GetProjectNameFromProcessWindow();

	protected Process GetProcess() => Process.GetProcessesByName(ProcessName).First();
}