using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Reports;
using BenchmarkDotNet.Running;

namespace AdventOfCode.Benchmarks.Library.Reporting;

public sealed class BaseBenchmarkColumn : IColumn
{
    public string Id { get; }
    public string ColumnName { get; }
    public int PriorityInCategory { get; }
    public string Legend => $"Custom '{ColumnName}' tag column";
    public ColumnCategory Category { get; }

    public bool AlwaysShow => true;
    public bool IsNumeric => false;

    public UnitType UnitType => UnitType.Dimensionless;

    private readonly Func<BaseBenchmarks, string> _selector;

    public BaseBenchmarkColumn(string columnName, ColumnCategory category, Func<BaseBenchmarks, string> selector, int priority)
    {
        Id = nameof(TagColumn) + "." + columnName;
        ColumnName = columnName;
        Category = category;

        PriorityInCategory = priority;

        _selector = selector;
    }

    public bool IsDefault(Summary summary, BenchmarkCase benchmarkCase) => false;
    public string GetValue(Summary summary, BenchmarkCase benchmarkCase)
    {
        var implementingType = benchmarkCase.Descriptor?.WorkloadMethod?.ReflectedType;

        if (implementingType == null)
        {
            throw new InvalidOperationException(
                $"Could not get the benchmark class type. Expecting implementation of '{nameof(BaseBenchmarks)}'.");
        }

        // Get the implementing type, but return it as the base type.
        var instance = Activator.CreateInstance(implementingType) as BaseBenchmarks;

        if (instance == null)
        {
            throw new InvalidOperationException(
                "Could not create instance of benchmark class.");
        }

        return _selector(instance);
    }

    public string GetValue(Summary summary, BenchmarkCase benchmarkCase, SummaryStyle style) => GetValue(summary, benchmarkCase);

    public bool IsAvailable(Summary summary) => true;

    public override string ToString() => ColumnName;
}
