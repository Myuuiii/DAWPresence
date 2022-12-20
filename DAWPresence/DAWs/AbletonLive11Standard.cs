using System.Diagnostics;

namespace DAWPresence.DAWs;

public class AbletonLive11Standard : Daw
{
	public AbletonLive11Standard()
	{
		ProcessName = "Ableton Live 11 Standard";
		DisplayName = ProcessName;
		ImageKey = "ableton-white";
		ApplicationId = "1053952444859686983";
		WindowTrim = " - " + DisplayName;
		TitleOffset = 27;
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