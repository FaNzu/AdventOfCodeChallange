using AdventOfCode.Solutions.Library.Metadata;
using AdventOfCode.Solutions.Library;
using System.Security.Cryptography;
using System.Text;

namespace AdventOfCode.Solutions.Puzzles.Year2015.Day04.Part2.Ask
{
    public sealed class OptimizedLogic : BaseSolution
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
                byte[] secretKeyBytes = Encoding.UTF8.GetBytes(secretKey);
                byte[] hashBuffer = new byte[secretKeyBytes.Length + 10];

                while (true)
                {
                    Buffer.BlockCopy(secretKeyBytes, 0, hashBuffer, 0, secretKeyBytes.Length);
                    int length = secretKeyBytes.Length + Encoding.UTF8.GetBytes(number.ToString(), 0, number.ToString().Length, hashBuffer, secretKeyBytes.Length);

                    byte[] hash = md5.ComputeHash(hashBuffer, 0, length);

                    if (IsValidHash(hash, startsWith.Length))
                    {
                        return number;
                    }
                    number++;
                }
            }
        }

        private bool IsValidHash(byte[] hash, int leadingZeroCount)
        {
            int fullBytes = leadingZeroCount / 2;
            for (int i = 0; i < fullBytes; i++)
            {
                if (hash[i] != 0) return false;
            }

            if (leadingZeroCount % 2 != 0)
            {
                return (hash[fullBytes] & 0xF0) == 0;
            }

            return true;
        }
    }
}
