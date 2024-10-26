namespace DAWPresence.DAWs;

using System.Diagnostics;
using static System.Diagnostics.Process;

public partial class SteinbergNuendo : Daw
{
    public SteinbergNuendo()
    {
        ProcessName = "Nuendo13";
        DisplayName = "Nuendo 13";
        ImageKey = "nuendo";
        ApplicationId = "1072197265587974144";
        WindowTrim = " - ";
    }

    public override string GetProjectNameFromProcessWindow()
    {
        Process? process = GetProcess();
        if (process is null) return "";
        string title = process.MainWindowTitle;

       
        int trimIndex = title.IndexOf(WindowTrim);
        if (trimIndex != -1)
        {
            int titleOffset = trimIndex + WindowTrim.Length;
            return title[titleOffset..];
        }
        return "";
    }
}