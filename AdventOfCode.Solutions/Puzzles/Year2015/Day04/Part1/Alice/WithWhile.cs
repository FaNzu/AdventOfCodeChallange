using System.Security.Cryptography;
using System.Text;
using AdventOfCode.Solutions.Library;
using AdventOfCode.Solutions.Library.Metadata;


namespace AdventOfCode.Solutions.Puzzles.Year2015.Day04.Part1.Alice;

public sealed class WithWhile : BaseSolution
{
    public override SolutionMetadata Metadata => new(Year.Year2015, Day.Day04, Part.Part1, Author.Alice);

    public override Task<string> Solve(string input)
    {
        int number = 0;

        using (var md5 = MD5.Create())
        {
            bool found = false;

            while (!found)
            {
                string combined = input + number;

                byte[] hashBytes = md5.ComputeHash(Encoding.UTF8.GetBytes(combined));

                string hash = BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
                //Console.WriteLine($"Hash for '{combined}': {hash}");

                if (hash.StartsWith("00000"))
                {
                    found = true;
                }
                else
                {
                    number++;
                }
            }

            return Task.FromResult(number.ToString());
        }
    }
}
