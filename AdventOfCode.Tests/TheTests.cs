using AdventOfCode.Solutions.Library;
using AdventOfCode.Tests.Library;

namespace AdventOfCode.Tests;

public sealed class TheTests
{
    /// <summary>
    /// This method tests all available solutions with all available test data.
    /// </summary>
    /// <remarks>
    /// Solutions and test data are discovered using reflection. No need to add new tests methods, only to add test
    /// cases in the 'Puzzles' directory. Assertion is done automatically by the test framework based on the output
    /// from the solution. If using Visual Studio, if is recommended to group tests by trait in the Test Explorer.
    /// </remarks>
    /// <param name="implementation">The solution to be tested.</param>
    /// <param name="input">Input for the solution.</param>
    /// <returns>The output from the solution.</returns>
    [TestCaseSource(typeof(TestsHelper), nameof(TestsHelper.GetAllTestCasesWithData))]
    public async Task<string> Test(BaseSolution implementation, string input)
    {
        return await implementation.Solve(input);
    }
}
