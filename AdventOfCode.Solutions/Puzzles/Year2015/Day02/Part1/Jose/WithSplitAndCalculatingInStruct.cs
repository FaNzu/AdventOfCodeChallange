using System.Globalization;
using AdventOfCode.Solutions.Library;
using AdventOfCode.Solutions.Library.Metadata;

namespace AdventOfCode.Solutions.Puzzles.Year2015.Day02.Part1.Jose;

public sealed class WithSplitAndCalculatingInStruct : BaseSolution
{
    public override SolutionMetadata Metadata => new(Year.Year2015, Day.Day02, Part.Part1, Author.Jose);

    public override Task<string> Solve(string input)
    {
        var lines = input.Split("\r\n");

        var wrappingPaperSize = 0;

        foreach (var line in lines)
        {
            var dimensions = line.Split('x');

            var present = new PresentBox(
                int.Parse(dimensions[0]),
                int.Parse(dimensions[1]),
                int.Parse(dimensions[2]));

            wrappingPaperSize += present.AreaOfAllSides + present.AreaOfSmallestSide;
        }

        return Task.FromResult(wrappingPaperSize.ToString(CultureInfo.InvariantCulture));
    }

    private readonly record struct PresentBox
    {
        public int AreaOfAllSides { get; }

        public int AreaOfSmallestSide { get; }

        public PresentBox(int length, int width, int height)
        {
            var side1 = length * width;
            var side2 = width * height;
            var side3 = height * length;

            AreaOfAllSides = 2 * side1 + 2 * side2 + 2 * side3;
            AreaOfSmallestSide = new int[] { side1, side2, side3 }.Min();
        }
    }
}
