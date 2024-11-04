using AdventOfCode.Solutions.Library.Metadata;
using AdventOfCode.Solutions.Library;

namespace AdventOfCode.Solutions.Puzzles.Year2015.Day13.Part2.Anna
{
    public class Solution : BaseSolution
    {
        public override SolutionMetadata Metadata => new(Year.Year2015, Day.Day13, Part.Part2, Author.Anna);

        public override Task<string> Solve(string input)
        {
            var people = new HashSet<string>();
            var happinessValues = new Dictionary<(string, string), int>();

            foreach (var l in input.Split('\n'))
            {
                var line = l.Trim();
                var split = line.Split(' ');
                var person1 = split[0];
                var person2 = split[10].Remove(split[10].Length - 1);
                people.Add(person1);
                people.Add(person2);

                int happinessValue;
                if (split[2] == "gain")
                {
                    happinessValue = int.Parse(split[3]);
                }
                else
                {
                    happinessValue = -int.Parse(split[3]);
                }
                happinessValues.Add((person1, person2), happinessValue);
            }

            foreach (var person in people)
            {
                happinessValues.Add(("me", person), 0);
                happinessValues.Add((person, "me"), 0);
            }
            people.Add("me");

            var largestHappiness = int.MinValue;
            var permutations = GetPermutations(people.ToList());
            foreach (var permutation in permutations)
            {
                var currentHappiness = 0;
                for (var i = 0; i < permutation.Count - 1; i++)
                {
                    currentHappiness += happinessValues[(permutation[i], permutation[i + 1])];
                    currentHappiness += happinessValues[(permutation[i + 1], permutation[i])];
                }
                currentHappiness += happinessValues[(permutation[0], permutation[permutation.Count - 1])];
                currentHappiness += happinessValues[(permutation[permutation.Count - 1], permutation[0])];

                if (currentHappiness > largestHappiness)
                {
                    largestHappiness = currentHappiness;
                }
            }

            return Task.FromResult(largestHappiness.ToString());
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

