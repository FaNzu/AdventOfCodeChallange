using AdventOfCode.Solutions.Library.Metadata;
using AdventOfCode.Solutions.Library;
using System.Text.RegularExpressions;

namespace AdventOfCode.Solutions.Puzzles.Year2015.Day08.Part2.Anna
{
    public class Solution : BaseSolution
    {
        public override SolutionMetadata Metadata => new(Year.Year2015, Day.Day08, Part.Part2, Author.Anna);

        public override Task<string> Solve(string input)
        {
            var codeLength = 0;
            var encodingLength = 0;

            foreach (var l in input.Split('\n'))
            {
                var line = l.Trim();

                codeLength += line.Length;

                var encodingLine = "QQ" + line + "QQ";
                encodingLine = Regex.Replace(encodingLine, Regex.Escape("\\\""), "QQQQ");
                encodingLine = Regex.Replace(encodingLine, Regex.Escape("\\\\"), "QQQQ");
                encodingLine = Regex.Replace(encodingLine, Regex.Escape("\\x") + "\\w\\w", "QQQQQ");
                encodingLength += encodingLine.Length;
            }

            var result = encodingLength - codeLength;
            return Task.FromResult(result.ToString());
        }
    }
}
