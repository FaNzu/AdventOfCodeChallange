using AdventOfCode.Solutions.Library;
using AdventOfCode.Solutions.Library.Metadata;

namespace AdventOfCode.Solutions.Puzzles.Year2015.Day16.Part1.Anna
{
    public class Solution : BaseSolution
    {
        public override SolutionMetadata Metadata => new(Year.Year2015, Day.Day16, Part.Part1, Author.Anna);

        public override Task<string> Solve(string input)
        {
            var realSue = new Dictionary<string, int>
            {
                { "children", 3 },
                { "cats", 7 },
                { "samoyeds", 2 },
                { "pomeranians", 3 },
                { "akitas", 0 },
                { "vizslas", 0 },
                { "goldfish", 5 },
                { "trees", 3 },
                { "cars", 2 },
                { "perfumes", 1 },
            };

            foreach (var l in input.Split('\n'))
            {
                var split = l.Trim().Split(' ');
                var numberSue = int.Parse(split[1].Trim(':'));
                var trait1 = split[2].Trim(':');
                var number1 = int.Parse(split[3].Trim(','));
                var trait2 = split[4].Trim(':');
                var number2 = int.Parse(split[5].Trim(','));
                var trait3 = split[6].Trim(':');
                var number3 = int.Parse(split[7]);

                if (number1 != realSue[trait1]) continue;
                if (number2 != realSue[trait2]) continue;
                if (number3 != realSue[trait3]) continue;

                return Task.FromResult(numberSue.ToString());
            }

            return Task.FromResult("No Sue found");
        }
    }
}

