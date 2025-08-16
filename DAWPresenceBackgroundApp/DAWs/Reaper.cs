namespace DAWPresence.DAWs;

public class Reaper : Daw
{
    public Reaper()
    {
        ProcessName = "reaper";
        DisplayName = "Reaper";
        ImageKey = "reaper";
        ApplicationId = "1082950869856821308";
        WindowTrim = " - REAPER v";
        TitleOffset = 0;
    }

    public override string GetProjectNameFromProcessWindow()
    {
        var process = GetProcess();
        if (process is null) return "";
        var title = process.MainWindowTitle;
        return title.Contains(WindowTrim)
            ? title[..^title.IndexOf(WindowTrim, StringComparison.Ordinal)]
            : "";
    }
}