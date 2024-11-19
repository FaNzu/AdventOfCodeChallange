using AdventOfCode.Solutions.Library.Metadata;
using AdventOfCode.Solutions.Library;

namespace AdventOfCode.Solutions.Puzzles.Year2015.Day05.Part2.Ask
{
    public sealed class NormalCalculations : BaseSolution
    {
        public override SolutionMetadata Metadata => new(Year.Year2015, Day.Day05, Part.Part2, Author.Ask);

        public override Task<string> Solve(string input)
        {
            var lines = input.Split("\r\n");

            var niceCount = 0;

            foreach (var line in lines)
            {
                if (IsNiceString(line))
                {
                    niceCount++;
                }
            }

            return Task.FromResult(niceCount.ToString());
        }

        private bool IsNiceString(string str)
        {
            return HasNonOverlappingPair(str) && HasRepeatingLetterWithGap(str);
        }

        private bool HasNonOverlappingPair(string str)
        {
            for (int i = 0; i < str.Length - 1; i++)
            {
                string pair = str.Substring(i, 2);
                if (str.IndexOf(pair, i + 2) != -1)
                {
                    return true;
                }
            }
            return false;
        }

        private bool HasRepeatingLetterWithGap(string str)
        {
            for (int i = 0; i < str.Length - 2; i++)
            {
                if (str[i] == str[i + 2])
                {
                    return true;
                }
            }
            return false;
        }
    }
}