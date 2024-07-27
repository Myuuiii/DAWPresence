using System.Diagnostics;

namespace DAWPresence.DAWs;

public class FLStudio : Daw
{
	public FLStudio()
	{
		ProcessName = "FL";
		DisplayName = "FL Studio";
		ImageKey = "fl";
		ApplicationId = "1053779878916395048";
		WindowTrim = " - " + DisplayName;
		TitleOffset = 15;
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