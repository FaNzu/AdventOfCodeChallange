using System.Globalization;
using AdventOfCode.Solutions.Library;
using AdventOfCode.Solutions.Library.Metadata;

namespace AdventOfCode.Solutions.Puzzles.Year2015.Day01.Part1.Jose;

public sealed class WithForeachAndComparison : BaseSolution
{
    public override SolutionMetadata Metadata => new(Year.Year2015, Day.Day01, Part.Part1, Author.Jose);

    public override Task<string> Solve(string input)
    {
        var floor = 0;

        foreach (var character in input)
        {
            if (character == '(')
            {
                floor++;
            }
            else
            {
                floor--;
            }
        }

        return Task.FromResult(floor.ToString(CultureInfo.InvariantCulture));
    }
}
