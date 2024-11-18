using AdventOfCode.Solutions.Library;
using AdventOfCode.Solutions.Library.Metadata;

namespace AdventOfCode.Solutions.Puzzles.Year2015.Day05.Part1.Jose;

public sealed class MyImplementation : BaseSolution
{
    public override SolutionMetadata Metadata => new(Year.Year2015, Day.Day05, Part.Part1, Author.Jose);

    public override Task<string> Solve(string input)
    {
        var lines = input.Split("\r\n");

        var niceCount = 0;

        foreach (var line in lines)
        {
            if (IsNice(line))
            {
                niceCount++;
            }
        }

        return Task.FromResult(niceCount.ToString());
    }

    private bool IsNice(string line)
    {
        if (HasForbiddenSubstrings(line))
        {
            return false;
        }

        var previousCharacter = '\0';
        var vowelCount = 0;

        var hasTwoConsecutives = false;
        var hasThreeVowels = false;

        foreach (char current in line)
        {
            CheckAndUpdateHasTwoConsecutives(current, ref previousCharacter, ref hasTwoConsecutives);
            CheckAndUpdateHasThreeVowels(current, ref vowelCount, ref hasThreeVowels);

            if (hasTwoConsecutives && hasThreeVowels)
            {
                return true;
            }
        }

        return false;
    }

    private bool HasForbiddenSubstrings(string line)
    {
        return line.Contains("ab")
            || line.Contains("cd")
            || line.Contains("pq")
            || line.Contains("xy");
    }

    private void CheckAndUpdateHasTwoConsecutives(char current, ref char previous, ref bool hasTwoConsecutives)
    {
        var oldPrevious = previous;
        previous = current;

        if (!hasTwoConsecutives)
        {
            hasTwoConsecutives = current == oldPrevious;
        }
    }

    private void CheckAndUpdateHasThreeVowels(char current, ref int count, ref bool hasThreeVowels)
    {
        if (IsVowel(current))
        {
            count++;
        }

        if (!hasThreeVowels)
        {
            hasThreeVowels = count == 3;
        }
    }

    private bool IsVowel(char character)
    {
        return character == 'a'
            || character == 'e'
            || character == 'i'
            || character == 'o'
            || character == 'u';
    }
}
