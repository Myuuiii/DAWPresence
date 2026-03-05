namespace DAWPresence.DAWs;

public class StudioOne : Daw
{
    public StudioOne()
    {
        ProcessName = "Studio One";
        DisplayName = "Studio One";
        ImageKey = "studio-one";
        ApplicationId = "1222887221577781278";
        WindowTrim = DisplayName + " - ";
        TitleOffset = 13;
    }

    public override string GetProjectNameFromProcessWindow()
    {
        var process = GetProcess();
        if (process is null) return "";
        var title = process.MainWindowTitle;
        return title.StartsWith(WindowTrim)
            ? title[WindowTrim.Length..]
            : "";
        ;
    }
}