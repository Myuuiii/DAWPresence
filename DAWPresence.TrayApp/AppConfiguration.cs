namespace DAWPresence;

public class AppConfiguration
{
	/// <summary>
	/// Interval at which the app will check if the DAW is (still) running and update the presence accordingly.
	/// </summary>
	public TimeSpan UpdateInterval { get; set; } = new(0, 0, 10);

	/// <summary>
	/// Time offset to add to the current time when displaying the elapsed time.
	/// </summary>
	public TimeSpan Offset { get; set; } = new(0, 0, 0);
	
	/// <summary>
	/// Text to show when no project is open
	/// </summary>
	public string IdleText { get; set; } = "Not working on a project";
	
	/// <summary>
	/// Text to show before the project name when a project is open
	/// </summary>
	public string WorkingPrefixText { get; set; } = "Working on ";
	
	/// <summary>
	/// Overwrite the image key (for custom images) 
	/// </summary>
	public bool UseCustomImage { get; set; } = false;
	
	/// <summary>
	/// Custom image key to use
	/// </summary>
	public string CustomImageKey { get; set; } = "custom";
}