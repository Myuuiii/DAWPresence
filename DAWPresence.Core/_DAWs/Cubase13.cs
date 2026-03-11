using System.Diagnostics.CodeAnalysis;

namespace DAWPresence.DAWs;

public class Cubase13 : Daw
{
    [SetsRequiredMembers]
    public Cubase13()
    {
        ProcessName = "Cubase13";
        DisplayName = "Cubase 13";
        ImageKey = "image";
        ApplicationId = "1223993322243362898";
        WindowTrim = "Cubase Pro Project - ";
        TitleOffset = 0;
    }

    public override string ParseProjectName(string title)
    {
        return title.Contains(WindowTrim) ? title.Replace(WindowTrim, "") : "";
    }

    public override string GetProjectNameFromProcessWindow()
    {
        var process = GetProcess();
        if (process is null) return "";
        return ParseProjectName(process.MainWindowTitle);
    }
}