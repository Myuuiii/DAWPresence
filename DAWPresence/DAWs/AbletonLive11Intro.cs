using System.Diagnostics;

namespace DAWPresence.DAWs;

public class AbletonLive11Intro : Daw
{
	public AbletonLive11Intro()
	{
		ProcessName = "Ableton Live 11 Intro";
		DisplayName = ProcessName;
		ImageKey = "ableton-white";
		ApplicationId = "1053952444859686983";
		WindowTrim = " - " + DisplayName;
		TitleOffset = 24;
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