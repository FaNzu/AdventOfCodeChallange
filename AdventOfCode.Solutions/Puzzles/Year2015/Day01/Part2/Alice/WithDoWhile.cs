using AdventOfCode.Solutions.Library;
using AdventOfCode.Solutions.Library.Metadata;

namespace AdventOfCode.Solutions.Puzzles.Year2015.Day01.Part2.Alice;

public class WithDoWhile : BaseSolution
{
    public override SolutionMetadata Metadata => new(Year.Year2015, Day.Day01, Part.Part2, Author.Alice);

    public override Task<string> Solve(string input)
    {
        int position = 0;
        int positionIndex = 0;
        int floor = 0;

        do
        {
            char c = input[positionIndex];
            position++;

            if (c == '(')
            {
                floor++;
            }
            else if (c == ')')
            {
                floor--;
            }

            positionIndex++;

        } while (floor != -1 && positionIndex < input.Length);

        return Task.FromResult(position.ToString());
    }
}