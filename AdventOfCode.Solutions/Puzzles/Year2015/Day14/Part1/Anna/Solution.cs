using AdventOfCode.Solutions.Library;
using AdventOfCode.Solutions.Library.Metadata;

namespace AdventOfCode.Solutions.Puzzles.Year2015.Day14.Part1.Anna
{
    public class Solution : BaseSolution
    {
        public override SolutionMetadata Metadata => new(Year.Year2015, Day.Day14, Part.Part1, Author.Anna);

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

            var largestDistance = 0;

            foreach (var name in reindeer)
            {
                var remainingTime = 2503;
                var distance = 0;
                bool flying = true;
                var speed = speeds[name];
                var flyingTime = flyingTimes[name];
                var restingTime = restingTimes[name];

                while (remainingTime > 0)
                {
                    if (flying)
                    {
                        if (flyingTime > remainingTime)
                        {
                            distance += remainingTime * speed;
                        }
                        else
                        {
                            distance += flyingTime * speed;
                        }
                        remainingTime -= flyingTime;
                    }
                    else
                    {
                        remainingTime -= restingTime;
                    }
                    flying = !flying;
                }

                if (distance > largestDistance)
                    largestDistance = distance;
            }

            return Task.FromResult(largestDistance.ToString());
        }
    }
}
