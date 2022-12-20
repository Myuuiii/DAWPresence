# DAW Presence

A Discord Rich Presence app for several DAWs like FL Studio and Ableton.

> The software is not finished yet and most DAWs don't have application ids yet so will not work. I expect to have added these by the 23rd of December 2022.

| ![](https://ss.myuuiii.com/Glf1gL6PvE.png) | ![](https://ss.myuuiii.com/Discord_edjjth5Bp5.png) |
| :----------------------------------------: | :------------------------------------------------: |


## How to use

- Download the latest release from the [releases](https://github.com/Myuuiii/DAWPresence/releases/) tab.
- Run the executable to create initial configuration file.
- Close the software if you wish to customize the config and restart the software after saving your changes.
- Upon running the software should show if it has detected a DAW that is currently supported. If it does, your Discord presence should be updated automatically.

### Set up as a background service

I will work out this section later ♥

## Roadmap

If there's something you'd like to have implemented you can either send `Myuuiii#0001` a message on Discord or create an issue here on GitHub with the label "enhancement"

- [ ] Add custom images for each distribution of Ableton Live
- [ ] Discord's implementation of "time elapsed" only allows for 24 hours before wrapping back around to "00:00:00". Make it so it can also track longer periods of time

## Contributing

### Adding a new DAW

If there is a DAW that is currently not implemented you can easily add it yourself. You can take any of the files in the `DAWs` folder as a reference and create a new class just like it with the values you need for your DAW. For most DAWs you will not have to change the `GetProjectNameFromProcessWindow()` method from the template. If your DAW's window title is more complex you can use the `Reaper.cs` class as a reference. 

## Custom Image Key

Some people might want a custom image on the rich presence. To bump the project a bit, I am making this exclusive to people that have starred this repository. For those that have, please contact `Myuuiii#0001` on Discord for more information. An example is shown below

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

