using AdventOfCode.Solutions.Library.Metadata;

namespace AdventOfCode.Solutions.Library;

public readonly record struct InputFile
{
    public Year Year { get; }

    public Day Day { get; }

    public Author Author { get; }

    public string Path { get; }

    public InputFile(Year year, Day day, Author author)
    {
        Year = year;
        Day = day;
        Author = author;
        Path = $"./Puzzles/{year}/{day}/Input/{author}.dat";
    }

    public bool Exists() => File.Exists(Path);

    public string GetContents() => File.ReadAllText(Path);
}
