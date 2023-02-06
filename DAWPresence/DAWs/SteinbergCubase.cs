using System.Diagnostics;
using System.Text.RegularExpressions;

namespace DAWPresence.DAWs;

public partial class SteinbergCubase : Daw
{
	public SteinbergCubase()
	{
		ProcessName = "Cubase";
		DisplayName = ProcessName;
		ImageKey = "cubase";
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