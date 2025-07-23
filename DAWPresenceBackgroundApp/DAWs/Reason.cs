using System.Diagnostics;

namespace DAWPresence.DAWs;

public class Reason: Daw
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
        Process? process = GetProcess();
        if (process is null) return "";
        var title = process.MainWindowTitle;
        var extIndex = title.LastIndexOf(".reason", StringComparison.OrdinalIgnoreCase);
        return extIndex > 0 ? title.Substring(0, extIndex) : title;
    }
}