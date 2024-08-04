using AdventOfCode.Solutions.Library;
using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Reports;
using BenchmarkDotNet.Running;

namespace AdventOfCode.Benchmarks.Library.Reporting;

public sealed class ParameterColumn : IColumn
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

    public ParameterColumn(string columnName, ColumnCategory category, Func<BaseBenchmarks, string> selector, int priority)
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
        foreach (var parameter in benchmarkCase.Parameters.Items)
        {
            if (parameter.Value is not BaseSolution solution)
            {
                throw new InvalidOperationException("Unexpected type of parameter.");
            }

            return solution.Metadata.Author.ToString();
        }

        throw new InvalidOperationException("Unexpected number of parameters.");
    }

    public string GetValue(Summary summary, BenchmarkCase benchmarkCase, SummaryStyle style) => GetValue(summary, benchmarkCase);

    public bool IsAvailable(Summary summary) => true;

    public override string ToString() => ColumnName;
}
