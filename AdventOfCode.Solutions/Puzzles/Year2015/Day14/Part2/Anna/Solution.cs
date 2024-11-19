using AdventOfCode.Solutions.Library;
using AdventOfCode.Solutions.Library.Metadata;

namespace AdventOfCode.Solutions.Puzzles.Year2015.Day14.Part2.Anna
{
    public class Solution : BaseSolution
    {
        public override SolutionMetadata Metadata => new(Year.Year2015, Day.Day14, Part.Part2, Author.Anna);

        public override Task<string> Solve(string input)
        {
            var reindeer = new HashSet<string>();
            var speeds = new Dictionary<string, int>();
            var flyingTimes = new Dictionary<string, int>();
            var restingTimes = new Dictionary<string, int>();

            foreach (var l in input.Split('\n'))
            {
                var split = l.Trim().Split(' ');
                var name = split[0];
                var speed = int.Parse(split[3]);
                var flyingTime = int.Parse(split[6]);
                var restingTime = int.Parse(split[13]);

                reindeer.Add(name);
                speeds.Add(name, speed);
                flyingTimes.Add(name, flyingTime);
                restingTimes.Add(name, restingTime);
            }

            var points = new Dictionary<string, int>();
            var distances = new Dictionary<string, int>();
            var flying = new Dictionary<string, bool>();
            var remaining = new Dictionary<string, int>();
            foreach (var name in reindeer)
            {
                points.Add(name, 0);
                distances.Add(name, 0);
                flying.Add(name, true);
                remaining.Add(name, flyingTimes[name]);
            }

            for (var i = 1; i <= 2503; i++)
            {
                foreach (var name in reindeer)
                {
                    if (flying[name])
                    {
                        distances[name] += speeds[name];
                    }

                    remaining[name] -= 1;
                    if (remaining[name] == 0)
                    {
                        flying[name] = !flying[name];
                        if(flying[name])
                        {
                            remaining[name] = flyingTimes[name];
                        }
                        else
                        {
                            remaining[name] = restingTimes[name];
                        }
                    }
                }

                var maxDistance = distances.Values.Max();
                var winners = distances.Where((kv) => kv.Value == maxDistance).Select((kv) => kv.Key);
                foreach (var name in winners)
                {
                    points[name] += 1;
                }
            }

            var largestDistance = points.Values.Max();

            return Task.FromResult(largestDistance.ToString());
        }
    }
}
