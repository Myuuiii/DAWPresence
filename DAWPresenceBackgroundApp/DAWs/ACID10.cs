namespace DAWPresence.DAWs;

public class Acid10 : Daw
{
    public Acid10()
    {
        ProcessName = "musicstudio100";
        DisplayName = "Acid Music Studio 10";
        ImageKey = "Acid";
        ApplicationId = "1053779878916395048";
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