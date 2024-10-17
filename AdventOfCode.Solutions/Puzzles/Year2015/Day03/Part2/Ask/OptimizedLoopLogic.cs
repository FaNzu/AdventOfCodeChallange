using AdventOfCode.Solutions.Library.Metadata;
using AdventOfCode.Solutions.Library;

namespace AdventOfCode.Solutions.Puzzles.Year2015.Day03.Part2.Ask
{
    public sealed class OptimizedLoopLogic : BaseSolution
    {
        public override SolutionMetadata Metadata => new(Year.Year2015, Day.Day03, Part.Part2, Author.Ask);

        public override Task<string> Solve(string input)
        {
            var answer = CountHousesWithRobot(input).ToString();
            return Task.FromResult(answer);
        }

        private int CountHousesWithRobot(string moves)
        {
            var houses = new HashSet<(int, int)>();
            int santaX = 0, santaY = 0;
            int robotX = 0, robotY = 0;
            houses.Add((0, 0));

            for (int i = 0; i < moves.Length; i++)
            {
                if (i % 2 == 0)
                {
                    // Santa's turn
                    switch (moves[i])
                    {
                        case '>': santaX++; break;
                        case '<': santaX--; break;
                        case '^': santaY++; break;
                        case 'v': santaY--; break;
                    }
                    houses.Add((santaX, santaY));
                }
                else
                {
                    // Robot's turn
                    switch (moves[i])
                    {
                        case '>': robotX++; break;
                        case '<': robotX--; break;
                        case '^': robotY++; break;
                        case 'v': robotY--; break;
                    }
                    houses.Add((robotX, robotY));
                }
            }

            return houses.Count;
        }
    }
}
