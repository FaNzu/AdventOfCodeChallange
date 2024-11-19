using AdventOfCode.Solutions.Library.Metadata;
using AdventOfCode.Solutions.Library;

namespace AdventOfCode.Solutions.Puzzles.Year2015.Day06.Part1.Ask
{
    public sealed class NormalCalculations : BaseSolution
    {
        public override SolutionMetadata Metadata => new(Year.Year2015, Day.Day06, Part.Part1, Author.Ask);

        public override Task<string> Solve(string input)
        {
            var lines = input.Split("\r\n");

            var niceCount = 0;

            foreach (var line in lines)
            {
                if (IsNiceString(line))
                {
                    niceCount++;
                }
            }

            return Task.FromResult(niceCount.ToString());
        }
    }
}
