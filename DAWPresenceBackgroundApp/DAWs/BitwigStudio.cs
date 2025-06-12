using System.Diagnostics;

namespace DAWPresence.DAWs;

public class BitwigStudio : Daw
{
	public BitwigStudio()
	{
		ProcessName = "Bitwig Studio";
		DisplayName = "Bitwig Studio - ";
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
		return title.Contains(WindowTrim)
			? title[..^TitleOffset]
			: "";
	}
}