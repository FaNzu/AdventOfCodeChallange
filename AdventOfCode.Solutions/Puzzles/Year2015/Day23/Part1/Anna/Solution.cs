using AdventOfCode.Solutions.Library;
using AdventOfCode.Solutions.Library.Metadata;

namespace AdventOfCode.Solutions.Puzzles.Year2015.Day23.Part1.Anna
{
    public class Solution : BaseSolution
    {
        public override SolutionMetadata Metadata => new(Year.Year2015, Day.Day23, Part.Part1, Author.Anna);

        public override Task<string> Solve(string input)
        {
            var instructions = new List<(string, string, int)>();
            foreach (var l in input.Split('\n'))
            {
                var split = l.Trim().Split(' ');
                var instr = split[0];
                if (instr == "jmp")
                {
                    var reg = "";
                    var offset = int.Parse(split[1]);
                    instructions.Add((instr, reg, offset));
                }
                else
                {
                    var reg = split[1].TrimEnd(',');
                    var offset = 0;
                    if (split.Length > 2)
                    { offset = int.Parse(split[2]); }
                    instructions.Add((instr, reg, offset));
                }
            }

            var instructionNumber = 0;
            var regA = 0;
            var regB = 0;
            while(true)
            {
                if (instructionNumber >= instructions.Count || instructionNumber < 0)
                { break; }

                var (instr, reg, offset) = instructions[instructionNumber];
                if (instr == "hlf")
                {
                    if (reg == "a")
                    { regA /= 2; }
                    else if (reg == "b")
                    { regB /= 2; }
                    else { throw new NotImplementedException(); }
                    instructionNumber++;
                }
                else if (instr == "tpl")
                {
                    if (reg == "a")
                    { regA *= 3; }
                    else if (reg == "b")
                    { regB *= 3; }
                    else { throw new NotImplementedException(); }
                    instructionNumber++;
                }
                else if (instr == "inc")
                {
                    if (reg == "a")
                    { regA += 1; }
                    else if (reg == "b")
                    { regB += 1; }
                    else { throw new NotImplementedException(); }
                    instructionNumber++;
                }
                else if (instr == "jmp")
                {
                    instructionNumber += offset;
                }
                else if (instr == "jie")
                {
                    if (reg == "a" && regA % 2 == 0)
                    { instructionNumber += offset; }
                    else if (reg == "b" && regB % 2 == 0)
                    { instructionNumber += offset; }
                    else { instructionNumber++; }
                }
                else if (instr == "jio")
                {
                    if (reg == "a" && regA == 1)
                    { instructionNumber += offset; }
                    else if (reg == "b" && regB == 1)
                    { instructionNumber += offset; }
                    else { instructionNumber++; }
                }
                else { throw new NotImplementedException(); }
            }

            return Task.FromResult(regB.ToString());
        }
    }
}
