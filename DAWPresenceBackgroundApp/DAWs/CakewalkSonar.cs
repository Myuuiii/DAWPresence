namespace DAWPresence.DAWs;

public class CakewalkSonar : Daw
{
    public CakewalkSonar()
    {
        ProcessName = "Cakewalk Sonar";
        DisplayName = "Cakewalk Sonar - ";
        ImageKey = "icon";
        ApplicationId = "";
        WindowTrim = DisplayName + " - ";
        TitleOffset = 17;
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