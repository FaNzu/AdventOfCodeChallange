using AdventOfCode.Solutions.Library;
using AdventOfCode.Solutions.Library.Metadata;

namespace AdventOfCode.Solutions.Puzzles.Year2015.Day24.Part1.Anna
{
    public class Solution : BaseSolution
    {
        public override SolutionMetadata Metadata => new(Year.Year2015, Day.Day24, Part.Part1, Author.Anna);

        public override Task<string> Solve(string input)
        {
            var packages = new List<int>();
            foreach (var line in input.Split('\n'))
            {
                packages.Add(int.Parse(line.Trim()));
            }

            var groupSum = packages.Sum() / 3;
            var firstGroups = ChooseForSum(packages, groupSum).OrderBy(s => s.Count);
            var smallestGroupSize = firstGroups.First().Count;

            var smallestQE = long.MaxValue;
            foreach (var group in firstGroups.Where(g => g.Count == smallestGroupSize))
            {
                long quantumEntanglement = 1;
                foreach (var package in group)
                {
                    quantumEntanglement *= package;
                }
                if(quantumEntanglement < smallestQE)
                {
                    smallestQE = quantumEntanglement;
                }
            }

            return Task.FromResult(smallestQE.ToString());
        }

        private static List<HashSet<int>> ChooseForSum(List<int> packages, int sum)
        {
            if(sum == 0)
            { return [[]]; }
            if (packages.Count == 0)
            { return []; }

            var result = new List<HashSet<int>>();
            var firstPackage = packages.First();

            var setsWithoutFirst = ChooseForSum(packages.Skip(1).ToList(), sum);
            result.AddRange(setsWithoutFirst);

            if(firstPackage > sum)
            {
                
            }
            else if(firstPackage == sum)
            {
                result.Add([firstPackage]);
            }
            else
            {
                var setsWithFirst = ChooseForSum(packages.Skip(1).ToList(), sum - firstPackage);
                setsWithFirst.ForEach(set => { set.Add(firstPackage); });
                result.AddRange(setsWithFirst);
            }
            
            return result;
        }
    }
}
