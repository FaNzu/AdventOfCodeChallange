using AdventOfCode.Solutions.Library.Metadata;
using AdventOfCode.Solutions.Library;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace AdventOfCode.Solutions.Puzzles.Year2015.Day08.Part1.Anna
{
    public class Solution : BaseSolution
    {
        public override SolutionMetadata Metadata => new(Year.Year2015, Day.Day08, Part.Part1, Author.Anna);

        public override Task<string> Solve(string input)
        {
            var codeLength = 0;
            var stringLength = 0;

            foreach (var l in input.Split('\n'))
            {
                var line = l.Trim();

                codeLength += line.Length;

                var stringLine = line.Substring(1, line.Length - 2);
                stringLine = Regex.Replace(stringLine, Regex.Escape("\\\""), "Q");
                stringLine = Regex.Replace(stringLine, Regex.Escape("\\\\"), "Q");
                stringLine = Regex.Replace(stringLine, Regex.Escape("\\x") + "\\w\\w", "Q");
                stringLength += stringLine.Length;
            }

            var result = codeLength - stringLength;
            return Task.FromResult(result.ToString());
        }
    }
}
