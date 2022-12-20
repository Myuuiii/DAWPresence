using System.Diagnostics;

namespace DAWPresence.DAWs;

public class AbletonLive9Standard : Daw
{
	public AbletonLive9Standard()
	{
		ProcessName = "Ableton Live 9 Standard";
		DisplayName = ProcessName;
		ImageKey = "ableton-white";
		ApplicationId = "1053952444859686983";
		WindowTrim = " - " + DisplayName;
		TitleOffset = 26;
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