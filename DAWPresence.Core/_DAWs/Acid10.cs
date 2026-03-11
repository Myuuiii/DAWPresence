using System.Diagnostics.CodeAnalysis;

namespace DAWPresence.DAWs;

public class Acid10 : Daw
{
    [SetsRequiredMembers]
    public Acid10()
    {
        ProcessName = "musicstudio100";
        DisplayName = "Acid Music Studio 10";
        ImageKey = "acid10";
        ApplicationId = "1479379779911286908";
        WindowTrim = " - ACID";
        TitleOffset = 0;
    }

    public override string ParseProjectName(string title)
    {
        if (!title.Contains(WindowTrim))
            return "";
        
        var projectNameWithExtension = title[..title.IndexOf(WindowTrim, StringComparison.Ordinal)];
        
        if (projectNameWithExtension.EndsWith(".acd", StringComparison.OrdinalIgnoreCase))
        {
            return projectNameWithExtension[..^4];
        }
        
        return projectNameWithExtension;
    }

    public override string GetProjectNameFromProcessWindow()
    {
        var process = GetProcess();
        if (process is null) return "";
        return ParseProjectName(process.MainWindowTitle);
    }
}