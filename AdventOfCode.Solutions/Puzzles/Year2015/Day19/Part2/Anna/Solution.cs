using AdventOfCode.Solutions.Library.Metadata;
using AdventOfCode.Solutions.Library;
using System.Text.RegularExpressions;
using System.Reflection;

namespace AdventOfCode.Solutions.Puzzles.Year2015.Day19.Part2.Anna
{
    public class Solution : BaseSolution
    {
        public override SolutionMetadata Metadata => new(Year.Year2015, Day.Day19, Part.Part2, Author.Anna);

        public HashSet<string> alphabet = new HashSet<string>();
        public HashSet<string> variables = new HashSet<string>();
        public HashSet<string> constants = new HashSet<string>();

        public Dictionary<(int, int), HashSet<(string, ((string, int), (string, int)))>> rules = new Dictionary<(int, int), HashSet<(string, ((string, int), (string, int)))>>();

        public override Task<string> Solve(string input)
        {
            var productions = new Dictionary<string, HashSet<List<string>>>();
            var molecule = "";

            var lines = input.Split('\n');
            foreach (var l in lines)
            {
                var line = l.Trim();
                if (line.Contains("=>"))
                {
                    var split = line.Split(" => ");
                    var leftSide = split[0];
                    var rightSide = split[1];

                    variables.Add(leftSide);
                    var splitRight = SplitString(rightSide);

                    if (productions.ContainsKey(leftSide))
                    { productions[leftSide].Add(splitRight); }
                    else
                    { productions.Add(leftSide, [splitRight]); }
                }
                else if (line.Length > 0)
                {
                    molecule = line;
                }
            }
            constants = alphabet.Except(variables).ToHashSet();

            var constantRules = new Dictionary<string, string>();
            var variableRules = new Dictionary<string, HashSet<(string, string)>>();
            var uniqueNumber = 0;
            foreach (var constant in constants)
            {
                var newVariable = "X" + uniqueNumber;
                uniqueNumber++;
                variables.Add(newVariable);

                constantRules.Add(newVariable, constant);
            }
            var reverseConstantRules = constantRules.ToDictionary((i) => i.Value, (i) => i.Key);

            foreach (var (leftSide, replacements) in productions)
            {
                foreach (var replacement in replacements)
                {
                    var newReplacement = replacement.Select(element =>
                    {
                        if (constants.Contains(element))
                        {
                            return reverseConstantRules[element];
                        }
                        else
                        { return element; }
                    }).ToList();

                    while (newReplacement.Count > 2)
                    {
                        var newVariable = "X" + uniqueNumber;
                        uniqueNumber++;
                        variables.Add(newVariable);

                        variableRules.Add(newVariable, [(newReplacement[0], newReplacement[1])]);
                        newReplacement.RemoveRange(0, 2);
                        newReplacement.Insert(0, newVariable);
                    }
                    if (variableRules.ContainsKey(leftSide))
                    { variableRules[leftSide].Add((newReplacement[0], newReplacement[1])); }
                    else
                    { variableRules.Add(leftSide, [(newReplacement[0], newReplacement[1])]); }
                }
            }
            var reverseVariableRules = new Dictionary<(string, string), HashSet<string>>();
            foreach (var (variable, production) in variableRules)
            {
                foreach (var p in production)
                {
                    if (reverseVariableRules.ContainsKey(p))
                    { reverseVariableRules[p].Add(variable); }
                    else
                    { reverseVariableRules.Add(p, [variable]); }
                }
            }

            var splitMolecule = SplitString(molecule);
            var results = new Dictionary<(int, int), HashSet<string>>();
            for (var i = 1; i <= splitMolecule.Count; i++)
            {
                for (var j = 0; j <= splitMolecule.Count - i; j++)
                {
                    if (i == 1)
                    {
                        var constant = splitMolecule[j];
                        if (reverseConstantRules.ContainsKey(constant))
                        {
                            results.Add((i, j), [reverseConstantRules[constant]]);
                        }
                        else
                        {
                            results.Add((i, j), [constant]);
                        }
                    }
                    else
                    {
                        var newVars = new HashSet<string>();
                        var newRules = new HashSet<(string, ((string, int), (string, int)))>();
                        for (var k = 1; k < i; k++)
                        {
                            var firstVar = results[(k, j)];
                            var secondVar = results[(i - k, j + k)];
                            foreach (var v in firstVar)
                            {
                                foreach (var w in secondVar)
                                {
                                    if(reverseVariableRules.TryGetValue((v, w), out var possibleVars))
                                    {
                                        foreach (var newVar in possibleVars)
                                        {
                                            newVars.Add(newVar);
                                            newRules.Add((newVar, ((v,k),(w,i-k))));
                                        }
                                    }
                                }
                            }
                        }
                        results.Add((i, j), newVars);
                        rules.Add((i, j), newRules);
                    }
                }
            }

            foreach (var v in rules[(splitMolecule.Count, 0)])
            {
                Console.WriteLine($"{v.Item1} => {v.Item2.Item1.Item1}({v.Item2.Item1.Item2}){v.Item2.Item2.Item1}({v.Item2.Item2.Item2})");
            }
            var minimalRules = CountRulesUsed(splitMolecule.Count, 0, "e");

            return Task.FromResult(minimalRules.ToString());
        }

        public int CountRulesUsed(int take, int start, string variable)
        {
            foreach (var ruleTaken in rules[(take, start)])
            {
                var leftSide = ruleTaken.Item1;

                var rightSide1Var = ruleTaken.Item2.Item1.Item1;
                var rightSide1k = ruleTaken.Item2.Item1.Item2;
                var rightSide2Var = ruleTaken.Item2.Item2.Item1;
                var rightSide2k = ruleTaken.Item2.Item2.Item2;

                if(leftSide == variable)
                {
                    var rightSideRules = 0;
                    if(rightSide1k != 1)
                    {
                        rightSideRules += CountRulesUsed(rightSide1k, start, rightSide1Var);
                    }
                    if(rightSide2k != 1)
                    {
                        rightSideRules += CountRulesUsed(rightSide2k, start + rightSide1k, rightSide2Var);
                    }
                    if(!leftSide.Contains('X'))
                    {
                        rightSideRules++;
                    }
                    return rightSideRules;
                }
            }
            throw new NotImplementedException();
        }

        public List<string> SplitString(string elements)
        {
            var splitElements = new List<string>();
            for (var i = 0; i < elements.Length; i++)
            {
                if (char.IsLower(elements[i]))
                { continue; }

                var element = elements[i].ToString();
                if (i == elements.Length - 1)
                {
                    splitElements.Add(element);
                    alphabet.Add(element);
                }
                else if (char.IsLower(elements[i + 1]))
                {
                    element += elements[i + 1];
                    splitElements.Add(element);
                    alphabet.Add(element);
                }
                else
                {
                    splitElements.Add(element);
                    alphabet.Add(element);
                }
            }

            return splitElements;
        }
    }
}
