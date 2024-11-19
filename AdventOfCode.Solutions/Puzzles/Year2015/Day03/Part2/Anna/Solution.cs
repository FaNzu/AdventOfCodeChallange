using AdventOfCode.Solutions.Library.Metadata;
using AdventOfCode.Solutions.Library;

namespace AdventOfCode.Solutions.Puzzles.Year2015.Day03.Part2.Anna
{
    public class Solution : BaseSolution
    {
        public override SolutionMetadata Metadata => new(Year.Year2015, Day.Day03, Part.Part2, Author.Anna);

        public override Task<string> Solve(string input)
        {
            var instructionsSanta = input.Where((_, index) => index % 2 == 0);
            var instructionsRobo = input.Where((_, index) => index % 2 == 1);

            var xSanta = 0; var ySanta = 0;
            var xRobo = 0; var yRobo = 0;

            var locationsSeen = new HashSet<(int, int)>
            {
                (xSanta, ySanta)
            };

            foreach (var direction in instructionsSanta)
            {
                if (direction == '^')
                    ySanta++;
                if (direction == 'v')
                    ySanta--;
                if (direction == '>')
                    xSanta++;
                if (direction == '<')
                    xSanta--;

                locationsSeen.Add((xSanta, ySanta));
            }

            foreach (var direction in instructionsRobo)
            {
                if (direction == '^')
                    yRobo++;
                if (direction == 'v')
                    yRobo--;
                if (direction == '>')
                    xRobo++;
                if (direction == '<')
                    xRobo--;

                locationsSeen.Add((xRobo, yRobo));
            }

            var distinctHousesVisited = locationsSeen.Count;

            return Task.FromResult(distinctHousesVisited.ToString());
        }
    }
}
