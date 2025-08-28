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
        const string prefix = "Cubase Pro Project by ";
        if (!title.StartsWith(prefix)) return "";
        var rest = title.Substring(prefix.Length);
        var parts = rest.Split(" - ");
        return parts.Length > 1 ? parts[^1] : "";
    }
}