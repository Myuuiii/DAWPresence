namespace DAWPresence.DAWs;

public class Acid10 : Daw
{
    public Acid10()
    {
        ProcessName = "musicstudio100";
        DisplayName = "Acid Music Studio 10";
        ImageKey = "acid10";
        ApplicationId = "1479379779911286908";
        WindowTrim = " - " + DisplayName;
        TitleOffset = 15;
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