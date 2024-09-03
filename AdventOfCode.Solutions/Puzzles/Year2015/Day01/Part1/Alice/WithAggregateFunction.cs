using AdventOfCode.Solutions.Library;
using AdventOfCode.Solutions.Library.Metadata;

namespace AdventOfCode.Solutions.Puzzles.Year2015.Day01.Part1.Alice;

    public class WithAggregateFunction : BaseSolution
    {
        public override SolutionMetadata Metadata => new(Year.Year2015, Day.Day01, Part.Part1, Author.Alice);

        public override Task<string> Solve(string input)
        {
            int floor = input.Aggregate(0, (currentFloor, c) =>
            {
                if (c == '(')
                {
                    return currentFloor + 1;
                }
                else if (c == ')')
                {
                    return currentFloor - 1;
                }
                return currentFloor;
            });

            return Task.FromResult(floor.ToString());
        }
    }