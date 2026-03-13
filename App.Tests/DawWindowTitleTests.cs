using DAWPresence;
using DAWPresence.DAWs;
using DAWPresenceBackgroundApp.DAWs;
using NUnit.Framework;
using Shouldly;

namespace App.Tests;

[TestFixture]
public class DawWindowTitleTests
{
    // Ableton Live 9

    [TestCase("MyProject - Ableton Live 9 Intro",    "MyProject")]
    [TestCase("[MyProject] - Ableton Live 9 Intro",  "[MyProject]")]
    public void AbletonLive9Intro_ParseProjectName(string title, string expected) =>
        new AbletonLive9Intro().ParseProjectName(title).ShouldBe(expected);

    [TestCase("MyProject - Ableton Live 9 Lite",    "MyProject")]
    [TestCase("[MyProject] - Ableton Live 9 Lite",  "[MyProject]")]
    public void AbletonLive9Lite_ParseProjectName(string title, string expected) =>
        new AbletonLive9Lite().ParseProjectName(title).ShouldBe(expected);

    [TestCase("MyProject - Ableton Live 9 Standard",    "MyProject")]
    [TestCase("[MyProject] - Ableton Live 9 Standard",  "[MyProject]")]
    public void AbletonLive9Standard_ParseProjectName(string title, string expected) =>
        new AbletonLive9Standard().ParseProjectName(title).ShouldBe(expected);

    [TestCase("MyProject - Ableton Live 9 Suite",    "MyProject")]
    [TestCase("[MyProject] - Ableton Live 9 Suite",  "[MyProject]")]
    public void AbletonLive9Suite_ParseProjectName(string title, string expected) =>
        new AbletonLive9Suite().ParseProjectName(title).ShouldBe(expected);

    // Ableton Live 10

    [TestCase("MyProject - Ableton Live 10 Intro",    "MyProject")]
    [TestCase("[MyProject] - Ableton Live 10 Intro",  "[MyProject]")]
    public void AbletonLive10Intro_ParseProjectName(string title, string expected) =>
        new AbletonLive10Intro().ParseProjectName(title).ShouldBe(expected);

    [TestCase("MyProject - Ableton Live 10 Lite",    "MyProject")]
    [TestCase("[MyProject] - Ableton Live 10 Lite",  "[MyProject]")]
    public void AbletonLive10Lite_ParseProjectName(string title, string expected) =>
        new AbletonLive10Lite().ParseProjectName(title).ShouldBe(expected);

    [TestCase("MyProject - Ableton Live 10 Standard",    "MyProject")]
    [TestCase("[MyProject] - Ableton Live 10 Standard",  "[MyProject]")]
    public void AbletonLive10Standard_ParseProjectName(string title, string expected) =>
        new AbletonLive10Standard().ParseProjectName(title).ShouldBe(expected);

    [TestCase("MyProject - Ableton Live 10 Suite",    "MyProject")]
    [TestCase("[MyProject] - Ableton Live 10 Suite",  "[MyProject]")]
    public void AbletonLive10Suite_ParseProjectName(string title, string expected) =>
        new AbletonLive10Suite().ParseProjectName(title).ShouldBe(expected);

    // Ableton Live 11

    [TestCase("MyProject - Ableton Live 11 Intro",    "MyProject")]
    [TestCase("[MyProject] - Ableton Live 11 Intro",  "[MyProject]")]
    public void AbletonLive11Intro_ParseProjectName(string title, string expected) =>
        new AbletonLive11Intro().ParseProjectName(title).ShouldBe(expected);

    [TestCase("MyProject - Ableton Live 11 Lite",    "MyProject")]
    [TestCase("[MyProject] - Ableton Live 11 Lite",  "[MyProject]")]
    public void AbletonLive11Lite_ParseProjectName(string title, string expected) =>
        new AbletonLive11Lite().ParseProjectName(title).ShouldBe(expected);

    [TestCase("MyProject - Ableton Live 11 Standard",    "MyProject")]
    [TestCase("[MyProject] - Ableton Live 11 Standard",  "[MyProject]")]
    public void AbletonLive11Standard_ParseProjectName(string title, string expected) =>
        new AbletonLive11Standard().ParseProjectName(title).ShouldBe(expected);

    [TestCase("MyProject - Ableton Live 11 Suite",    "MyProject")]
    [TestCase("[MyProject] - Ableton Live 11 Suite",  "[MyProject]")]
    public void AbletonLive11Suite_ParseProjectName(string title, string expected) =>
        new AbletonLive11Suite().ParseProjectName(title).ShouldBe(expected);

    // Ableton Live 12

    [TestCase("MyProject - Ableton Live 12 Lite",    "MyProject")]
    [TestCase("[MyProject] - Ableton Live 12 Lite",  "[MyProject]")]
    public void AbletonLive12Lite_ParseProjectName(string title, string expected) =>
        new AbletonLive12Lite().ParseProjectName(title).ShouldBe(expected);

    [TestCase("MyProject - Ableton Live 12 Standard",    "MyProject")]
    [TestCase("[MyProject] - Ableton Live 12 Standard",  "[MyProject]")]
    public void AbletonLive12Standard_ParseProjectName(string title, string expected) =>
        new AbletonLive12Standard().ParseProjectName(title).ShouldBe(expected);

    [TestCase("MyProject - Ableton Live 12 Suite",    "MyProject")]
    [TestCase("[MyProject] - Ableton Live 12 Suite",  "[MyProject]")]
    public void AbletonLive12Suite_ParseProjectName(string title, string expected) =>
        new AbletonLive12Suite().ParseProjectName(title).ShouldBe(expected);

    // Other DAWs

    [TestCase("MyProject.acd - ACID Music Studio 10.0", "MyProject")]
    public void Acid10_ParseProjectName(string title, string expected) =>
        new Acid10().ParseProjectName(title).ShouldBe(expected);

    [TestCase("MyProject - Bitwig Studio - ", "MyProject - ")]
    public void BitwigStudio_ParseProjectName(string title, string expected) =>
        new BitwigStudio().ParseProjectName(title).ShouldBe(expected);

    [TestCase("Cakewalk Sonar -  - MyProject - Cakewalk Sonar - ", "Cakewalk Sonar -  - MyProject - ")]
    public void CakewalkSonar_ParseProjectName(string title, string expected) =>
        new CakewalkSonar().ParseProjectName(title).ShouldBe(expected);

    [TestCase("Cubase Pro Project - MyProject", "MyProject")]
    public void Cubase13_ParseProjectName(string title, string expected) =>
        new Cubase13().ParseProjectName(title).ShouldBe(expected);

    [TestCase("Cubase Pro Project by JohnDoe - MyProject", "MyProject")]
    public void Cubase14_ParseProjectName(string title, string expected) =>
        new Cubase14().ParseProjectName(title).ShouldBe(expected);

    [TestCase("Cubase 15 - MyProject", "MyProject")]
    public void Cubase15_ParseProjectName(string title, string expected) =>
        new Cubase15().ParseProjectName(title).ShouldBe(expected);

    [TestCase("MyProject - FL Studio 21", "MyProject")]
    public void FLStudio_ParseProjectName(string title, string expected) =>
        new FLStudio().ParseProjectName(title).ShouldBe(expected);

    [TestCase("MyProject - FL Studio 21", "MyProject")]
    public void FLStudio64Bit_ParseProjectName(string title, string expected) =>
        new FLStudio64Bit().ParseProjectName(title).ShouldBe(expected);

    [TestCase("anything", "")]
    public void FLStudioMobile_ParseProjectName(string title, string expected) =>
        new FLStudioMobile().ParseProjectName(title).ShouldBe(expected);

    [TestCase("MyProject.fspro - Event Editor", "MyProject")]
    public void FMODStudio_ParseProjectName(string title, string expected) =>
        new FMODStudio().ParseProjectName(title).ShouldBe(expected);

    [TestCase("Nuendo 13 - MyProject", "MyProject")]
    public void SteinbergNuendo_ParseProjectName(string title, string expected) =>
        new SteinbergNuendo().ParseProjectName(title).ShouldBe(expected);

    [TestCase("MyProject.rpp - REAPER v7.0/win64", "MyProject.rpp")]
    public void Reaper_ParseProjectName(string title, string expected) =>
        new Reaper().ParseProjectName(title).ShouldBe(expected);

    [TestCase("MyProject.reason", "MyProject")]
    public void Reason_ParseProjectName(string title, string expected) =>
        new Reason().ParseProjectName(title).ShouldBe(expected);

    [TestCase("rekordbox 6.8.5", "")]
    public void Rekordbox_ParseProjectName(string title, string expected) =>
        new Rekordbox().ParseProjectName(title).ShouldBe(expected);

    [TestCase("MyProject.xrns - Renoise (x64)", "MyProject")]
    public void Renoise_ParseProjectName(string title, string expected) =>
        new Renoise().ParseProjectName(title).ShouldBe(expected);

    [TestCase("Serato DJ Pro", "")]
    public void SeratoDjPro_ParseProjectName(string title, string expected) =>
        new SeratoDjPro().ParseProjectName(title).ShouldBe(expected);

    [TestCase("Studio One - MyProject", "MyProject")]
    public void StudioOne_ParseProjectName(string title, string expected) =>
        new StudioOne().ParseProjectName(title).ShouldBe(expected);

    [TestCase("Studio Pro - HPro 2K*", "HPro 2K")]
    public void FenderStudioPro_ParseProjectName(string title, string expected) =>
        new FenderStudioPro().ParseProjectName(title).ShouldBe(expected);
}
