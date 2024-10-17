using AdventOfCode.Solutions.Library;
using AdventOfCode.Solutions.Library.Metadata;

namespace AdventOfCode.Solutions.Puzzles.Year2015.Day03.Part2.Alice;

public sealed class WithHashSetAndSwitch : BaseSolution
{
    public override SolutionMetadata Metadata => new(Year.Year2015, Day.Day03, Part.Part2, Author.Alice);

    public override Task<string> Solve(string input)
    {
        int santaX = 0, santaY = 0;
        int roboSantaX = 0, roboSantaY = 0;

        var visitedHouses = new HashSet<(int, int)>();
        visitedHouses.Add((santaX, santaY));

        for (int i = 0; i < input.Length; i++)
        {
            var direction = input[i];
            if (i % 2 == 0) //even 0,2,4...
            {
                switch (direction)
                {
                    case '^':
                        santaY++;
                        break;
                    case 'v':
                        santaY--;
                        break;
                    case '>':
                        santaX++;
                        break;
                    case '<':
                        santaX--;
                        break;
                }

                visitedHouses.Add((santaX, santaY));
            }
            else
            {
                switch (direction)
                {
                    case '^':
                        roboSantaY++;
                        break;
                    case 'v':
                        roboSantaY--;
                        break;
                    case '>':
                        roboSantaX++;
                        break;
                    case '<':
                        roboSantaX--;
                        break;

                }

                visitedHouses.Add((roboSantaX, roboSantaY));
            }
        }

        return Task.FromResult(visitedHouses.Count.ToString());
    }
}