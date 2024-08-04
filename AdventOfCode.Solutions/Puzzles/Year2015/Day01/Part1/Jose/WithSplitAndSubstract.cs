using System.Globalization;
using AdventOfCode.Solutions.Library;
using AdventOfCode.Solutions.Library.Metadata;

namespace AdventOfCode.Solutions.Puzzles.Year2015.Day01.Part1.Jose;

public sealed class WithSplitAndSubstract : BaseSolution
{
    public override SolutionMetadata Metadata => new(Year.Year2015, Day.Day01, Part.Part1, Author.Jose);

    public override Task<string> Solve(string input)
    {
        return Task.FromResult((input.Split("(").Length - input.Split(")").Length)
            .ToString(CultureInfo.InvariantCulture));
    }
}
