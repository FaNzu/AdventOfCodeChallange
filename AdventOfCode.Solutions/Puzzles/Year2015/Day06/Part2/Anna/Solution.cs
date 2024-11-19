using AdventOfCode.Solutions.Library.Metadata;
using AdventOfCode.Solutions.Library;

namespace AdventOfCode.Solutions.Puzzles.Year2015.Day06.Part2.Anna
{
    public class Solution : BaseSolution
    {
        public override SolutionMetadata Metadata => new(Year.Year2015, Day.Day06, Part.Part2, Author.Anna);

        public override Task<string> Solve(string input)
        {
            var lights = new Dictionary<(int, int), int>();
            for (var x = 0; x <= 999; x++)
            {
                for (var y = 0; y <= 999; y++)
                {
                    lights.Add((x, y), 0);
                }
            }

            foreach (var l in input.Split('\n'))
            {
                var line = l.Trim();

                (int lowX, int lowY, int highX, int highY) coordinates = GetCoordinates(line);

                if (line.Contains("turn on"))
                {
                    for (int x = coordinates.lowX; x <= coordinates.highX; x++)
                    {
                        for (int y = coordinates.lowY; y <= coordinates.highY; y++)
                        {
                            lights[(x, y)] += 1;
                        }
                    }
                }

                if (line.Contains("turn off"))
                {
                    for (int x = coordinates.lowX; x <= coordinates.highX; x++)
                    {
                        for (int y = coordinates.lowY; y <= coordinates.highY; y++)
                        {
                            lights[(x, y)] -= 1;
                            if (lights[(x,y)] < 0)
                                lights[(x,y)] = 0;
                        }
                    }
                }

                if (line.Contains("toggle"))
                {
                    for (int x = coordinates.lowX; x <= coordinates.highX; x++)
                    {
                        for (int y = coordinates.lowY; y <= coordinates.highY; y++)
                        {
                            lights[(x, y)] += 2;
                        }
                    }
                }
            }

            var totalBrightness = lights.Values.Sum();
            return Task.FromResult(totalBrightness.ToString());
        }

        private (int, int, int, int) GetCoordinates(string line)
        {
            var split1 = line.Split(" ");
            var leftSide = "";
            var rightSide = "";
            if (split1.Length == 4)
            {
                leftSide = split1[1];
                rightSide = split1[3];
            }
            else
            {
                leftSide = split1[2];
                rightSide = split1[4];
            }

            var leftSplit = leftSide.Split(',');
            var lowX = int.Parse(leftSplit[0]);
            var lowY = int.Parse(leftSplit[1]);

            var rightSplit = rightSide.Split(',');
            var highX = int.Parse(rightSplit[0]);
            var highY = int.Parse(rightSplit[1]);

            return (lowX, lowY, highX, highY);
        }
    }
}
