using AdventOfCode.Solutions.Library;
using AdventOfCode.Solutions.Library.Metadata;

namespace AdventOfCode.Solutions.Puzzles.Year2015.Day01.Part1.Rasmus;
public class HestingSanta : BaseSolution
{
    public override SolutionMetadata Metadata => new(Year.Year2015, Day.Day01, Part.Part1, Author.RTK);

    public override Task<string> Solve(string input)
    {
        var floor = 0;

        foreach (var c in input)
        {
            if (c == '(')
            {
                floor++;
            }
            else if (c == ')')
            {
                floor--;
            }
        }

        return Task.FromResult(floor.ToString());
    }
}
