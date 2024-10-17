using AdventOfCode.Solutions.Library.Metadata;
using AdventOfCode.Tests.Library;

namespace AdventOfCode.Tests.Puzzles.Year2015;

public sealed class Year2015_Day02_Part1 : BaseTestData
{
    public override SimpleMetadata Metadata => new(Year.Year2015, Day.Day02, Part.Part1);

    public override List<(string, string)> TestCases => new()
    {
        ("2x3x4", "58"),
        ("1x1x10", "43")
    };
}
