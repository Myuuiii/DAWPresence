namespace DAWPresence.DAWs;

public class SeratoDjPro : Daw
{
    public SeratoDjPro() : base()
    {
        ProcessName = "Serato DJ Pro";
        DisplayName = "Serato DJ Pro";
        ImageKey = "serrato";
        ApplicationId = "1400470240533545040";
        WindowTrim = string.Empty;
        TitleOffset = 0;
        HideDetails = true;
    }

    public override string GetProjectNameFromProcessWindow() =>
        // Does not have a project name in the window title 
        string.Empty;
}