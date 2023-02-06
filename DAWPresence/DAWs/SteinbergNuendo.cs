using System.Diagnostics;
using System.Text.RegularExpressions;

namespace DAWPresence.DAWs;

public partial class SteinbergNuendo : Daw
{
	public SteinbergNuendo()
	{
		ProcessName = "Nuendo 12";
		DisplayName = "Nuendo";
		ImageKey = "nuendo";
		ApplicationId = "1072197265587974144";
		WindowTrim = "Nuendo Project";
		TitleOffset = 17;
	}

	public override string GetProjectNameFromProcessWindow()
	{
		Process? process = GetProcess();
		if (process is null) return "";
		string title = process.MainWindowTitle;
		return title.Contains(WindowTrim)
			? title[TitleOffset..]
			: "";
	}
}