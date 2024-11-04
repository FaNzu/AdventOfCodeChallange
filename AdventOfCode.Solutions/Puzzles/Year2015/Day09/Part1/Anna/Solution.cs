using AdventOfCode.Solutions.Library.Metadata;
using AdventOfCode.Solutions.Library;

namespace AdventOfCode.Solutions.Puzzles.Year2015.Day09.Part1.Anna
{
    public class Solution : BaseSolution
    {
        public override SolutionMetadata Metadata => new(Year.Year2015, Day.Day09, Part.Part1, Author.Anna);

        public override Task<string> Solve(string input)
        {
            var distances = new Dictionary<(string, string), int>();
            var cities = new HashSet<string>();

            foreach (var l in input.Split('\n'))
            {
                var line = l.Trim();

                var split = line.Split(' ');
                var city1 = split[0];
                var city2 = split[2];
                var distance = int.Parse(split[4]);

                distances.Add((city1, city2), distance);
                distances.Add((city2, city1), distance);

                cities.Add(city1);
                cities.Add(city2);
            }

            var permutations = GetPermutations(cities.ToList());
            var smallestDistance = int.MaxValue;
            foreach (var permutation in permutations)
            {
                var distanceOfPermutation = 0;
                for (int i = 0; i < permutation.Count - 1; i++)
                {
                    distanceOfPermutation += distances[(permutation[i], permutation[i + 1])];
                }

                if (distanceOfPermutation < smallestDistance)
                    smallestDistance = distanceOfPermutation;
            }

            return Task.FromResult(smallestDistance.ToString());
        }

        public List<List<string>> GetPermutations(List<string> list)
        {
            if (list.Count <= 1) return new List<List<string>> { list };

            var lastItem = list[list.Count - 1];
            list.RemoveAt(list.Count - 1); 
            var permutationsExceptLast = GetPermutations(list);

            var returnPerms = permutationsExceptLast.SelectMany(l =>
            {
                var lWithLastItem = new List<List<string>>();

                for (int i = 0; i <= l.Count; i++)
                {
                    var clonedList = CloneList(l);
                    clonedList.Insert(i, lastItem);
                    lWithLastItem.Add(clonedList);
                }

                return lWithLastItem;
            }).ToList();

            return returnPerms;
        }

        public List<string> CloneList(List<string> list)
        {
            var resultList = new List<string>();
            foreach (var item in list)
            {
                resultList.Add(item);
            }
            return resultList;
        }
    }
}
