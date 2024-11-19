using AdventOfCode.Solutions.Library;
using AdventOfCode.Solutions.Library.Metadata;

namespace AdventOfCode.Solutions.Puzzles.Year2015.Day05.Part1.Alice;

public sealed class NiceString : BaseSolution
{
    public override SolutionMetadata Metadata => new(Year.Year2015, Day.Day05, Part.Part1, Author.Alice);

    public override Task<string> Solve(string input)
    {
        int niceStrings = 0;

        var lines = input.Split("\r\n");

        foreach (var line in lines)
        {
            if (IsNiceString(line))
            {
                niceStrings++;
            }
        }

        return Task.FromResult(niceStrings.ToString());
    }

    public static bool IsNiceString(string input)
    {
        int vowelCount = 0;
        bool hasDoubleLetter = false;
        bool hasForbiddenSubstring = false;

        foreach (char c in input)
        {
            if ("aeiou".Contains(c))
            {
                vowelCount++;
            }
        }

        for (int i = 0; i < input.Length - 1; i++)
        {

            if (input[i] == input[i + 1])
            {
                hasDoubleLetter = true;
            }

            if (input.Substring(i, 2) == "ab" || input.Substring(i, 2) == "cd" ||
                input.Substring(i, 2) == "pq" || input.Substring(i, 2) == "xy")
            {
                hasForbiddenSubstring = true;
                break;
            }
        }

        return vowelCount >= 3 && hasDoubleLetter && !hasForbiddenSubstring;
    }
}