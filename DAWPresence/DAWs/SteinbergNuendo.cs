using System.Diagnostics;
using static System.Diagnostics.Process;

namespace DAWPresence.DAWs;

public partial class SteinbergNuendo : Daw
{
	public SteinbergNuendo()
	{
		ProcessName = "Nuendo";
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

	protected override Process GetProcess() => GetProcesses().First(x => x.ProcessName.StartsWith(ProcessName));
}