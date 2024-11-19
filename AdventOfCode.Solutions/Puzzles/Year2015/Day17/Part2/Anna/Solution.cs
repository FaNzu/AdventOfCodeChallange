using AdventOfCode.Solutions.Library;
using AdventOfCode.Solutions.Library.Metadata;

namespace AdventOfCode.Solutions.Puzzles.Year2015.Day17.Part2.Anna
{
    public class Solution : BaseSolution
    {
        public override SolutionMetadata Metadata => new(Year.Year2015, Day.Day17, Part.Part2, Author.Anna);

        public override Task<string> Solve(string input)
        {
            var containers = new List<int>();
            foreach (var l in input.Split('\n'))
            {
                var container = int.Parse(l.Trim());
                containers.Add(container);
            }

            var combinations = GetAllCombinations(containers, 150);
            var minimum = combinations.OrderBy(l => l.Count).First().Count;
            var result = combinations.Count(l => l.Count == minimum);

            return Task.FromResult(result.ToString());
        }

        private List<List<int>> GetAllCombinations(List<int> containers, int total)
        {
            if (total < 0)
            {
                return [];
            }
            if (total == 0)
            {
                return [[]];
            }
            if (containers.Count == 1)
            {
                if (containers[0] == total)
                {
                    return new List<List<int>> { new List<int> { total } };
                }
                else
                {
                    return [];
                }
            }

            var firstContainer = containers[0];
            var combinationsWithoutFirst = GetAllCombinations(containers[1..], total);
            var combinationsWithFirst = GetAllCombinations(containers[1..], total - firstContainer);
            combinationsWithFirst.ForEach(combination => combination.Add(firstContainer));

            return combinationsWithoutFirst.Concat(combinationsWithFirst).ToList();
        }
    }
}

