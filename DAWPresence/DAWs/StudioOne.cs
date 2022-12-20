using System.Diagnostics;

namespace DAWPresence.DAWs;

public class StudioOne : DAW
{
	public StudioOne()
	{
		ProcessName = "Studio One";
		DisplayName = "Studio One";
		ImageKey = "icon";
		ApplicationId = "";
		WindowTrim = " - " + DisplayName;
		;
		TitleOffset = 13;
	}

	public override string GetProjectNameFromProcessWindow()
	{
		Process? process = GetProcess();
		if (process is null) return "";
		string title = process.MainWindowTitle;
		return title.Contains(WindowTrim) ? title[..^TitleOffset] : "";
	}
}