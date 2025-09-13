namespace DAWPresence.DAWs;

public class FLStudioMobile : Daw
{
    public FLStudioMobile()
    {
        ProcessName = "FLMobile.UWP";
        DisplayName = "FL Studio Mobile";
        ImageKey = "flm";
        ApplicationId = "1416423031256911994";
        WindowTrim = string.Empty;
        TitleOffset = 0;
        HideDetails = true;
    }

    public override string GetProjectNameFromProcessWindow()
    {
        // Does not have a project name in the window title
        return string.Empty;
    }
}