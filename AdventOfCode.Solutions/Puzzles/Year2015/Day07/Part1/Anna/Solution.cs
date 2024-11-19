using AdventOfCode.Solutions.Library.Metadata;
using AdventOfCode.Solutions.Library;

using QuikGraph.Algorithms;
using QuikGraph;

namespace AdventOfCode.Solutions.Puzzles.Year2015.Day07.Part1.Anna
{
    public class Solution : BaseSolution
    {
        public override SolutionMetadata Metadata => new(Year.Year2015, Day.Day07, Part.Part1, Author.Anna);

        public override Task<string> Solve(string input)
        {
            var wires = new Dictionary<string, ushort>
            {
                { "1", 1 }
            };
            var operations = new Dictionary<string, (string, ushort)>
            {
                { "1", ("CONSTANT", 0)  }
            };
            var graph = new BidirectionalGraph<string, SEdge<string>>(false);
            foreach (var l in input.Split('\n'))
            {
                var line = l.Trim();

                var split1 = line.Split(" -> ");
                var target = split1[1];
                graph.AddVertex(target);
                var sources = split1[0].Split(" ");
                if (sources.Length == 1)
                {
                    var success = ushort.TryParse(sources[0], out ushort constant);
                    if (success)
                    {
                        wires.Add(target, constant);
                        operations.Add(target, ("CONSTANT", 0));
                    }
                    else
                    {
                        var source = sources[0];
                        graph.AddVertex(source);
                        graph.AddEdge(new SEdge<string>(source, target));
                        operations.Add(target, ("COPY", 0));
                    }
                }
                else if (sources.Length == 2)
                {
                    if (sources[0] == "NOT")
                    {
                        var source = sources[1];
                        graph.AddVertex(source);
                        graph.AddEdge(new SEdge<string>(source, target));
                        operations.Add(target, ("NOT", 0));
                    }
                }
                else if (sources.Length == 3)
                {
                    var op = sources[1];
                    if (op.Contains("SHIFT"))
                    {
                        var source = sources[0];
                        var bitsToShift = ushort.Parse(sources[2]);
                        graph.AddVertex(source);
                        graph.AddEdge(new SEdge<string>(source, target));
                        operations.Add(target, (op, bitsToShift));
                    }
                    else if (op == "AND")
                    {
                        var source1 = sources[0];
                        var source2 = sources[2];
                        graph.AddVertex(source1);
                        graph.AddVertex(source2);
                        graph.AddEdge(new SEdge<string>(source1, target));
                        graph.AddEdge(new SEdge<string>(source2, target));
                        operations.Add(target, (op, 0));
                        
                    }
                    else if (op == "OR")
                    {
                        var source1 = sources[0];
                        var source2 = sources[2];
                        graph.AddVertex(source1);
                        graph.AddVertex(source2);
                        graph.AddEdge(new SEdge<string>(source1, target));
                        graph.AddEdge(new SEdge<string>(source2, target));
                        operations.Add(target, (op, 0));
                    }
                }
            }

            var sort = graph.TopologicalSort();
            foreach (var vertex in sort)
            {
                (string operation, ushort bitsToShift) = operations[vertex];

                if (operation == "COPY")
                {
                    var predecessor = graph.InEdge(vertex, 0).Source;
                    var predecessorValue = wires[predecessor];

                    wires.Add(vertex, predecessorValue);
                }
                else if (operation == "NOT")
                {
                    var predecessor = graph.InEdge(vertex, 0).Source;
                    var predecessorValue = wires[predecessor];

                    var newValue = (ushort)~predecessorValue;
                    wires.Add(vertex, newValue);
                }
                else if (operation == "AND")
                {
                    var predecessor1 = graph.InEdge(vertex, 0).Source;
                    var predecessorValue1 = wires[predecessor1];
                    var predecessor2 = graph.InEdge(vertex, 1).Source;
                    var predecessorValue2 = wires[predecessor2];

                    var newValue = (ushort)(predecessorValue1 & predecessorValue2);
                    wires.Add(vertex, newValue);
                }
                else if (operation == "OR")
                {
                    var predecessor1 = graph.InEdge(vertex, 0).Source;
                    var predecessorValue1 = wires[predecessor1];
                    var predecessor2 = graph.InEdge(vertex, 1).Source;
                    var predecessorValue2 = wires[predecessor2];

                    var newValue = (ushort)(predecessorValue1 | predecessorValue2);
                    wires.Add(vertex, newValue);
                }
                else if (operation == "RSHIFT")
                {
                    var predecessor = graph.InEdge(vertex,0).Source;
                    var predecessorValue = wires[predecessor];

                    var newValue = (ushort)(predecessorValue >> bitsToShift);
                    wires.Add(vertex, newValue);
                }
                else if (operation == "LSHIFT")
                {
                    var predecessor = graph.InEdge(vertex, 0).Source;
                    var predecessorValue = wires[predecessor];

                    var newValue = (ushort)(predecessorValue << bitsToShift);
                    wires.Add(vertex, newValue);
                }
            }

            var result = wires["a"];
            return Task.FromResult(result.ToString());
        }
    }
}
