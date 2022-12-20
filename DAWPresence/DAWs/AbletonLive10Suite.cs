using System.Diagnostics;

namespace DAWPresence.DAWs;

public class AbletonLive10Suite : DAW
{
	public AbletonLive10Suite()
	{
		ProcessName = "Ableton Live 10 Suite";
		DisplayName = "Ableton Live 10 Suite";
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