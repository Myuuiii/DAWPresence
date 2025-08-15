namespace DAWPresence.DAWs;

public class Reason : Daw
{
    public Reason()
    {
        ProcessName = "Reason";
        DisplayName = "Reason";
        ImageKey = "reason";
        ApplicationId = "1397542364360474694";
        WindowTrim = "";
        TitleOffset = 0;
    }

    public override string GetProjectNameFromProcessWindow()
    {
        var process = GetProcess();
        if (process is null) return "";
        var title = process.MainWindowTitle;
        // Only trim ".reason" if present in the window title
        var extIndex = title.IndexOf(".reason", StringComparison.OrdinalIgnoreCase);
        if (extIndex > 0)
            return title.Substring(0, extIndex);
        return title;
    }
}