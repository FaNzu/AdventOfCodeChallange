using AdventOfCode.Solutions.Library;

namespace AdventOfCode.Tests.Library;

public static class TestsHelper
{
    public static IEnumerable<TestCaseData> GetAllTestCasesWithData()
    {
        foreach (var (solution, year, day, part) in GenericHelper.GetAllByMetadata<BaseSolution>())
        {
            var testData = GenericHelper.GetByMetadata<BaseTestData>(year, day, part);

            if (testData.Count == 0)
            {
                continue;
            }

            foreach (var test in testData)
            {
                foreach (var (testInput, expectedResult) in test.TestCases)
                {
                    yield return new TestCaseData(solution, testInput)
                        .SetCategory($"{year} → {day} → {part} → {solution.Metadata.Author}")
                        .Returns(expectedResult);
                }
            }
        }
    }
}
