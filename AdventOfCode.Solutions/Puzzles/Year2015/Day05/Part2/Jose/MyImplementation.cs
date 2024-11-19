using AdventOfCode.Solutions.Library;
using AdventOfCode.Solutions.Library.Metadata;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AdventOfCode.Solutions.Puzzles.Year2015.Day05.Part2.Jose;

public sealed class MyImplementation : BaseSolution
{
    public override SolutionMetadata Metadata => new(Year.Year2015, Day.Day05, Part.Part2, Author.Jose);

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
        var groupsOf2 = GetStringGroupings(line, 2)
            .Where(x => x.Length == 2)
            .Select((value, index) => new StringTwo(value, index))
            .ToList();

        var groupsOf3 = GetStringGroupings(line, 3)
            .Where(x => x.Length == 3)
            .Select(x => new StringThree(x))
            .ToList();

        var req1 = HasPairRepeating(groupsOf2);
        var req2 = HasTripletWithRepeatingWithLetterInBetween(groupsOf3);

        return req1 && req2;
    }

    private IEnumerable<string> GetStringGroupings(string value, int count)
    {
        var index = 0;

        while (index < value.Length)
        {
            if (value.Length - index >= count)
            {
                yield return value.Substring(index, count);
            }
            else
            {
                yield return value.Substring(index, value.Length - index);
            }

            index++;
        }
    }

    private bool HasPairRepeating(IReadOnlyList<StringTwo> items)
    {
        var data = new Dictionary<int, string>();

        foreach (var item in items)
        {
            if (IsFoundInStringButNotOverlapping(item, data))
            {
                return true;
            }

            data.Add(item.Index, item.Value);
        }

        return false;
    }

    private bool IsFoundInStringButNotOverlapping(StringTwo item, IReadOnlyDictionary<int, string> existingItems)
    {
        var repeatingItems = existingItems.Where(x => x.Value == item.Value).ToList();

        if (!repeatingItems.Any())
        {
            return false;
        }

        return repeatingItems.Any(x => Math.Abs(x.Key - item.Index) > 1);
    }

    private bool HasTripletWithRepeatingWithLetterInBetween(IReadOnlyList<StringThree> items)
    {
        return items.Any(x => x.RepeatsWithLetterInBetween);
    }

    private readonly record struct StringTwo
    {
        public string Value { get; }

        public int Index { get; }

        public StringTwo(string value, int index)
        {
            if (value.Length != 2)
            {
                throw new ArgumentException("Value must have a length of 2.");
            }

            Value = value;
            Index = index;
        }
    }

    private readonly record struct StringThree
    {
        public string Value { get; }

        public bool RepeatsWithLetterInBetween => Value[0] == Value[2];

        public StringThree(string value)
        {
            if (value.Length != 3)
            {
                throw new ArgumentException("Value must have a length of 3.");
            }

            Value = value;
        }
    }
}
