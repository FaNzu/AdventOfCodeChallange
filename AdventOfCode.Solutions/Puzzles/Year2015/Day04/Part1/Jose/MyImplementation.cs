using System.Security.Cryptography;
using System.Text;
using AdventOfCode.Solutions.Library;
using AdventOfCode.Solutions.Library.Metadata;

namespace AdventOfCode.Solutions.Puzzles.Year2015.Day04.Part1.Jose;

public sealed class MyImplementation : BaseSolution
{
    public override SolutionMetadata Metadata => new(Year.Year2015, Day.Day04, Part.Part1, Author.Jose);

    public override Task<string> Solve(string input)
    {
        using (var algo = MD5.Create())
        {
            var n = 1;

            while (true)
            {
                var inputBytes = Encoding.UTF8.GetBytes(input + n.ToString());
                var hashBytes = algo.ComputeHash(inputBytes);

                var hash = CreateHexadecimalString(hashBytes);

                if (hash.StartsWith("00000"))
                {
                    return Task.FromResult(n.ToString());
                }

                n++;
            }
        }
    }

    private static string CreateHexadecimalString(byte[] input)
    {
        var sb = new StringBuilder();

        for (int i = 0; i < input.Length; i++)
        {
            sb.Append(input[i].ToString("x2"));
        }

        return sb.ToString();
    }
}
