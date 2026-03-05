namespace DAWPresence.DAWs;

public class AbletonLive11Suite : Daw
{
    public AbletonLive11Suite()
    {
        ProcessName = "Ableton Live 11 Suite";
        DisplayName = ProcessName;
        ImageKey = "ableton";
        ApplicationId = "1053952444859686983";
        WindowTrim = " - " + DisplayName;
        TitleOffset = 24;
    }

    public override string GetProjectNameFromProcessWindow()
    {
        var process = GetProcess();
        if (process is null) return "";
        var title = process.MainWindowTitle;
        return title.Contains(WindowTrim)
            ? title[..^TitleOffset]
            : "";
    }
}
