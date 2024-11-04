using AdventOfCode.Solutions.Library.Metadata;
using AdventOfCode.Solutions.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Solutions.Puzzles.Year2015.Day05.Part1.Anna
{
    public class Solution : BaseSolution
    {
        public override SolutionMetadata Metadata => new(Year.Year2015, Day.Day05, Part.Part1, Author.Anna);

        public override Task<string> Solve(string input)
        {
            var niceStrings = 0;

            var vowels = new List<char> { 'a', 'e', 'i', 'o', 'u' };

            var lines = input.Split('\n');

            foreach (var line in lines)
            {
                if (line.Contains("ab"))
                    continue;
                if (line.Contains("cd"))
                    continue;
                if (line.Contains("pq"))
                    continue;
                if (line.Contains("xy"))
                    continue;

                var lineVowels = line.Count(vowels.Contains);
                if (lineVowels < 3)
                    continue;

                for (var i = 0; i < line.Count() - 1; i++)
                {
                    if (line[i] == line[i + 1])
                    {
                        niceStrings++;
                        break;
                    }
                }
            }

            return Task.FromResult(niceStrings.ToString());

        }
    }
}
