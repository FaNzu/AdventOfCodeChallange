using CommandLine;

namespace AdventOfCode.Cli;

public sealed class Options
{
    [Option('y', "year", Required = false)]
    public int? Year { get; set; }

    [Option('d', "day", Required = false)]
    public int? Day { get; set; }

    [Option('p', "part", Required = false)]
    public int? Part { get; set; }

    [Option('a', "author", Required = false)]
    public string? Author { get; set; }
}
