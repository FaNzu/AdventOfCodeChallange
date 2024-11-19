using AdventOfCode.Solutions.Library.Metadata;
using AdventOfCode.Solutions.Library;
using System.Text.RegularExpressions;

namespace AdventOfCode.Solutions.Puzzles.Year2015.Day19.Part2.Anna
{
    internal class SlightlySmarterSolution : BaseSolution
    {
        public override SolutionMetadata Metadata => new(Year.Year2015, Day.Day19, Part.Part2, Author.Anna);

        public Dictionary<string, HashSet<string>> replacements = new Dictionary<string, HashSet<string>>();

        public override Task<string> Solve(string input)
        {
            var calibrationMolecule = "";
            foreach (var l in input.Split('\n'))
            {
                var line = l.Trim();

                if (line.Length == 0) continue;
                if (!line.Contains("=>"))
                {
                    calibrationMolecule = line;
                    continue;
                }

                var split = line.Split(" => ");

                var leftSide = split[0];
                var rightSide = split[1];

                if (replacements.ContainsKey(rightSide))
                {
                    replacements[rightSide].Add(leftSide);
                }
                else
                {
                    replacements.Add(rightSide, [leftSide]);
                }
            }

            var iteration = 1;
            var currentIteration = new HashSet<string> { calibrationMolecule };
            while (true)
            {
                var newIteration = new HashSet<string>();
                foreach (var molecule in currentIteration)
                {
                    var newMolecules = GetAllMolecules(molecule);
                    foreach (var newMolecule in newMolecules)
                    {
                        if (newMolecule == "e")
                        {
                            return Task.FromResult(iteration.ToString());
                        }
                        newIteration.Add(newMolecule);
                    }
                }
                iteration++;
                currentIteration = newIteration;
            }
        }

        public HashSet<string> GetAllMolecules(string calibrationMolecule)
        {
            var molecules = new HashSet<string>();

            foreach (var (element, replacement) in replacements)
            {
                var matches = Regex.Matches(calibrationMolecule, element);
                foreach (var match in matches.Cast<Match>())
                {
                    foreach (var replacementElement in replacement)
                    {
                        var newMolecule =
                            calibrationMolecule.Substring(0, match.Index) +
                            replacementElement +
                            calibrationMolecule.Substring(match.Index + element.Length);
                        molecules.Add(newMolecule);
                    }
                }
            }

            return molecules;
        }
    }
}
