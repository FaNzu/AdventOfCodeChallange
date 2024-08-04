using System.Collections.Immutable;
using System.Reflection;
using AdventOfCode.Solutions.Library.Metadata;

namespace AdventOfCode.Solutions.Library;

public abstract class GenericHelper
{
    public static IEnumerable<(T, Year, Day, Part)> GetAllByMetadata<T>() where T : WithMetadata
    {
        foreach (var year in Enum.GetValues<Year>())
        {
            foreach (var day in Enum.GetValues<Day>())
            {
                foreach (var part in Enum.GetValues<Part>())
                {
                    var items = GetByMetadata<T>(year, day, part);

                    if (items.Count > 0)
                    {
                        foreach (var item in items)
                        {
                            yield return (item, year, day, part);
                        }
                    }
                }
            }
        }
    }

    public static ImmutableList<T> GetByMetadata<T>(Year year, Day day, Part part) where T : WithMetadata
    {
        var baseType = typeof(T);

        var assembly = Assembly.GetAssembly(baseType)
            ?? throw new InvalidOperationException("Could not find the required assembly.");

        var classes = assembly.GetExportedTypes()
            .Where(type => !type.IsAbstract && type.IsSubclassOf(baseType))
            .Select(type =>
            {
                try
                {
                    var instance = Activator.CreateInstance(type) as T
                        ?? throw new Exception($"Could not cast instance of type '{type.FullName}' to output type.");

                    return instance;
                }
                catch (Exception ex)
                {
                    throw new InvalidOperationException(
                        $"Could not create instance of type '{type.FullName}'. See inner exception for more details",
                        ex);
                }
            })
            .Where(solution =>
            {
                return solution.Metadata.Year == year
                    && solution.Metadata.Day == day
                    && solution.Metadata.Part == part;
            })
            .ToImmutableList();

        return classes;
    }
}
