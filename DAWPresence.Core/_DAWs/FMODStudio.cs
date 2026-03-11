using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace DAWPresence.DAWs;

public class FMODStudio : Daw
{
    [SetsRequiredMembers]
    public FMODStudio()
    {
        ProcessName = "FMOD Studio"; 
        DisplayName = "FMOD Studio";
        ImageKey = "fmodstudio";
        ApplicationId = "1420211706474533005";
        WindowTrim = "";
        TitleOffset = 0;
    }

    public override string ParseProjectName(string title)
    {
        if (string.IsNullOrWhiteSpace(title) || !title.Contains(" - "))
            return "";

        string nameWithExt = title.Split(" - ")[0];
        return Path.GetFileNameWithoutExtension(nameWithExt);
    }

    public override string GetProjectNameFromProcessWindow()
    {
        Process? process = GetProcess();
        if (process is null) return "";
        return ParseProjectName(process.MainWindowTitle);
    }
}
