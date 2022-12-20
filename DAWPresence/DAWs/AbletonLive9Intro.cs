using System.Diagnostics;

namespace DAWPresence.DAWs;

public class AbletonLive9Intro : DAW
{
	public AbletonLive9Intro()
	{
		ProcessName = "Ableton Live 9 Intro";
		DisplayName = "Ableton Live 9 Intro";
		ImageKey = "icon";
		ApplicationId = "";
		WindowTrim = " - " + DisplayName;
		TitleOffset = 23;
	}

	public override string GetProjectNameFromProcessWindow()
	{
		Process? process = GetProcess();
		if (process is null) return "";
		string title = process.MainWindowTitle;
		return title.Contains(WindowTrim) ? title[..^TitleOffset] : "";
	}
}