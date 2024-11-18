using AdventOfCode.Solutions.Library.Metadata;
using AdventOfCode.Tests.Library;

namespace AdventOfCode.Tests.Puzzles.Year2015;

public sealed class Year2015_Day05_Part2 : BaseTestData
{
    public override SimpleMetadata Metadata => new(Year.Year2015, Day.Day05, Part.Part2);

    public override List<(string, string)> TestCases => new()
    {
        ("qjhvhtzxzqqjkmpb", "1"),
        ("xxyxx", "1"),
        ("uurcxstgmygtbstg", "0"),
        ("ieodomkazucvgmuy", "0")
    };
}
