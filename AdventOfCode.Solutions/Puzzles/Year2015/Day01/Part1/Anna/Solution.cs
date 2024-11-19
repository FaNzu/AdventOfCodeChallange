using AdventOfCode.Solutions.Library;
using AdventOfCode.Solutions.Library.Metadata;

namespace AdventOfCode.Solutions.Puzzles.Year2015.Day01.Part1.Anna
{
    public class Solution : BaseSolution
    {
        public override SolutionMetadata Metadata => new(Year.Year2015, Day.Day01, Part.Part1, Author.Anna);

        public override Task<string> Solve(string input)
        {
            var floor = 0;

            foreach (var chr in input)
            {
                if (chr == '(')
                    floor++;
                else
                    floor--;
            }

            return Task.FromResult(floor.ToString());
        }
    }
}
