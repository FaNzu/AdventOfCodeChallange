using AdventOfCode.Solutions.Library.Metadata;
using AdventOfCode.Tests.Library;

namespace AdventOfCode.Tests.Puzzles.Year2015;

public sealed class Year2015_Day05_Part1 : BaseTestData
{
    public override SimpleMetadata Metadata => new(Year.Year2015, Day.Day05, Part.Part1);

    public override List<(string, string)> TestCases => new()
    {
        ("ugknbfddgicrmopn", "true"),
        ("aaa", "true"),
        ("jchzalrnumimnmhp", "false"),
        ("haegwjzuvuyypxyu", "false"),
        ("dvszwmarrgswjxmb", "false")
    };
}
