using AdventOfCode.Solutions.Library.Metadata;
using AdventOfCode.Solutions.Library;

namespace AdventOfCode.Solutions.Puzzles.Year2015.Day03.Part2.Ask;
public sealed class NormalCalculations : BaseSolution
{
    public override SolutionMetadata Metadata => new(Year.Year2015, Day.Day03, Part.Part2, Author.Ask);

    public override Task<string> Solve(string input)
    {
        var directions = new Dictionary<char, (int x, int y)>
            {
                { '>', (1, 0) },
                { '<', (-1, 0) },
                { '^', (0, 1) },
                { 'v', (0, -1) }
            };

        var santaHouses = VisitHouses(input, directions, 0);
        var robotHouses = VisitHouses(input, directions, 1);

        var allHouses = new HashSet<(int, int)>(santaHouses);
        allHouses.UnionWith(robotHouses);

        return Task.FromResult(allHouses.Count.ToString());
    }

    private HashSet<(int, int)> VisitHouses(string moves, Dictionary<char, (int x, int y)> directions, int startIndex)
    {
        var houses = new HashSet<(int, int)>();
        var current = (x: 0, y: 0);
        houses.Add(current);

        for (int i = startIndex; i < moves.Length; i += 2)
        {
            if (directions.ContainsKey(moves[i]))
            {
                current = (current.x + directions[moves[i]].x, current.y + directions[moves[i]].y);
                houses.Add(current);
            }
        }

        return houses;
    }
}
