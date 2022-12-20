using System.Diagnostics;

namespace DAWPresence.DAWs;

public class AbletonLive9Suite : DAW
{
	public AbletonLive9Suite()
	{
		ProcessName = "Ableton Live 9 Suite";
		DisplayName = "Ableton Live 9 Suite";
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