using System.Diagnostics;

namespace DAWPresence.DAWs;

public class AbletonLive11Suite : DAW
{
	public AbletonLive11Suite()
	{
		ProcessName = "Ableton Live 11 Suite";
		DisplayName = "Ableton Live 11 Suite";
		ImageKey = "icon";
		ApplicationId = "";
		WindowTrim = " - " + DisplayName;
		TitleOffset = 16;
	}

	public override string GetProjectNameFromProcessWindow()
	{
		Process? process = GetProcess();
		if (process is null) return "";
		string title = process.MainWindowTitle;
		return title.Contains(WindowTrim) ? title[..^TitleOffset] : "";
	}
}