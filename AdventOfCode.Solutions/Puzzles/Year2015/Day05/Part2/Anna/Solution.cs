using AdventOfCode.Solutions.Library.Metadata;
using AdventOfCode.Solutions.Library;
using System.Text.RegularExpressions;

namespace AdventOfCode.Solutions.Puzzles.Year2015.Day05.Part2.Anna
{
    public class Solution : BaseSolution
    {
        public override SolutionMetadata Metadata => new(Year.Year2015, Day.Day05, Part.Part2, Author.Anna);

        public override Task<string> Solve(string input)
        {
            var niceStrings = 0;

            var lines = input.Split('\n');

            foreach (var l in lines)
            {
                var line = l.Trim();

                var hasTwoPairs = false;
                for (var i = 0; i < line.Count() - 1; i++)
                {
                    var pair = line[i].ToString() + line[i + 1].ToString();
                    var lineWithPairReplaced = line.Replace(pair, "XX");
                    var pairOccurrences = Regex.Matches(lineWithPairReplaced, "XX").Count;
                    if (pairOccurrences > 1)
                    {
                        hasTwoPairs = true;
                        break;
                    }
                }
                if (!hasTwoPairs)
                    continue;

                var repeatWithOneBetween = false;
                for (var i = 0; i < line.Count() - 2; i++)
                {
                    if (line[i].Equals(line[i + 2]))
                    {
                        repeatWithOneBetween = true;
                        break;
                    }
                }
                if (!repeatWithOneBetween)
                    continue;

                niceStrings++;
            }

            return Task.FromResult(niceStrings.ToString());
        }
    }
}
