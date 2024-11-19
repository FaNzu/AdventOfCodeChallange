using AdventOfCode.Solutions.Library.Metadata;
using AdventOfCode.Solutions.Library;

namespace AdventOfCode.Solutions.Puzzles.Year2015.Day03.Part1.Ask;
public sealed class WithHashset : BaseSolution
{
    public override SolutionMetadata Metadata => new(Year.Year2015, Day.Day03, Part.Part1, Author.Ask);

    public override Task<string> Solve(string input)
    {
        var directions = new Dictionary<char, (int x, int y)>
        {
            { '>', (1, 0) },
            { '<', (-1, 0) },
            { '^', (0, 1) },
            { 'v', (0, -1) }
        };

        var houses = new HashSet<(int, int)>();
        var current = (x: 0, y: 0);
        houses.Add(current);

        foreach (var move in input)
        {
            if (directions.ContainsKey(move))
            {
                current = (current.x + directions[move].x, current.y + directions[move].y);
                houses.Add(current);
            }
        }

        return Task.FromResult(houses.Count.ToString());
    }
}