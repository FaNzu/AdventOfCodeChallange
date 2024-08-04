namespace AdventOfCode.Solutions.Library.Metadata;

public sealed record SolutionMetadata(Year Year, Day Day, Part Part, Author Author)
    : SimpleMetadata(Year, Day, Part)
{
}
