namespace DAWPresence.DAWs;

public class Cubase15 : Daw
{
    public Cubase15()
    {
        ProcessName = "Cubase15";
        DisplayName = "Cubase 15";
        ImageKey = "image";
        ApplicationId = "1223993322243362898";
        WindowTrim = "Cubase";
        TitleOffset = 0;
    }

    public override string GetProjectNameFromProcessWindow()
    {
        var process = GetProcess();
        if (process is null) return "";
        var title = process.MainWindowTitle;
        const string prefix = "Cubase";
        if (!title.StartsWith(prefix)) return "";
        var rest = title.Substring(prefix.Length);
        var parts = rest.Split(" - ");
        return parts.Length > 1 ? parts[^1] : "";
    }
}