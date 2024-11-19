using AdventOfCode.Solutions.Library.Metadata;
using AdventOfCode.Solutions.Library;

namespace AdventOfCode.Solutions.Puzzles.Year2015.Day02.Part2.Anna
{
    public class Solution : BaseSolution
    {
        public override SolutionMetadata Metadata => new(Year.Year2015, Day.Day02, Part.Part2, Author.Anna);

        public override Task<string> Solve(string input)
        {
            var ribbonLength = 0;

            var lines = input.Split('\n');

            foreach (var line in lines)
            {
                var dimensions = line.Split('x');
                var l = int.Parse(dimensions[0]);
                var w = int.Parse(dimensions[1]);
                var h = int.Parse(dimensions[2]);

                var wrappingLength = Math.Min(Math.Min(2 * (l + w), 2 * (l + h)), 2 * (w + h));

                var bowLength = l * w * h;

                ribbonLength += wrappingLength + bowLength;
            }

            return Task.FromResult(ribbonLength.ToString());
        }
    }
}
