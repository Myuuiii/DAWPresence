namespace DAWPresence.DAWs;

public class Cubase14 : Daw
{
    public Cubase14()
    {
        ProcessName = "Cubase14";
        DisplayName = "Cubase 14";
        ImageKey = "image";
        ApplicationId = "1223993322243362898";
        WindowTrim = "Cubase Pro Project - ";
        TitleOffset = 0;
    }

    public override string GetProjectNameFromProcessWindow()
    {
        var process = GetProcess();
        if (process is null) return "";
        var title = process.MainWindowTitle;
        return title.Contains(WindowTrim)
            ? title.Replace(WindowTrim, "")
            : "";
    }
}