using AdventOfCode.Solutions.Library;
using AdventOfCode.Solutions.Library.Metadata;
using System.Security.Cryptography;

namespace AdventOfCode.Solutions.Puzzles.Year2015.Day04.Part2.Anna
{
    public class Solution : BaseSolution
    {
        public override SolutionMetadata Metadata => new(Year.Year2015, Day.Day04, Part.Part2, Author.Anna);

        public override Task<string> Solve(string input)
        {
            using MD5 md5 = MD5.Create();
            var secretKey = input.Trim();
            var lowestNumber = 1;
            while (true)
            {
                var hashInput = secretKey + lowestNumber;
                var encoding = System.Text.Encoding.UTF8.GetBytes(hashInput);
                var hash = md5.ComputeHash(encoding);

                var firstEntry = hash[0].ToString("X2");
                var secondEntry = hash[1].ToString("X2");
                var thirdEntry = hash[2].ToString("X2");
                var firstSixCharacters = firstEntry + secondEntry + thirdEntry;

                var hashCorrect = firstSixCharacters.StartsWith("000000");
                if (hashCorrect)
                {
                    break;
                }

                lowestNumber++;
            }


            return Task.FromResult(lowestNumber.ToString());
        }
    }
}
