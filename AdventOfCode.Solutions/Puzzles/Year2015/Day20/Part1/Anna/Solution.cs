using AdventOfCode.Solutions.Library.Metadata;
using AdventOfCode.Solutions.Library;
using AdventOfCode.Solutions.Puzzles.Year2015.Day20.Input;

namespace AdventOfCode.Solutions.Puzzles.Year2015.Day20.Part1.Anna
{
    public class Solution : BaseSolution
    {
        public override SolutionMetadata Metadata => new(Year.Year2015, Day.Day20, Part.Part1, Author.Anna);

        public Dictionary<string, HashSet<string>> replacements = new Dictionary<string, HashSet<string>>();

        public override Task<string> Solve(string input)
        {
            var presents = int.Parse(input.Trim());

            for (var i = 1; i <= presents; i++)
            {
                var factorSum = SumOfFactors(i);
                if (10 * factorSum >= presents)
                { return Task.FromResult(i.ToString()); }
            }

            return Task.FromResult(0.ToString());
        }

        private long SumOfFactors(int number)
        {
            long product = 1;
            Dictionary<long, long> primeFactors = FindPrimeFactors(number);
            foreach (var (p, exp) in primeFactors)
            {
                var factor = ((long)Math.Pow(p, exp + 1) - 1) / (p - 1);
                product *= factor;
            }

            return product;
        }

        private Dictionary<long, long> FindPrimeFactors(int number)
        {
            var result = new Dictionary<long, long>();

            foreach (var p in PrimeNumbers.PrimesUnder1Million)
            {
                if (number == 1) { return result; }

                if (number % p == 0)
                {
                    result.Add(p, 0);
                    while (number % p == 0)
                    {
                        result[p] += 1;
                        number /= p;
                    }
                }
            }

            if (number == 1) { return result; }

            return result;
        }
    }
}
