using AdventOfCode.Solutions.Library.Metadata;

namespace AdventOfCode.Solutions.Library;

public abstract class BaseSolution : WithMetadata
{
    public abstract override SolutionMetadata Metadata { get; }

    public BaseSolution()
    {
    }

    public abstract Task<string> Solve(string input);

    public override string ToString() => $"{Metadata.Year}-{Metadata.Day}-{Metadata.Part} | {Metadata.Author} | {GetType().Name}";
}
