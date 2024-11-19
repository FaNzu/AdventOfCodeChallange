using AdventOfCode.Solutions.Library.Metadata;
using AdventOfCode.Solutions.Library;
using System.Security.Cryptography;
using System.Text;

namespace AdventOfCode.Solutions.Puzzles.Year2015.Day04.Part1.Ask
{
    public sealed class NormalCalculations : BaseSolution
    {
        public override SolutionMetadata Metadata => new(Year.Year2015, Day.Day04, Part.Part1, Author.Ask);

        public override Task<string> Solve(string input)
        {
            var answer = FindLowestNumber(input.Trim(), "00000").ToString();
            return Task.FromResult(answer);
        }

        private int FindLowestNumber(string secretKey, string startsWith)
        {
            int number = 0;
            using (var md5 = MD5.Create())
            {
                while (true)
                {
                    var stringToHash = Encoding.UTF8.GetBytes(secretKey + number);
                    var hashBytes = md5.ComputeHash(stringToHash);
                    var md5Hash = GetMd5Hash(hashBytes);

                    if (md5Hash.StartsWith(startsWith))
                    {
                        return number;
                    }
                    number++;
                }
            }
        }

        private string GetMd5Hash(byte[] data)
        {
            StringBuilder sb = new StringBuilder();
            foreach (byte b in data)
            {
                sb.Append(b.ToString("x2"));
            }
            return sb.ToString();
        }
    }
}
