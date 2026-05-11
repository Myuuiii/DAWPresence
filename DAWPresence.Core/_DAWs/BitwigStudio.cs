using System.Diagnostics.CodeAnalysis;

namespace DAWPresence.DAWs;

public class BitwigStudio : Daw
{
    [SetsRequiredMembers]
    public BitwigStudio()
    {
        ProcessName = "Bitwig Studio";
        DisplayName = "Bitwig Studio";
        ImageKey = "icon";
        ApplicationId = "";
        WindowTrim = " - Bitwig Studio - ";
        TitleOffset = 16;
    }

    public override string ParseProjectName(string title)
    {
        if (title.StartsWith("Bitwig Studio - "))
        {
            return title[16..];
        }
        
        // Fall back to old format: "ProjectName - Bitwig Studio - "
        return title.Contains(WindowTrim)
            ? title[..^TitleOffset]
            : "";
    }

    public override string GetProjectNameFromProcessWindow()
    {
        var process = GetProcess();
        if (process is null) return "";
        return ParseProjectName(process.MainWindowTitle);
    }
}