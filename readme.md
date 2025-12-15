![](https://cdn.myuuiii.com/projects/dawpresence/DAWRichPresence_v3.png)

A Discord Rich Presence app for several DAWs like FL Studio and Ableton.

| ![](https://ss.myuuiii.com/82ded0e9-b1e8-479b-86e0-cba978a63ddd.png) | ![](https://ss.myuuiii.com/7634c47d-db45-4323-bc5c-7c6ab1993ea3.png) |
| :----------------------------------------: | :------------------------------------------------: |


## How to use

- Make sure you have the latest .NET Desktop Runtime installed. You can download it [here through the official microsoft website](https://dotnet.microsoft.com/en-us/download/dotnet/thank-you/runtime-desktop-8.0.6-windows-x64-installer)
- Download the latest release from the [releases](https://github.com/Myuuiii/DAWPresence/releases/) tab.
- Run the executable to create initial configuration files. The software will continue to run in the background.
- Close the software by running the executable again. You can change the config to your liking.
- Upon running the software, if it has detected a DAW that is currently supported, your Discord presence should be updated automatically.

### Running as a Background Service & Using the Tray Icon

DAWPresence runs as a background app and provides a tray icon for easy access and management.

#### How to Install and Run

1. **Download & Extract:** Download the latest release and extract it to a folder of your choice.
2. **Run the App:** Double-click the `DAWPresenceBackgroundApp.exe` file. The app will start in the background and show a tray icon in your Windows system tray (bottom-right corner).
3. **Tray Icon Features:**
	- **Right-click the tray icon** to access options such as exit, reload config, or open the config folder.
	- **Left-click** may show status or bring up a menu (depending on version).
4. **Auto-Start (Optional):** To have DAWPresence start automatically with Windows:
	- **Right-click the tray icon** and check the `Open on Startup` option in the menu. This will automatically configure DAWPresence to launch with Windows using the built-in method.
5. **Stopping the App:**
	- Right-click the tray icon and select `Exit`, or run the executable again to close it.

---

## Contributing

### Adding a new DAW

If you want to add a new DAW or want a maintainer to, you can do so by following the steps below:

### DIY:

- Create a new branch in the format `{username}/{DAWname}-support` 
- Create a new class in the `DAWPresenceBackgroundApp/DAWs` folder that inherits from `Daw`
- Provide required values, such as the process name, window title, and the DAW name
- Push the branch to the repository
- Create a pull request to the main branch
- A maintainer will review the pull request and provide feedback
- Once the pull request is approved, it will be merged into the main branch

*Don't worry about the ApplicationId, one of the maintainers will provide those for you*

### Requesting a new DAW

- Create a new issue with the `DAW REQUEST` label
- Provide the name of the DAW and any additional information that might be useful (such as window title when no project is loaded, and when a project is loaded)
- A maintainer will create a new branch and add the DAW to the project for you
- You can then test the new DAW support and provide feedback
- Once the DAW is working as expected, the branch will be merged into the main branch

## Custom Image Key

Some people might want a custom image on the rich presence. To bump the project a bit, I am making this exclusive to people that have starred this repository. For those that have, please contact `myuuiii` on Discord for more information. An example is shown below

![](https://ss.myuuiii.com/7634c47d-db45-4323-bc5c-7c6ab1993ea3.png)


###### Example config.yml

```yml
# How often to update Discord Rich Presence (hh:mm:ss)
UpdateInterval: 00:00:03
# Offset to subtract from elapsed time (hh:mm:ss)
Offset: 00:00:00
# Text to show when no project is open
IdleText: Not working on a project
# Prefix for the project name
WorkingPrefixText: 'Working on '
# Enable custom image for Discord Rich Presence
UseCustomImage: true
# Your custom image key (contact myuuiii on Discord if eligible)
CustomImageKey: myuuiii
# Disable pop-up notifications
DisablePopup: false
# Keep DAW presence after opening plugin windows
EnablePersistence: false
# Reset elapsed time when switching projects
ResetElapsedOnProjectSwitch: false
# Enable secret mode to hide project details
SecretMode: false
# Text to show when secret mode is enabled
SecretModeText: Working on a secret project
```

## Contributors

Special thanks to everyone who has contributed to DAWPresence, including code, ideas, testing, and feedback!

- **myuuiii** - Creator & main developer
- **[Simoxus](https://github.com/Simoxus)** - Helped with tray icon, better config loading, installing on startup, and more.
- **[Intancote](https://github.com/Intancote)** - Added FL Studio Mobile
- **[Ahontt](https://github.com/ahontt)** - Fixed Cubase 15 detection
- **[Alixti](https://github.com/alixti)** - Added secret project to hide project details

If you have contributed and would like to be mentioned here, feel free to open a pull request!!

