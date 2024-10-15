using AdventOfCode.Solutions.Library.Metadata;
using AdventOfCode.Solutions.Library;
using System.Security.Cryptography;
using System.Text;

namespace AdventOfCode.Solutions.Puzzles.Year2015.Day04.Part2.Ask
{
    public sealed class NormalCalculations : BaseSolution
    {
        public override SolutionMetadata Metadata => new(Year.Year2015, Day.Day04, Part.Part2, Author.Ask);

        public override Task<string> Solve(string input)
        {
            var answer = FindLowestNumber(input.Trim(), "000000").ToString();
            return Task.FromResult(answer);
        }

        private int FindLowestNumber(string secretKey, string startsWith)
        {
            int number = 0;
            using (var md5 = MD5.Create())
            {
                while (true)
                {
                    var stringToHash = secretKey + number;
                    var hashed = GetMd5Hash(md5, stringToHash);
                    if (hashed.StartsWith(startsWith))
                    {
                        return number;
                    }
                    number++;
                }
            }
        }

        private string GetMd5Hash(MD5 md5, string input)
        {
            byte[] data = md5.ComputeHash(Encoding.UTF8.GetBytes(input));
            StringBuilder sb = new StringBuilder();
            foreach (byte b in data)
            {
                sb.Append(b.ToString("x2"));
            }
            return sb.ToString();
        }
    }
}
