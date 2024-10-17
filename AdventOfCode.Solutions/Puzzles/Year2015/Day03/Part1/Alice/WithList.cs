using AdventOfCode.Solutions.Library;
using AdventOfCode.Solutions.Library.Metadata;

namespace AdventOfCode.Solutions.Puzzles.Year2015.Day03.Part1.Alice;

public sealed class WithList  : BaseSolution
{
    public override SolutionMetadata Metadata => new(Year.Year2015, Day.Day03, Part.Part1, Author.Alice);
    public override Task<string> Solve(string input)
    {
        int x = 0, y = 0;

       var visitedHouses = new List<(int, int)>();

        visitedHouses.Add((x, y));

        foreach (char direction in input)
        {
            switch (direction)
            {
                case '^':
                    y += 1;
                    break;
                case 'v':
                    y -= 1;
                    break;
                case '>':
                    x += 1;
                    break;
                case '<':
                    x -= 1;
                    break;
            }

            if (!visitedHouses.Contains((x, y)))
            {
                visitedHouses.Add((x, y));
            }
        }

        return Task.FromResult(visitedHouses.Count.ToString());

    }
}