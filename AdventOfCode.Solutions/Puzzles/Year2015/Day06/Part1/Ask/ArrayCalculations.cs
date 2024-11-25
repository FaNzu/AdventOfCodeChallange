using AdventOfCode.Solutions.Library.Metadata;
using AdventOfCode.Solutions.Library;

namespace AdventOfCode.Solutions.Puzzles.Year2015.Day06.Part1.Ask
{
    public sealed class ArrayCalculations : BaseSolution
    {
        public override SolutionMetadata Metadata => new(Year.Year2015, Day.Day06, Part.Part1, Author.Ask);
        public override Task<string> Solve(string input)
        {
            var gridSize = 1000;
            var brightnessGrid = new int[gridSize, gridSize];

            foreach (var inputLine in input.Split('\n'))
            {
                (int command, int x1, int y1, int x2, int y2) = ReadingLine(inputLine.Trim());

                for (int x = x1; x <= x2; x++)
                {
                    for (int y = y1; y <= y2; y++)
                    {
                        switch (command)
                        {
                            case 0: //toogle
                                brightnessGrid[x, y] = 1 - brightnessGrid[x, y];
                                break;
                            case 1: //on
                                brightnessGrid[x, y] = 1;
                                break;
                            case 2: //off
                                brightnessGrid[x, y] = 0;
                                break;
                        }
                    }
                }
            }

            long totalBrightness = 0;
            for (int x = 0; x < gridSize; x++)
            {
                for (int y = 0; y < gridSize; y++)
                {
                    totalBrightness += brightnessGrid[x, y];
                }
            }

            return Task.FromResult(totalBrightness.ToString());
        }

        private (int, int, int, int, int) ReadingLine(string line)
        {
            var parts = line.Split(' ');
            int command = parts[0] == "toggle" ? 0 : parts[1] == "on" ? 1 : 2;
            var startCoords = parts[command == 0 ? 1 : 2].Split(',');
            var endCoords = parts[^1].Split(',');

            return (
                command,
                int.Parse(startCoords[0]),
                int.Parse(startCoords[1]),
                int.Parse(endCoords[0]),
                int.Parse(endCoords[1])
            );
        }
    }
}