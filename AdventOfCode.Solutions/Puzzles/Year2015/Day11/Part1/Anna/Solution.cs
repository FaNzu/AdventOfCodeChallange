using AdventOfCode.Solutions.Library.Metadata;
using AdventOfCode.Solutions.Library;

namespace AdventOfCode.Solutions.Puzzles.Year2015.Day11.Part1.Anna
{
    public class Solution : BaseSolution
    {
        public override SolutionMetadata Metadata => new(Year.Year2015, Day.Day11, Part.Part1, Author.Anna);

        public override Task<string> Solve(string input)
        {
            var password = input.Trim();

            while(!ValidatePassword(password))
            {
                password = IncrementPassword(password);
            }

            return Task.FromResult(password.ToString());
        }

        private bool ValidatePassword(string password)
        {
            if (password.Contains('i') || password.Contains('l') || password.Contains('o'))
            {
                return false;
            }

            var hasStraight = false;
            for (int i = 0; i < password.Length - 2; i++)
            {
                var char1 = password[i];
                var char2 = password[i+1];
                var char3 = password[i+2];

                if((char)(Convert.ToUInt16(char1) + 1) == char2 && (char)(Convert.ToUInt16(char2) + 1) == char3)
                    hasStraight = true;
            }
            if (!hasStraight)
                return false;

            var pairs = new HashSet<string>();
            for (int i = 0; i < password.Length - 1; i++)
            {
                if (password[i] == password[i+1])
                {
                    var pair = password[i].ToString() + password[i + 1].ToString();
                    pairs.Add(pair);
                }
            }
            var hasTwoPairs = pairs.Count > 1;
            if (!hasTwoPairs)
                return false;

            return true;
        }

        private string IncrementPassword(string password)
        {
            var lastChar = password[password.Length - 1];
            password = password.Remove(password.Length - 1);

            if(lastChar == 'z')
            {
                password = IncrementPassword(password);
                password += 'a';
            }
            else
            {
                lastChar++;
                if(lastChar == 'i')
                {
                    lastChar = 'j';
                }
                else if(lastChar == 'l')
                {
                    lastChar = 'm';
                }
                else if (lastChar == 'o')
                {
                    lastChar = 'p';
                }
                password += lastChar;
            }

            return password;
        }
    }
}
