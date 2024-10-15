using AdventOfCode.Solutions.Library.Metadata;
using AdventOfCode.Tests.Library;

namespace AdventOfCode.Tests.Puzzles.Year2015;

public sealed class Year2015_Day03_Part2 : BaseTestData
{
    public override SimpleMetadata Metadata => new(Year.Year2015, Day.Day03, Part.Part2);

    public override List<(string, string)> TestCases => new()
    {
        ("^v", "3"),
        ("^>v<", "3"),
        ("^v^v^v^v^v", "11"),
    };
}
