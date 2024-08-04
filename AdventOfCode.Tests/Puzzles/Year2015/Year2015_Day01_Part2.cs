using AdventOfCode.Solutions.Library.Metadata;
using AdventOfCode.Tests.Library;

namespace AdventOfCode.Tests.Puzzles.Year2015;

public sealed class Year2015_Day01_Part2 : BaseTestData
{
    public override SimpleMetadata Metadata => new(Year.Year2015, Day.Day01, Part.Part2);

    public override List<(string, string)> TestCases => new()
    {
        (")", "1"),
        ("()())", "5")
    };
}
