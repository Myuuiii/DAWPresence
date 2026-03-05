namespace DAWPresence.DAWs;

public class FLStudioMobile : Daw
{
    public FLStudioMobile()
    {
        ProcessName = "FLMobile.UWP";
        DisplayName = "FL Studio Mobile";
        ImageKey = "flm";
        ApplicationId = "1416423031256911994";
        WindowTrim = "";
        TitleOffset = 0;
    }

    public override string GetProjectNameFromProcessWindow()
    {
        var packages = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Packages");
        if (!Directory.Exists(packages)) return string.Empty;

        var recentFile = Directory.EnumerateDirectories(packages, "Image-Line.FLStudioMobile_*")
            .Select(d => Path.Combine(d, "LocalState", "recent.state"))
            .FirstOrDefault(File.Exists);

        if (recentFile is null) return string.Empty;

        var line = File.ReadLines(recentFile).FirstOrDefault();
        return line?.Length > 20 ? line[20..].Split('\0')[0].Trim() : string.Empty;
    }
}