using AdventOfCode.Solutions.Library.Metadata;
using AdventOfCode.Tests.Library;

namespace AdventOfCode.Tests.Puzzles.Year2015;

public sealed class Year2015_Day04_Part1 : BaseTestData
{
    public override SimpleMetadata Metadata => new(Year.Year2015, Day.Day04, Part.Part1);

    public override List<(string, string)> TestCases => new()
    {
        ("abcdef", "609043"),
        ("pqrstuv", "1048970"),
    };
}
