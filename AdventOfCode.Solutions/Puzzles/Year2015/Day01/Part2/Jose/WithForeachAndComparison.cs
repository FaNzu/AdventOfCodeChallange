using System.Globalization;
using AdventOfCode.Solutions.Library;
using AdventOfCode.Solutions.Library.Metadata;

namespace AdventOfCode.Solutions.Puzzles.Year2015.Day01.Part2.Jose;

public sealed class WithForeachAndComparison : BaseSolution
{
    public override SolutionMetadata Metadata => new(Year.Year2015, Day.Day01, Part.Part2, Author.Jose);

    public override Task<string> Solve(string input)
    {
        var floor = 0;
        var position = 1;

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

            if (floor == -1)
            {
                return Task.FromResult(position.ToString(CultureInfo.InvariantCulture));
            }

            position++;
        }

        throw new InvalidOperationException("Assuming input moves to floor -1 at some point.");
    }
}
