using AdventOfCode.Solutions.Library.Metadata;
using AdventOfCode.Tests.Library;

namespace AdventOfCode.Tests.Puzzles.Year2015;

public sealed class Year2015_Day03_Part1 : BaseTestData
{
    public override SimpleMetadata Metadata => new(Year.Year2015, Day.Day03, Part.Part1);

    public override List<(string, string)> TestCases => new()
    {
        (">", "2"),
        ("^>v<", "4"),
        ("^v^v^v^v^v", "2"),
    };
}
