using AdventOfCode.Solutions.Library.Metadata;
using AdventOfCode.Solutions.Library;
using System.IO;

namespace AdventOfCode.Solutions.Puzzles.Year2015.Day10.Part2.Anna
{
    public class Solution : BaseSolution
    {
        public override SolutionMetadata Metadata => new(Year.Year2015, Day.Day10, Part.Part2, Author.Anna);

        public override Task<string> Solve(string input)
        {
            var result = input.Trim();
            for (var i = 1; i <= 50; i++)
            {
                Console.WriteLine(i);
                result = ProcessString(result);
            }

            return Task.FromResult(result.Length.ToString());
        }

        private string ProcessString(string input)
        {
            if (input.Length > 50000)
            {
                var halfway = input.Length / 2;
                var firstHalf = input.Substring(0, halfway);
                var secondHalf = input.Substring(halfway);

                while (firstHalf[firstHalf.Length - 1] == secondHalf[0])
                {
                    firstHalf += secondHalf[0];
                    secondHalf = secondHalf.Substring(1);
                }

                string firstResult = "";
                Thread t1 = new Thread(() => firstResult = ProcessString(firstHalf));
                t1.Start();
                string secondResult = "";
                Thread t2 = new Thread(() => secondResult = ProcessString(secondHalf));
                t2.Start();

                t1.Join();
                t2.Join();

                return firstResult + secondResult;
            }

            var result = "";

            var currentDigit = input[0];
            var occurrences = 1;
            for (var j = 1; j < input.Length; j++)
            {
                if (input[j] == currentDigit)
                {
                    occurrences++;
                }
                else
                {
                    result += (occurrences.ToString() + currentDigit);

                    currentDigit = input[j];
                    occurrences = 1;
                }
            }
            result += (occurrences.ToString() + currentDigit);

            return result;
        }
    }
}
