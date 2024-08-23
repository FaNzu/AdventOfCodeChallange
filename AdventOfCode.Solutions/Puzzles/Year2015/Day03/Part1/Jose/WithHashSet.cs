using AdventOfCode.Solutions.Library;
using AdventOfCode.Solutions.Library.Metadata;

namespace AdventOfCode.Solutions.Puzzles.Year2015.Day03.Part1.Jose;

public sealed class WithHashSet : BaseSolution
{
    public override SolutionMetadata Metadata => new(Year.Year2015, Day.Day03, Part.Part1, Author.Jose);

    public override Task<string> Solve(string input)
    {
        var visitedPositions = new HashSet<(int, int)>()
        {
            (0, 0),
        };

        var (currentX, currentY) = (0, 0);

        foreach (var @char in input)
        {
            var (movementX, movementY) = Directions[@char];

            currentX += movementX;
            currentY += movementY;

            visitedPositions.Add((currentX, currentY));
        }

        return Task.FromResult(visitedPositions.Count.ToString());
    }

    private static readonly Dictionary<char, (int, int)> Directions = new()
    {
        { '^', (0, -1) },
        { '>', (1, 0) },
        { 'v', (0, 1) },
        { '<', (-1, 0) },
    };
}
