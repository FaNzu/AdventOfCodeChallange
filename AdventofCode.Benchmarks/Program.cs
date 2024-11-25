using AdventOfCode.Benchmarks.Library;
using BenchmarkDotNet.Running;

namespace AdventOfCode.Benchmarks;

public static class Program
{
    public static void Main(string[] args)
    {
        var benchmarks = typeof(Program).Assembly
            .GetExportedTypes()
            .Where(type => !type.IsAbstract && type.IsSubclassOf(typeof(BaseBenchmarks)))
            .ToArray();

        BenchmarkSwitcher.FromTypes(benchmarks).Run(args);
    }
}
