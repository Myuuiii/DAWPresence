using DAWPresence;
using DAWPresence.DAWs;
using DAWPresenceBackgroundApp.DAWs;
using NUnit.Framework;
using Shouldly;

namespace App.Tests;

[TestFixture]
public class DawWindowTitleTests
{
    private static IEnumerable<TestCaseData> ParseProjectNameCases()
    {
        // Ableton Live 9
        yield return new TestCaseData(new AbletonLive9Intro(),    "MyProject - Ableton Live 9 Intro",    "MyProject");
        yield return new TestCaseData(new AbletonLive9Lite(),     "MyProject - Ableton Live 9 Lite",     "MyProject");
        yield return new TestCaseData(new AbletonLive9Standard(), "MyProject - Ableton Live 9 Standard", "MyProject");
        yield return new TestCaseData(new AbletonLive9Suite(),    "MyProject - Ableton Live 9 Suite",    "MyProject");
        // Ableton Live 10
        yield return new TestCaseData(new AbletonLive10Intro(),    "MyProject - Ableton Live 10 Intro",    "MyProject");
        yield return new TestCaseData(new AbletonLive10Lite(),     "MyProject - Ableton Live 10 Lite",     "MyProject");
        yield return new TestCaseData(new AbletonLive10Standard(), "MyProject - Ableton Live 10 Standard", "MyProject");
        yield return new TestCaseData(new AbletonLive10Suite(),    "MyProject - Ableton Live 10 Suite",    "MyProject");
        // Ableton Live 11
        yield return new TestCaseData(new AbletonLive11Intro(),    "MyProject - Ableton Live 11 Intro",    "MyProject");
        yield return new TestCaseData(new AbletonLive11Lite(),     "MyProject - Ableton Live 11 Lite",     "MyProject");
        yield return new TestCaseData(new AbletonLive11Standard(), "MyProject - Ableton Live 11 Standard", "MyProject");
        yield return new TestCaseData(new AbletonLive11Suite(),    "MyProject - Ableton Live 11 Suite",    "MyProject");
        // Ableton Live 12
        yield return new TestCaseData(new AbletonLive12Lite(),     "MyProject - Ableton Live 12 Lite",     "MyProject");
        yield return new TestCaseData(new AbletonLive12Standard(), "MyProject - Ableton Live 12 Standard", "MyProject");
        yield return new TestCaseData(new AbletonLive12Suite(),    "MyProject - Ableton Live 12 Suite",    "MyProject");
        // Other DAWs
        yield return new TestCaseData(new Acid10(),         "MyProject.acd - ACID Music Studio 10.0",                   "MyProject");
        yield return new TestCaseData(new BitwigStudio(),   "MyProject - Bitwig Studio - ",                       "MyProject - ");
        yield return new TestCaseData(new CakewalkSonar(),  "Cakewalk Sonar -  - MyProject - Cakewalk Sonar - ",  "Cakewalk Sonar -  - MyProject - ");
        yield return new TestCaseData(new Cubase13(),       "Cubase Pro Project - MyProject",                     "MyProject");
        yield return new TestCaseData(new Cubase14(),       "Cubase Pro Project by JohnDoe - MyProject",          "MyProject");
        yield return new TestCaseData(new Cubase15(),       "Cubase 15 - MyProject",                              "MyProject");
        yield return new TestCaseData(new FLStudio(),       "MyProject - FL Studio 21",                           "MyProject");
        yield return new TestCaseData(new FLStudio64Bit(),  "MyProject - FL Studio 21",                           "MyProject");
        yield return new TestCaseData(new FLStudioMobile(), "anything",                                           "");
        yield return new TestCaseData(new FMODStudio(),     "MyProject.fspro - Event Editor",                     "MyProject");
        yield return new TestCaseData(new SteinbergNuendo(),"Nuendo 13 - MyProject",                              "MyProject");
        yield return new TestCaseData(new Reaper(),         "MyProject.rpp - REAPER v7.0/win64",                  "MyProject.rpp");
        yield return new TestCaseData(new Reason(),         "MyProject.reason",                                   "MyProject");
        yield return new TestCaseData(new Rekordbox(),      "rekordbox 6.8.5",                                    "");
        yield return new TestCaseData(new Renoise(),        "MyProject.xrns - Renoise (x64)",                     "MyProject");
        yield return new TestCaseData(new SeratoDjPro(),    "Serato DJ Pro",                                      "");
        yield return new TestCaseData(new StudioOne(),         "Studio One - MyProject",         "MyProject");
        yield return new TestCaseData(new FenderStudioPro(),  "Studio Pro - HPro 2K*",         "HPro 2K");
    }

    [TestCaseSource(nameof(ParseProjectNameCases))]
    public void ParseProjectName_ReturnsExpectedName(Daw daw, string windowTitle, string expectedName)
    {
        daw.ParseProjectName(windowTitle).ShouldBe(expectedName);
    }
}
