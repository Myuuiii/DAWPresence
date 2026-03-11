using System.Diagnostics.CodeAnalysis;

namespace DAWPresence.DAWs;

public class FLStudio64Bit : Daw
{
    [SetsRequiredMembers]
    public FLStudio64Bit()
    {
        ProcessName = "FL64";
        DisplayName = "FL Studio";
        ImageKey = "fl";
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