using AdventOfCode.Benchmarks.Library;
using BenchmarkDotNet.Running;

namespace AdventOfCode.Benchmarks;

public static class Program
{
    public static void Main(string[] args)
    {
        // Does not work, all available benchmarks should appear when running the program and they don't.
        // But if you type *, they run.
        var benchmarks = typeof(Program).Assembly
            .GetExportedTypes()
            .Where(type => !type.IsAbstract && type.IsSubclassOf(typeof(BaseBenchmarks)))
            .ToArray();

        BenchmarkSwitcher.FromTypes(benchmarks).Run(args);
    }
}
