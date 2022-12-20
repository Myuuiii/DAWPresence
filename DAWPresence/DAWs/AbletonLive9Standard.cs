using System.Diagnostics;

namespace DAWPresence.DAWs;

public class AbletonLive9Standard : DAW
{
	public AbletonLive9Standard()
	{
		ProcessName = "Ableton Live 9 Standard";
		DisplayName = "Ableton Live 9 Standard";
		ImageKey = "icon";
		ApplicationId = "";
		WindowTrim = " - " + DisplayName;
		TitleOffset = 26;
	}

	public override string GetProjectNameFromProcessWindow()
	{
		Process? process = GetProcess();
		if (process is null) return "";
		string title = process.MainWindowTitle;
		return title.Contains(WindowTrim) ? title[..^TitleOffset] : "";
	}
}