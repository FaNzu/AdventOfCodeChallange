using AdventOfCode.Solutions.Library.Metadata;
using AdventOfCode.Solutions.Library;

namespace AdventOfCode.Solutions.Puzzles.Year2015.Day03.Part1.Anna
{
    public class Solution : BaseSolution
    {
        public override SolutionMetadata Metadata => new(Year.Year2015, Day.Day03, Part.Part1, Author.Anna);

        public override Task<string> Solve(string input)
        {
            var x = 0; var y = 0;
            var locationsSeen = new HashSet<(int, int)>
            {
                (x, y)
            };

            foreach (var direction in input)
            {
                if (direction == '^')
                    y++;
                if (direction == 'v')
                    y--;
                if (direction == '>')
                    x++;
                if (direction == '<')
                    x--;

                locationsSeen.Add((x, y));
            }

            var distinctHousesVisited = locationsSeen.Count;

            return Task.FromResult(distinctHousesVisited.ToString());
        }
    }
}
