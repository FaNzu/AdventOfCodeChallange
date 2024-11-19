using AdventOfCode.Solutions.Library;
using AdventOfCode.Solutions.Library.Metadata;

namespace AdventOfCode.Solutions.Puzzles.Year2015.Day06.Part1;

public sealed class Grid : BaseSolution
{
    public override SolutionMetadata Metadata => new(Year.Year2015, Day.Day06, Part.Part1, Author.Alice);

    public override async Task<string> Solve(string input)
    {
        int[,] grid = new int[1000, 1000];

        string[] instructions = input.Split('\n');

        foreach (string instruction in instructions)
        {
            ProcessInstruction(grid, instruction);
        }

        int litLights = CountLightsOn(grid);

        return litLights.ToString();
    }

    private void ProcessInstruction(int[,] grid, string instruction)
    {
        string operation;
        if (instruction.StartsWith("turn on"))
        {
            operation = "turn on";
            instruction = instruction.Substring("turn on ".Length);
        }
        else if (instruction.StartsWith("turn off"))
        {
            operation = "turn off";
            instruction = instruction.Substring("turn off ".Length);
        }
        else
        {
            operation = "toggle";
            instruction = instruction.Substring("toggle ".Length);
        }

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
                switch (operation)
                {
                    case "turn on":
                        grid[x, y] = 1;
                        break;
                    case "turn off":
                        grid[x, y] = 0;
                        break;
                    case "toggle":
                        grid[x, y] = 1 - grid[x, y];
                        break;
                }
            }
        }
    }

    private int CountLightsOn(int[,] grid)
    {
        int count = 0;
        for (int x = 0; x < grid.GetLength(0); x++)
        {
            for (int y = 0; y < grid.GetLength(1); y++)
            {
                count += grid[x, y];
            }
        }
        return count;
    }
}
