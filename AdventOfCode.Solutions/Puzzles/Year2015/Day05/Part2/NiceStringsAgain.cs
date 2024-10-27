using AdventOfCode.Solutions.Library;
using AdventOfCode.Solutions.Library.Metadata;

namespace AdventOfCode.Solutions.Puzzles.Year2015.Day05.Part2;

public sealed class NiceStringsAgain : BaseSolution
{
    public override SolutionMetadata Metadata => new(Year.Year2015, Day.Day05, Part.Part2, Author.Alice);

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
        bool hasRepeatingPair = false;
        bool hasRepeatingLetterWithOneBetween = false;

        for (int i = 0; i < input.Length - 1; i++)
        {

            char firstChar = input[i];
            char secondChar = input[i + 1];


            for (int j = i + 2; j < input.Length - 1; j++)
            {

                if (input[j] == firstChar && input[j + 1] == secondChar)
                {
                    hasRepeatingPair = true;
                    break;
                }

            }
        }

        for (int i = 0; i < input.Length - 2; i++)
        {
            if (input[i] == input[i + 2])
            {
                hasRepeatingLetterWithOneBetween = true;
                break;
            }
        }

        return hasRepeatingPair && hasRepeatingLetterWithOneBetween;
    }
}
