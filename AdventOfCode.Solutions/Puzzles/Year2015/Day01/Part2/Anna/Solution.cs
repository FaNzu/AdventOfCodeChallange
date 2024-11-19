using AdventOfCode.Solutions.Library.Metadata;
using AdventOfCode.Solutions.Library;

namespace AdventOfCode.Solutions.Puzzles.Year2015.Day01.Part2.Anna
{
    public class Solution : BaseSolution
    {
        public override SolutionMetadata Metadata => new(Year.Year2015, Day.Day01, Part.Part2, Author.Anna);

        public override Task<string> Solve(string input)
        {
            var floor = 0;
            var chrNum = 1;

            foreach (var chr in input)
            {
                if (chr == '(')
                    floor++;
                else
                    floor--;

                if(floor < 0)
                    return Task.FromResult(chrNum.ToString());

                chrNum++;
            }
            
            return Task.FromResult(floor.ToString());
        }
    }
}
