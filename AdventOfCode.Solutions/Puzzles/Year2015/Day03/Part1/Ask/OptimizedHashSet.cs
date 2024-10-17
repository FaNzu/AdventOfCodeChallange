using AdventOfCode.Solutions.Library.Metadata;
using AdventOfCode.Solutions.Library;

namespace AdventOfCode.Solutions.Puzzles.Year2015.Day03.Part1.Ask;
public sealed class OptimizedHashSet : BaseSolution
{
    public override SolutionMetadata Metadata => new(Year.Year2015, Day.Day03, Part.Part1, Author.Ask);

    public override Task<string> Solve(string input)
    {
        var answer = CountHousesWithPresents(input).ToString();
        return Task.FromResult(answer);
    }

    private int CountHousesWithPresents(string moves)
    {
        var houses = new HashSet<(int, int)>();
        int x = 0, y = 0;
        houses.Add((x, y));

        foreach (var move in moves)
        {
            switch (move)
            {
                case '>': x++; break;
                case '<': x--; break;
                case '^': y++; break;
                case 'v': y--; break;
            }
            houses.Add((x, y));
        }

        return houses.Count;
    }
}