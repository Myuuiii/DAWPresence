using System.Diagnostics;

namespace DAWPresence.DAWs;

public class AbletonLive11Standard : DAW
{
	public AbletonLive11Standard()
	{
		ProcessName = "Ableton Live 11 Standard";
		DisplayName = "Ableton Live 11 Standard";
		ImageKey = "icon";
		ApplicationId = "";
		WindowTrim = " - " + DisplayName;
		TitleOffset = 27;
	}

	public override string GetProjectNameFromProcessWindow()
	{
		Process? process = GetProcess();
		if (process is null) return "";
		string title = process.MainWindowTitle;
		return title.Contains(WindowTrim) ? title[..^TitleOffset] : "";
	}
}