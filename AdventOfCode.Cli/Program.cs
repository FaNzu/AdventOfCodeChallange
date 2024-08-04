using System.Collections.Immutable;
using System.Numerics;
using AdventOfCode.Solutions.Library;
using AdventOfCode.Solutions.Library.Metadata;
using CommandLine;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AdventOfCode.Cli;

public sealed class Program
{
    public static async Task Main(string[] args)
    {
        Console.WriteLine(CliHelper.Title);

        await Parser.Default.ParseArguments<Options>(args)
            .WithParsedAsync(async options =>
            {
                CliHelper.EnsureOptionsHaveValidValues(options);

                var (year, day, part, author) = CliHelper.MapOptions(options);

                var app = new Program(year, day, part, author);
                await app.RunAsync();
            });
    }

    private readonly Year _year;
    private readonly Day _day;
    private readonly Part _part;
    private readonly Author _author;

    public Program(Year year, Day day, Part part, Author author)
    {
        _year = year;
        _day = day;
        _part = part;
        _author = author;
    }

    public async Task RunAsync()
    {
        Console.WriteLine($" > Year='{_year}', Day='{_day}', Part='{_part}', Author='{_author}'.");

        var solutions = GetSolutions();
        var input = GetInput();

        Console.WriteLine();

        await SelectAndRunSolutions(solutions, input);
    }

    private async Task SelectAndRunSolutions(IImmutableList<BaseSolution> solutions, string input)
    {
        var selectedSolutions = SelectSolution(solutions);

        foreach (var solution in selectedSolutions)
        {
            await RunSolution(solution, input);
        }
    }

    private IImmutableList<BaseSolution> SelectSolution(IImmutableList<BaseSolution> solutions)
    {
        if (solutions.Count == 1)
        {
            return solutions;
        }

        return SelectSolutionUsingMenu(solutions);
    }

    private IImmutableList<BaseSolution> SelectSolutionUsingMenu(IImmutableList<BaseSolution> solutions)
    {
        Console.WriteLine(" > SELECT A SOLUTION TO RUN:");

        for (var i = 0; i < solutions.Count; i++)
        {
            // Note that items are displayed with option values starting at 1.
            Console.WriteLine($" · [{i + 1}] {solutions[i].GetType().Name}");
        }

        Console.WriteLine($" · [0] Run 'em all!");
        Console.WriteLine();

        var index = CliHelper.GetValidIntFromConsole(
            " > YOUR SELECTION: ",
            $" > Invalid input. Must be a number from 0 to {solutions.Count}.",
            x => x >= 0 && x <= solutions.Count);

        // 0 is a valid option, indicating "run all".
        if (index == 0)
        {
            return solutions;
        }

        return ImmutableList.Create(solutions.ElementAt(index - 1));
    }

    private async Task RunSolution(BaseSolution solution, string input)
    {
        Console.WriteLine($" > RUNNING SOLUTION '{solution.GetType().Name}'.");

        var output = await solution.Solve(input);

        Console.WriteLine(" > Output:");
        Console.WriteLine(output);
        Console.WriteLine();
    }

    private string GetInput()
    {
        var inputFile = new InputFile(_year, _day, _author);

        if (!inputFile.Exists())
        {
            throw new InvalidOperationException($"Could not find input file in '{inputFile.Path}'.");
        }

        Console.WriteLine($" > Using input file '{inputFile.Path}'.");

        return inputFile.GetContents();
    }

    private IImmutableList<BaseSolution> GetSolutions()
    {
        var solutions = GenericHelper.GetByMetadata<BaseSolution>(_year, _day, _part)
            .Where(x => x.Metadata.Author == _author)
            .ToImmutableList();

        Console.WriteLine($" > Found {solutions.Count} solutions for this date & author.");

        return solutions;
    }
}
