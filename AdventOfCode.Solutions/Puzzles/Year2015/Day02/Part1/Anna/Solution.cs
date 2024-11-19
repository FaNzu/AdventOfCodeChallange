using AdventOfCode.Solutions.Library.Metadata;
using AdventOfCode.Solutions.Library;

namespace AdventOfCode.Solutions.Puzzles.Year2015.Day02.Part1.Anna
{
    public class Solution : BaseSolution
    {
        public override SolutionMetadata Metadata => new(Year.Year2015, Day.Day02, Part.Part1, Author.Anna);

        public override Task<string> Solve(string input)
        {
            var sqFeet = 0;

            var lines = input.Split('\n');

            foreach (var line in lines)
            {
                var dimensions = line.Split('x');
                var l = int.Parse(dimensions[0]);
                var w = int.Parse(dimensions[1]);
                var h = int.Parse(dimensions[2]);

                var surfaceArea = 2 * l * w + 2 * w * h + 2 * h * l;

                sqFeet += surfaceArea + Math.Min(Math.Min(l*w,l*h), w*h);
            }

            return Task.FromResult(sqFeet.ToString());
        }
    }
}
