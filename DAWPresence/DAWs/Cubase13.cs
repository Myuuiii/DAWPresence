using System.Diagnostics;

namespace DAWPresence.DAWs;

public class Cubase13 : Daw
{
    public Cubase13()
    {
        ProcessName = "Cubase13";
        DisplayName = "Cubase 13";
        ImageKey = "image";
        ApplicationId = "1223993322243362898";
        WindowTrim = "Cubase Pro Project - ";
        TitleOffset = 0;
    }

    public override string GetProjectNameFromProcessWindow()
    {
        Process? process = GetProcess();
        if (process is null) return "";
        string title = process.MainWindowTitle;
        return title.Contains(WindowTrim)
            ? title.Replace(WindowTrim, "")
            : "";
    }
}