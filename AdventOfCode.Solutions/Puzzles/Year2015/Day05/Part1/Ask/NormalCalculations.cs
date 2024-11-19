using AdventOfCode.Solutions.Library.Metadata;
using AdventOfCode.Solutions.Library;

namespace AdventOfCode.Solutions.Puzzles.Year2015.Day05.Part1.Ask
{
    public sealed class NormalCalculations : BaseSolution
    {
        public override SolutionMetadata Metadata => new(Year.Year2015, Day.Day05, Part.Part1, Author.Ask);

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
            return HasAtLeastThreeVowels(str) && HasDoubleLetter(str) && !HasDisallowedSubstrings(str);
        }

        private bool HasAtLeastThreeVowels(string str)
        {
            int vowelCount = str.Count(c => "aeiou".Contains(c));
            return vowelCount >= 3;
        }

        private bool HasDoubleLetter(string str)
        {
            for (int i = 0; i < str.Length - 1; i++)
            {
                if (str[i] == str[i + 1]) return true;
            }
            return false;
        }

        private bool HasDisallowedSubstrings(string str)
        {
            string[] disallowed = { "ab", "cd", "pq", "xy" };
            return disallowed.Any(sub => str.Contains(sub));
        }
    }
}
