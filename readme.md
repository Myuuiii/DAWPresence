![](https://cdn.myuuiii.com/projects/dawpresence/DAWRichPresence_v3.png)

A Discord Rich Presence app for several DAWs like FL Studio and Ableton.

| ![](https://ss.myuuiii.com/82ded0e9-b1e8-479b-86e0-cba978a63ddd.png) | ![](https://ss.myuuiii.com/7634c47d-db45-4323-bc5c-7c6ab1993ea3.png) |
| :----------------------------------------: | :------------------------------------------------: |

## How to use

- Make sure you have the latest .NET Desktop Runtime installed. You can download it [here through the official microsoft website](https://dotnet.microsoft.com/en-us/download/dotnet/thank-you/runtime-desktop-8.0.6-windows-x64-installer)

- Download the latest release from the [releases](https://github.com/Myuuiii/DAWPresence/releases/) tab.
- Run the executable to create initial configuration files. The software will continute to run in the background.
- Close the software by running to executable again. You can change the config to your liking.
- Upon running the software, if it has detected a DAW that is currently supported, your Discord presence should be updated automatically.



## Roadmap

- [ ] Tray icon for easy access to the software
- [ ] Option to enable DAW Persistence upon first detection (keep the DAW presence after opening plugin windows, which can mess with the detection)
- [ ] Option to reset elapsed time when switching projects

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

###### config.yml

```yml
UpdateInterval: 00:00:03
Offset: 00:00:00
IdleText: Not working on a project
WorkingPrefixText: 'Working on '
UseCustomImage: true
CustomImageKey: myuuiii
```

