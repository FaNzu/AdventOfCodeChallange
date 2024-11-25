using AdventOfCode.Solutions.Library.Metadata;
using AdventOfCode.Solutions.Library;

namespace AdventOfCode.Solutions.Puzzles.Year2015.Day06.Part1.Ask
{
    public sealed class NormalCalculations : BaseSolution
    {
        public override SolutionMetadata Metadata => new(Year.Year2015, Day.Day06, Part.Part1, Author.Ask);

        public override Task<string> Solve(string input)
        {
            var idealLights = new HashSet<(int, int)>();

            foreach (var inputLine in input.Split('\n'))
            {
                (int command, int x1, int y1, int x2, int y2) = ReadingLine(inputLine.Trim());

                for (int x = x1; x <= x2; x++)
                {
                    for (int y = y1; y <= y2; y++)
                    {
                        var cords = (x, y);
                        switch (command)
                        {
                            case 0:
                                if (idealLights.Contains(cords))
                                    idealLights.Remove(cords);
                                else
                                    idealLights.Add(cords);
                                break;
                            case 1:
                                idealLights.Add(cords);
                                break;
                            case 2:
                                idealLights.Remove(cords);
                                break;
                        }
                    }
                }
            }

            return Task.FromResult(idealLights.Count.ToString());
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