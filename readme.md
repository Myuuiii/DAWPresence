![](https://cdn.myuuiii.com/projects/dawpresence/DAWRichPresence_v3.png)

A Discord Rich Presence app for several DAWs like FL Studio and Ableton.

> The software is not finished yet and DAWs except for Ableton Live and FL Studio don't have application ids yet so will not work. I expect to have added these by the 23rd of December 2022.

| ![](https://ss.myuuiii.com/Glf1gL6PvE.png) | ![](https://ss.myuuiii.com/Discord_edjjth5Bp5.png) |
| :----------------------------------------: | :------------------------------------------------: |

## How to use

- Make sure you have the latest .NET Desktop Runtime installed. You can download it [here through the official microsoft website](https://dotnet.microsoft.com/en-us/download/dotnet/thank-you/runtime-desktop-8.0.6-windows-x64-installer)

- Download the latest release from the [releases](https://github.com/Myuuiii/DAWPresence/releases/) tab.
- Run the executable to create initial configuration file.
- Close the software if you wish to customize the config and restart the software after saving your changes.
- Upon running the software, if it has detected a DAW that is currently supported, your Discord presence should be updated automatically.



## Roadmap

If there's something you'd like to have implemented you can either send `myuuiii` a message on Discord or create an issue here on GitHub with the label "enhancement"

- [ ] Discord's implementation of "time elapsed" only allows for 24 hours before wrapping back around to "00:00:00". Make it so it can also track longer periods of time

## Contributing

### Adding a new DAW

If there is a DAW that is currently not implemented you can easily add it yourself. You can take any of the files in the `DAWs` folder as a reference and create a new class just like it with the values you need for your DAW. For most DAWs you will not have to change the `GetProjectNameFromProcessWindow()` method from the template. If your DAW's window title is more complex you can use the `Reaper.cs` class as a reference. 

## Custom Image Key

Some people might want a custom image on the rich presence. To bump the project a bit, I am making this exclusive to people that have starred this repository. For those that have, please contact `myuuiii` on Discord for more information. An example is shown below

![](https://ss.myuuiii.com/Discord_pe6dCw5B1o.png)

###### config.yml

```yml
UpdateInterval: 00:00:03
Offset: 00:00:00
IdleText: Not working on a project
WorkingPrefixText: 'Working on '
UseCustomImage: true
CustomImageKey: myuuiii
```

