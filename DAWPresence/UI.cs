namespace DAWPresence;

public static class UI
{
    public static class Messages
    {
        public const string Ready = "[green]Ready[/]";
        public const string NoDawFound = "[gray]No DAW found[/]";
        public const string NoProjectFound = "[gray]No project found[/]";
        public const string CriticalError = "[red]Critical error[/]";
        public const string DiscordRpcError = "[purple]Discord RPC error[/]";
        public const string LoadedConfig = "[yellow]Loaded config[/]";
        public const string CreatingConfig = "[yellow]Creating config[/]";
        
        public const string f_DawLoaded = "[gray]{0} has been registered[/]";
        public const string f_DawDetected = "[magenta]Detected: {0}[/]";
        
        public const string f_ProjectDetected = "[magenta]Project: {0}[/]";
        
        public const string f_NewVersionAvailable = $"[aqua]A new version of DAWPresence is available! (Current: {{0}}, Latest: {{1}})\nDownload the new version at [yellow]https://github.com/myuuiii/DAWPresence/releases[/][/]";
        public const string FailedToCheckForUpdates = "[red]Failed to check for updates[/]";
        
        public const string BetaReleaseWarning = "[yellow]This is a beta release. Please report any issues to the GitHub repository at [yellow]https://github.com/myuuiii/DAWPresence/releases[/][/]";
        public const string DebugReleaseWarning = "[red]This is a debug release and should only be used for testing. Please get a beta or stable version at [yellow]https://github.com/myuuiii/DAWPresence/releases[/][/]";
    }
}