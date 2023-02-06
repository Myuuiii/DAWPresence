using System.Diagnostics;
using System.Text.RegularExpressions;

namespace DAWPresence.DAWs;

public partial class SteinbergNuendo : Daw
{
	public SteinbergNuendo()
	{
		ProcessName = "Nuendo";
		DisplayName = ProcessName;
		ImageKey = "nuendo";
		ApplicationId = "";
		WindowTrim = " - " + DisplayName;
		TitleOffset = 0;
	}

	public override string GetProjectNameFromProcessWindow()
	{
		Process? process = GetProcess();
		if (process is null) return "";
		string title = process.MainWindowTitle;
		return title.Contains(WindowTrim)
			? TitleRegex().Match(title[..^TitleOffset]).Value
			: "";
	}

	[GeneratedRegex("[^\\[]*")]
	private static partial Regex TitleRegex();
}