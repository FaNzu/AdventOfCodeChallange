using AdventOfCode.Solutions.Library;
using AdventOfCode.Solutions.Library.Metadata;

namespace AdventOfCode.Tests.Library;

public abstract class BaseTestData : WithMetadata
{
    public abstract override SimpleMetadata Metadata { get; }

    public abstract List<(string, string)> TestCases { get; }
}
