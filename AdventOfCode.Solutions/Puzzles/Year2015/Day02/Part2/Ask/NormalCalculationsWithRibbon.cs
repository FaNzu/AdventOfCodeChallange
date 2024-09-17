using System.Globalization;
using AdventOfCode.Solutions.Library;
using AdventOfCode.Solutions.Library.Metadata;

namespace AdventOfCode.Solutions.Puzzles.Year2015.Day02.Part2.Jose;

public sealed class WithSplitAndCalculatingInStruct : BaseSolution
{
    public override SolutionMetadata Metadata => new(Year.Year2015, Day.Day02, Part.Part2, Author.Ask);

    public override Task<string> Solve(string input)
    {
        var lines = input.Split("\r\n");

        var totalRibbon = 0;

        foreach (var line in lines)
        {
            var dimensions = line.Split('x');

            var box = new PresentBox(
                int.Parse(dimensions[0]),
                int.Parse(dimensions[1]),
                int.Parse(dimensions[2]));

            totalRibbon += box.RibbonLength + box.BowLength;
        }

        return Task.FromResult(totalRibbon.ToString(CultureInfo.InvariantCulture));
    }

    private readonly record struct PresentBox
    {
        public int RibbonLength { get; }

        public int BowLength { get; }

        public PresentBox(int length, int width, int height)
        {
            var dimensions = new int[] { length, width, height };
            Array.Sort(dimensions);

            RibbonLength = 2 * dimensions[0] + 2 * dimensions[1];
            BowLength = length * width * height;
        }
    }
}
