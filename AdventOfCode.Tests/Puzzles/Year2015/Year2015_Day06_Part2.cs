using AdventOfCode.Solutions.Library.Metadata;
using AdventOfCode.Tests.Library;

namespace AdventOfCode.Tests.Puzzles.Year2015;

public sealed class Year2015_Day06_Part2 : BaseTestData
{
    public override SimpleMetadata Metadata => new(Year.Year2015, Day.Day06, Part.Part2);

    public override List<(string, string)> TestCases => new()
    {
        ("turn on 0,0 through 999,999","1000000"),
        ("toggle 0,0 through 999,0", "2000"),
        ("turn off 499,499 through 500,500", "0"),
        ("turn on 0,0 through 999,999\nturn off 499,499 through 500,500", "999996")
    };
}
