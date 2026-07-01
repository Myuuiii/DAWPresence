using System.Diagnostics.CodeAnalysis;

namespace DAWPresence.DAWs;

public class SteinbergNuendo15 : Daw
{
    [SetsRequiredMembers]
    public SteinbergNuendo15()
    {
        ProcessName = "Nuendo15";
        DisplayName = "Nuendo 15";
        ImageKey = "nuendo";
        ApplicationId = "1072197265587974144";
        WindowTrim = " - ";
    }

    public override string ParseProjectName(string title)
    {
        var trimIndex = title.IndexOf(WindowTrim);
        if (trimIndex != -1)
        {
            var titleOffset = trimIndex + WindowTrim.Length;
            return title[titleOffset..];
        }

        return "";
    }

    public override string GetProjectNameFromProcessWindow()
    {
        var process = GetProcess();
        if (process is null) return "";
        return ParseProjectName(process.MainWindowTitle);
    }
}
