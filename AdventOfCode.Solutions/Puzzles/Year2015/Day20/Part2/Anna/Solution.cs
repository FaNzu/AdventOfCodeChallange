using AdventOfCode.Solutions.Library.Metadata;
using AdventOfCode.Solutions.Library;
using AdventOfCode.Solutions.Puzzles.Year2015.Day20.Input;
using QuikGraph.Algorithms;

namespace AdventOfCode.Solutions.Puzzles.Year2015.Day20.Part2.Anna
{
    public class Solution : BaseSolution
    {
        public override SolutionMetadata Metadata => new(Year.Year2015, Day.Day20, Part.Part2, Author.Anna);

        public override Task<string> Solve(string input)
        {
            var presents = int.Parse(input.Trim());

            for (var i = 1; i <= presents; i++)
            {
                var factorSum = SumOfFactorsUsedLess(i);
                if (11 * factorSum >= presents)
                { return Task.FromResult(i.ToString()); }
            }

            return Task.FromResult(0.ToString());
        }

        public Dictionary<long, long> Uses = [];
        private long SumOfFactorsUsedLess(int number)
        {
            var factors = FindAllFactors(number);
            long sum = 0;
            foreach (var factor in factors)
            {
                if (Uses.TryGetValue(factor, out long uses))
                {
                    if (uses < 50)
                    {
                        sum += factor;
                    }
                    Uses[factor] = ++uses;

                }
                else
                {
                    Uses.Add(factor, 1);
                    sum += factor;
                }
            }

            return sum;
        }

        private static HashSet<long> FindAllFactors(int number)
        {
            var factors = new HashSet<long> { 1 };
            var primeFactors = FindPrimeFactors(number);
            foreach (var (primeFactor, exp) in primeFactors)
            {
                var factorsFromPrime = new HashSet<long>();
                for (var x = 1; x <= exp; x++)
                {
                    var primeExp = (long)Math.Pow(primeFactor, x);
                    factorsFromPrime.Add(primeExp);
                    foreach (var factor in factors)
                    {
                        factorsFromPrime.Add(factor * primeExp);
                    }
                }
                factors.UnionWith(factorsFromPrime);
            }

            return factors;
        }

        private static Dictionary<long, long> FindPrimeFactors(int number)
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
