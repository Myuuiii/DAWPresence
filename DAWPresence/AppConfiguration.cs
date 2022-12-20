namespace DAWPresence;

public class AppConfiguration
{
	public TimeSpan UpdateInterval { get; set; } = new(0, 0, 3);
	public TimeSpan Offset { get; set; } = new(0, 0, 0);
	public string IdleText { get; set; } = "Not working on a project";
	public string WorkingPrefixText { get; set; } = "Working on ";
	public bool UseCustomImage { get; set; } = false;
	public string CustomImageKey { get; set; } = "custom";
}