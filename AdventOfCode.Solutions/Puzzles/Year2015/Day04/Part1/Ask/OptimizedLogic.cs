using AdventOfCode.Solutions.Library.Metadata;
using AdventOfCode.Solutions.Library;
using System.Security.Cryptography;
using System.Text;

namespace AdventOfCode.Solutions.Puzzles.Year2015.Day04.Part1.Ask
{
    public sealed class OptimizedLogic : BaseSolution
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
                //preparing byte arrays for later computing
                byte[] secretKeyBytes = Encoding.UTF8.GetBytes(secretKey);
                byte[] hashBuffer = new byte[secretKeyBytes.Length + 10]; // Buffer to hold key + number bytes

                while (true)
                {
                    // Create the input buffer with the secret key and current number
                    Buffer.BlockCopy(secretKeyBytes, 0, hashBuffer, 0, secretKeyBytes.Length);
                    int length = secretKeyBytes.Length + Encoding.UTF8.GetBytes(number.ToString(), 0, number.ToString().Length, hashBuffer, secretKeyBytes.Length);

                    // Compute the hash
                    byte[] hash = md5.ComputeHash(hashBuffer, 0, length);

                    // Check if the hash starts with the required number of leading zeros
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
            // Calculate how many complete bytes are needed for the leading zero check
            int fullBytes = leadingZeroCount / 2;
            for (int i = 0; i < fullBytes; i++)
            {
                if (hash[i] != 0) return false; // If any byte is non-zero, return false
            }

            // Check the nibble (half-byte) of the next byte if the leading zero count is odd
            // We do this because number is 5, and a 0 could be hiding in the next byte
            if (leadingZeroCount % 2 != 0)
            {
                return (hash[fullBytes] & 0xF0) == 0; // Check if the upper nibble is zero
            }

            // If all byte placements contains Zero return true
            return true;
        }
    }
}
