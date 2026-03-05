using System.Diagnostics;

namespace DAWPresence.DAWs;

public class FMODStudio : Daw
{
    public FMODStudio()
    {
        ProcessName = "FMOD Studio"; 
        DisplayName = "FMOD Studio";
        ImageKey = "fmodstudio";
        ApplicationId = "1420211706474533005";
        WindowTrim = "";
        TitleOffset = 0;
    }

    public override string GetProjectNameFromProcessWindow()
    {
        Process? process = GetProcess();
        if (process is null) return "";

        string title = process.MainWindowTitle;

        // fmod studio's titles look like: "PROJECT.fspro - Event Editor"
        if (string.IsNullOrWhiteSpace(title) || !title.Contains(" - "))
            return "";

        string nameWithExt = title.Split(" - ")[0];
        return Path.GetFileNameWithoutExtension(nameWithExt);
    }
}
