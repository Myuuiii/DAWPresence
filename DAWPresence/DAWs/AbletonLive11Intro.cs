using System.Diagnostics;

namespace DAWPresence.DAWs;

public class AbletonLive11Intro : DAW
{
	public AbletonLive11Intro()
	{
		ProcessName = "Ableton Live 11 Intro";
		DisplayName = "Ableton Live 11 Intro";
		ImageKey = "icon";
		ApplicationId = "";
		WindowTrim = " - " + DisplayName;
		TitleOffset = 24;
	}

	public override string GetProjectNameFromProcessWindow()
	{
		Process? process = GetProcess();
		if (process is null) return "";
		string title = process.MainWindowTitle;
		return title.Contains(WindowTrim) ? title[..^TitleOffset] : "";
	}
}