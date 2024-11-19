using AdventOfCode.Solutions.Library;
using AdventOfCode.Solutions.Library.Metadata;

namespace AdventOfCode.Solutions.Puzzles.Year2015.Day16.Part2.Anna
{
    public class Solution : BaseSolution
    {
        public override SolutionMetadata Metadata => new(Year.Year2015, Day.Day16, Part.Part2, Author.Anna);

        public static Dictionary<string, int> RealSue =
            new Dictionary<string, int>
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

        public override Task<string> Solve(string input)
        {
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

                if (!CheckTrait(trait1, number1)) continue;
                if (!CheckTrait(trait2, number2)) continue;
                if (!CheckTrait(trait3, number3)) continue;

                return Task.FromResult(numberSue.ToString());
            }

            return Task.FromResult("No Sue found");
        }

        private bool CheckTrait(string trait, int number)
        {
            if (trait == "cats" || trait == "trees")
            {
                if (number <= RealSue[trait])
                { return false; }
            }
            else if (trait == "goldfish" || trait == "pomeranians")
            {
                if (number >= RealSue[trait])
                { return false; }
            }
            else
            {
                if (number != RealSue[trait])
                { return false; }
            }

            return true;
        }
    }
}

