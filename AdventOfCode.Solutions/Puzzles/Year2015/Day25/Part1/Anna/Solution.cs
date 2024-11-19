using AdventOfCode.Solutions.Library;
using AdventOfCode.Solutions.Library.Metadata;

namespace AdventOfCode.Solutions.Puzzles.Year2015.Day25.Part1.Anna
{
    public class Solution : BaseSolution
    {
        public override SolutionMetadata Metadata => new(Year.Year2015, Day.Day25, Part.Part1, Author.Anna);

        public override Task<string> Solve(string input)
        {
            var split = input.Split(' ');
            var row = int.Parse(split[16].TrimEnd(','));
            var column = int.Parse(split[18].TrimEnd('.')); ;

            var generationNumber = 1;
            for (var i = 0; i < row; i++)
            {
                generationNumber += i;
            }
            for (var i = 1; i < column; i++)
            {
                generationNumber += i + row;
            }

            ulong code = 20151125;
            for (var i = 2; i <= generationNumber; i++)
            {
                var mult = code * 252533;
                code = mult % 33554393;
            }

            return Task.FromResult(code.ToString());
        }
    }
}
