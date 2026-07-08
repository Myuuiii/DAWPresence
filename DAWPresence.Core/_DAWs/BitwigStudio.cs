using System.Diagnostics.CodeAnalysis;

namespace DAWPresence.DAWs;

public class BitwigStudio : Daw
{
    [SetsRequiredMembers]
    public BitwigStudio()
    {
        ProcessName = "BitwigStudioApp";
        DisplayName = "Bitwig Studio";
        ImageKey = "icon";
        ApplicationId = "1524459853417025606";
        WindowTrim = DisplayName + " - ";
        TitleOffset = WindowTrim.Length;
    }

    public override string GetProjectNameFromProcessWindow()
    {
        var process = GetProcess();
        if (process is null) return "";

        var title = process.MainWindowTitle;

        return title.StartsWith(WindowTrim)
            ? title[TitleOffset..]
            : "";
    }
}