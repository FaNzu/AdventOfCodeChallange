using AdventOfCode.Benchmarks.Library.Reporting;
using AdventOfCode.Solutions.Library;
using AdventOfCode.Solutions.Library.Metadata;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Diagnosers;

namespace AdventOfCode.Benchmarks.Library;

[Config(typeof(BenchmarkConfiguration))]
public abstract class BaseBenchmarks
{
    private sealed class BenchmarkConfiguration : ManualConfig
    {
        public BenchmarkConfiguration()
        {
            AddDiagnoser(MemoryDiagnoser.Default);
            HideColumns("Method", "solution");
            AddColumn(new BaseBenchmarkColumn("Year", ColumnCategory.Job, be => be.Metadata.Year.ToString(), 0));
            AddColumn(new BaseBenchmarkColumn("Day", ColumnCategory.Job, be => be.Metadata.Day.ToString(), 1));
            AddColumn(new BaseBenchmarkColumn("Part", ColumnCategory.Job, be => be.Metadata.Part.ToString(), 2));
            AddColumn(new ParameterColumn("Author", ColumnCategory.Job, be => be.Metadata.Part.ToString(), 3));
            AddColumn(new ParameterColumn2("Solution", ColumnCategory.Job, be => be.Metadata.Part.ToString(), 4));
        }
    }

    public abstract SimpleMetadata Metadata { get; }

    public string Input { get; }

    public BaseBenchmarks()
    {
        Input = FirstSuitableInputOrThrow();
    }

    private string FirstSuitableInputOrThrow()
    {
        foreach (var author in Enum.GetValues<Author>())
        {
            var inputFile = new InputFile(Metadata.Year, Metadata.Day, author);

            if (inputFile.Exists())
            {
                return inputFile.GetContents();
            }
        }

        throw new InvalidOperationException(
            "Could not find input for benchmark. Please create a file with input for this benchmark and try again.");
    }

    public IEnumerable<BaseSolution> AvailableSolutions()
    {
        var solutions = GenericHelper.GetByMetadata<BaseSolution>(
            Metadata.Year,
            Metadata.Day,
            Metadata.Part);

        foreach (var solution in solutions)
        {
            yield return solution;
        }
    }

    [Benchmark]
    [ArgumentsSource(nameof(AvailableSolutions))]
    public async Task Run(BaseSolution solution)
    {
        await solution.Solve(Input);
    }
}
