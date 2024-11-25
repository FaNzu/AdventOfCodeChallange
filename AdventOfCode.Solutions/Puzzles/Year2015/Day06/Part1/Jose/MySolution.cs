using AdventOfCode.Solutions.Library;
using AdventOfCode.Solutions.Library.Metadata;

namespace AdventOfCode.Solutions.Puzzles.Year2015.Day06.Part1.Jose;

public sealed class MySolution : BaseSolution
{
    public override SolutionMetadata Metadata => new(Year.Year2015, Day.Day06, Part.Part1, Author.Jose);

    public override Task<string> Solve(string input)
    {
        var instructions = input.Split("\r\n");

        var grid = new Grid(1000, 1000);

        foreach (var instruction in instructions)
        {
            var theInstruction = Instruction.Create(instruction);

            grid.Update(theInstruction);
        }

        return Task.FromResult(grid.CountActive().ToString());
    }

    private sealed class Grid(int x, int y)
    {
        private readonly bool[,] _grid = new bool[x, y];

        public int CountActive()
        {
            var count = 0;

            ExecuteThroughRange(
                new Range(
                    new Coordinates(0, 0),
                    new Coordinates(
                        _grid.GetLength(0) - 1,
                        _grid.GetLength(1) - 1)),
                (grid, x, y) =>
                {
                    if (grid[x, y])
                    {
                        count++;
                    }
                });

            return count;
        }

        public void Update(Instruction instruction) => UpdateInternal((dynamic)instruction);

        private void UpdateInternal(Instruction _) => throw new NotSupportedException();

        private void UpdateInternal(ToggleInstruction toggle)
        {
            ExecuteThroughRange(toggle.Range, (grid, x, y) => grid[x, y] = !grid[x, y]);
        }

        private void UpdateInternal(TurnOnInstruction turnOn)
        {
            ExecuteThroughRange(turnOn.Range, (grid, x, y) => grid[x, y] = true);
        }

        private void UpdateInternal(TurnOffInstruction turnOff)
        {
            ExecuteThroughRange(turnOff.Range, (grid, x, y) => grid[x, y] = false);
        }

        private void ExecuteThroughRange(Range range, Action<bool[,], int, int> action)
        {
            for (var x = range.Start.X; x <= range.End.X; x++)
            {
                for (var y = range.Start.Y; y <= range.End.Y; y++)
                {
                    action(_grid, x, y);
                }
            }
        }
    }

    private sealed record Coordinates(int X, int Y);

    private sealed record Range(Coordinates Start, Coordinates End);

    private abstract class Instruction
    {
        public static Instruction Create(string input)
        {
            var parts = input.Split(' ');

            if (parts.Length == 4)
            {
                return new ToggleInstruction(CreateRange(parts[1], parts[3]));
            }

            var range = CreateRange(parts[2], parts[4]);

            switch (parts[1])
            {
                case "on":
                    return new TurnOnInstruction(range);
                case "off":
                    return new TurnOffInstruction(range);
                default:
                    throw new NotSupportedException();
            }
        }

        private static Range CreateRange(string from, string to)
        {
            return new Range(
                CreateCoordinates(from),
                CreateCoordinates(to));
        }

        private static Coordinates CreateCoordinates(string input)
        {
            var parts = input.Split(',');

            return new Coordinates(
                int.Parse(parts[0]),
                int.Parse(parts[1]));
        }
    }

    private sealed class ToggleInstruction(Range range) : Instruction
    {
        public Range Range { get; } = range;
    }

    private sealed class TurnOnInstruction(Range range) : Instruction
    {
        public Range Range { get; } = range;
    }

    private sealed class TurnOffInstruction(Range range) : Instruction
    {
        public Range Range { get; } = range;
    }
}