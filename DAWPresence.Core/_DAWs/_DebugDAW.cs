using System.Diagnostics.CodeAnalysis;

namespace DAWPresence.DAWs;

// this one can be used to see if DAWs are detected and working without needing to open a DAW
#if DEBUG
public class CalculatorDebug : Daw
{
    [SetsRequiredMembers]
    public CalculatorDebug()
    {
        ProcessName = "Discord";
        DisplayName = "Discord";
        ImageKey = "myuuiii";
        ApplicationId = "1053952444859686983";
        WindowTrim = "";
        TitleOffset = 0;
        HideDetails = false;
    }

    public override string ParseProjectName(string title) => "Thanks for 100 stars!";

    public override string GetProjectNameFromProcessWindow() => "Thanks for 100 stars!";
}
#endif