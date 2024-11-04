using AdventOfCode.Solutions.Library.Metadata;
using AdventOfCode.Solutions.Library;

namespace AdventOfCode.Solutions.Puzzles.Year2015.Day18.Part1
{
    public class Solution : BaseSolution
    {
        public override SolutionMetadata Metadata => new(Year.Year2015, Day.Day18, Part.Part1, Author.Anna);

        public Dictionary<(int, int), bool> lights = [];
        public int width = 0;
        public int height = 0;

        public override Task<string> Solve(string input)
        {
            var lines = input.Split('\n');
            width = lines.Length;
            height = lines[0].Trim().Length;
            for (int i = 0; i < width; i++)
            {
                var line = lines[i].Trim();
                for (int j = 0; j < height; j++)
                {
                    var initialState = line[j];
                    if (initialState == '#')
                    {
                        lights.Add((i, j), true);
                    }
                    else if (initialState == '.')
                    {
                        lights.Add((i, j), false);
                    }
                }
            }

            for (int update = 1; update <= 100; update++)
            {
                UpdateLights();
            }
            
            var result = lights.Count(kv => kv.Value);
            return Task.FromResult(result.ToString());
        }

        private void UpdateLights()
        {
            var newLights = new Dictionary<(int, int), bool>();
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    var newLight = UpdateLight(i, j);
                    newLights.Add((i,j), newLight);
                }
            }
            lights = newLights;
        }

        private bool UpdateLight(int i, int j)
        {
            var currentState = lights[(i, j)];
            bool newState = false;

            var neighborsOn = CountNeighborsOn(i, j);

            if(currentState)
            {
                if (neighborsOn == 2 || neighborsOn == 3)
                {
                    newState = true;
                }
            }
            else
            {
                if (neighborsOn == 3)
                {
                    newState = true;
                }
            }

            return newState;
        }

        private int CountNeighborsOn(int i, int j)
        {
            var neighborsOn = 0;
            for(int p = i-1; p <= i+1; p++)
            {
                for (int q = j-1; q <= j+1; q++)
                {
                    if(p == i && q == j)
                    {
                        continue;
                    }
                    if(lights.ContainsKey((p, q)))
                    {
                        if (lights[(p,q)])
                        {
                            neighborsOn++;
                        }
                    }
                }
            }

            return neighborsOn;
        }
    }
}
