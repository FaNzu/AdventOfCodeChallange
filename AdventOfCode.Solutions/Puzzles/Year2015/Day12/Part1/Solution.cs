using AdventOfCode.Solutions.Library.Metadata;
using AdventOfCode.Solutions.Library;

namespace AdventOfCode.Solutions.Puzzles.Year2015.Day12.Part1.Anna
{
    public class Solution : BaseSolution
    {
        public override SolutionMetadata Metadata => new(Year.Year2015, Day.Day12, Part.Part1, Author.Anna);

        public override Task<string> Solve(string input)
        {
            var constants =
                input.Split('{').Where(s => s != "")
                .SelectMany(s => s.Split('}')).Where(s => s != "")
                .SelectMany(s => s.Split('[')).Where(s => s != "")
                .SelectMany(s => s.Split(']')).Where(s => s != "")
                .SelectMany(s => s.Split(':')).Where(s => s != "")
                .SelectMany(s => s.Split(',')).Where(s => s != "");

            var result = 0;
            foreach (var constant in constants)
            {
                int.TryParse(constant, out var number);
                result += number;
            }

            return Task.FromResult(result.ToString());
        }
    }
}
