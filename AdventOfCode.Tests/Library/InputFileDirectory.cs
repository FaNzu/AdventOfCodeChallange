using AdventOfCode.Solutions.Library.Metadata;

namespace AdventOfCode.Tests.Library;

public readonly record struct InputFileDirectory
{
    public string Value { get; }

    public InputFileDirectory(Year year, Day day, Author author)
    {
        Value = $"./Puzzles/{year}/{day}/Input/{author}.dat";
    }
}
