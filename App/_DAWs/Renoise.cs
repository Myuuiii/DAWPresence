namespace DAWPresence.DAWs;

public class Renoise : Daw
{
    public Renoise()
    {
        ProcessName = "Renoise";
        DisplayName = "Renoise (x64)";
        ImageKey = "renoise";
        ApplicationId = "1400470416383938772";
        WindowTrim = string.Empty;
        TitleOffset = 0;
    }

    public override string GetProjectNameFromProcessWindow()
    {
        var process = GetProcess();
        if (process is null) return string.Empty;
        var title = process.MainWindowTitle;
        if (string.IsNullOrEmpty(title)) return string.Empty;
        var dashIndex = title.IndexOf(" - ", StringComparison.Ordinal);
        if (dashIndex > 0)
        {
            var projectPart = title.Substring(0, dashIndex);
            if (projectPart.EndsWith(".xrns", StringComparison.OrdinalIgnoreCase))
                return projectPart.Substring(0, projectPart.Length - 5); // Remove ".xrns"
            return projectPart;
        }

        return title;
    }
}