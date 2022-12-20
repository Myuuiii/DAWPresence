using System.Diagnostics;

namespace DAWPresence.DAWs;

public class AbletonLive10Intro : DAW
{
	public AbletonLive10Intro()
	{
		ProcessName = "Ableton Live 10 Intro";
		DisplayName = "Ableton Live 10 Intro";
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