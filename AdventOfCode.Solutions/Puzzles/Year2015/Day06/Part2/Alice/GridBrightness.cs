using AdventOfCode.Solutions.Library;
using AdventOfCode.Solutions.Library.Metadata;

namespace AdventOfCode.Solutions.Puzzles.Year2015.Day06.Part2.Alice;

public sealed class  GridBrightness : BaseSolution
{
    public override SolutionMetadata Metadata => new(Year.Year2015, Day.Day06, Part.Part2, Author.Alice);

    public override async Task<string> Solve(string input)
    {
        int[,] grid = new int[1000, 1000];
        string[] instructions = input.Split('\n');

        foreach (string instruction in instructions)
        {
            ProcessInstruction(grid, instruction);
        }

        int totalBrightness = CountTotalBrightness(grid);
        return totalBrightness.ToString();
    }

    private void ProcessInstruction(int[,] grid, string instruction)
    {
        string operation = instruction.StartsWith("turn on") ? "turn on" :
                           instruction.StartsWith("turn off") ? "turn off" : "toggle";
        instruction = instruction.Substring(operation.Length + 1);

        string[] parts = instruction.Split(" through ");
        string[] startCoords = parts[0].Split(',');
        string[] endCoords = parts[1].Split(',');

        int startX = int.Parse(startCoords[0]);
        int startY = int.Parse(startCoords[1]);
        int endX = int.Parse(endCoords[0]);
        int endY = int.Parse(endCoords[1]);

        for (int x = startX; x <= endX; x++)
        {
            for (int y = startY; y <= endY; y++)
            {
                grid[x, y] = operation switch
                {
                    "turn on" => grid[x, y] + 1,
                    "turn off" => Math.Max(grid[x, y] - 1, 0),
                    "toggle" => grid[x, y] + 2,
                    _ => grid[x, y]
                };
            }
        }
    }

    private int CountTotalBrightness(int[,] grid)
    {
        int totalBrightness = 0;
        for (int x = 0; x < grid.GetLength(0); x++)
        {
            for (int y = 0; y < grid.GetLength(1); y++)
            {
                totalBrightness += grid[x, y];
            }
        }
        return totalBrightness;
    }
}
