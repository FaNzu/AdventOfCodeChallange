using AdventOfCode.Solutions.Library;
using AdventOfCode.Solutions.Library.Metadata;

namespace AdventOfCode.Solutions.Puzzles.Year2015.Day01.Part1.Alice;

public sealed class WithSwitch : BaseSolution
{
    public override SolutionMetadata Metadata => new(Year.Year2015, Day.Day01, Part.Part1, Author.Alice);

    public override Task<string> Solve(string input)
    {
        var floor = 0;

        foreach (char c in input)
        {
            switch (c)
            {
                case '(':
                    floor++;
                    break;
                case ')':
                    floor--;
                    break;
            }
        }

        return Task.FromResult(floor.ToString());
    }
}