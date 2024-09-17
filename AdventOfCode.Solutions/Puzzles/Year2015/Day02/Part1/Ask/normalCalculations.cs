using System.Globalization;
using AdventOfCode.Solutions.Library;
using AdventOfCode.Solutions.Library.Metadata;

namespace AdventOfCode.Solutions.Puzzles.Year2015.Day02.Part1.Ask;

public sealed class NormalCalculations : BaseSolution
{
    public override SolutionMetadata Metadata => new(Year.Year2015, Day.Day02, Part.Part1, Author.Ask);

    public override Task<string> Solve(string input)
    {
        var lines = input.Split("\r\n");

        var totalPaper = 0;

        foreach (var line in lines)
        {
            var sides = line.Split('x');

            var box = new PresentBox(
                int.Parse(sides[0]),
                int.Parse(sides[1]),
                int.Parse(sides[2]));

            totalPaper += box.BoxAreal + box.SmallArea;
        }

        return Task.FromResult(totalPaper.ToString());
    }

    private readonly record struct PresentBox
    {
        public int BoxAreal { get; }

        public int SmallArea { get; }

        public PresentBox(int length, int width, int height)
        {
            var side1 = length * width;
            var side2 = width * height;
            var side3 = height * length;

            BoxAreal = 2 * side1 + 2 * side2 + 2 * side3;
            SmallArea = new int[] { side1, side2, side3 }.Min();
        }
    }
}
