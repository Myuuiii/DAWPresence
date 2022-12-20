using System.Diagnostics;

namespace DAWPresence.DAWs;

public class AbletonLive9Suite : Daw
{
	public AbletonLive9Suite()
	{
		ProcessName = "Ableton Live 9 Suite";
		DisplayName = ProcessName;
		ImageKey = "ableton-white";
		ApplicationId = "1053952444859686983";
		WindowTrim = " - " + DisplayName;
		TitleOffset = 23;
	}

	public override string GetProjectNameFromProcessWindow()
	{
		Process? process = GetProcess();
		if (process is null) return "";
		string title = process.MainWindowTitle;
		return title.Contains(WindowTrim)
			? title[..^TitleOffset]
			: "";
	}
}