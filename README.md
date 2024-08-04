# AdventOfCode

This repository is meant to enable a group of people to participate (and compete) together in [Advent of Code](https://adventofcode.com/). This group of people is the _Cloud Feature Team_ in [Umbraco](https://umbraco.com/) 🙂

## 📄 Solution description

The repository contains a .NET C# solution with several projects. This is what they do:

- `AdventOfCode.Cli`: a console app that runs solutions for a certain year/day/part/author.
- `AdventOfCode.Solutions`: the solutions to the Advent Of Code puzzles.
- `AdventOfCode.Tests`: a test runner with solution and test data auto-discovery.
- `AdventOfCode.Benchmarks`: a benchmark runner with solution and input auto-discovery.

The main principle behind the repository is that many people can have many solutions for the puzzles in Advent of Code. One person can have three solutions for a certain puzzle. This is all handled with _metadata_ following the way Advent of Code works: there are `Year`, `Day`, and `Part` properties. There is also an `Author` property, which introduces the collaboration and/or competition factor.

In order to participate, the following things need to be done in code:

- Add your **solutions**. _These are the classes that solve each of the challenges in Advent of Code_.
- Add **test data**. _Test data come from the examples that the Advent of Code website provides for each challenge._
- Add **benchmarking classes**. _These are just a slim class with metadata, so that the benchmark runner can discover our solutions_.

The following sections explain how to get those things done. However, the solution has been written in a way that allows you to discover where to put things without thinking too much. **Follow existing conventions and you will be fine.**

## ✏ How to write my solutions

- Solutions belong into `AdventOfCode.Solutions`, `Puzzles` directory.
- Create a class for each of your solutions.
- Class needs to inherit from `BaseSolution`.
- Class needs to have the correct metadata.
- Give your class a meaningful name. It will be used in tests and benchmarks to identify your solution.

This is what is meant with "metadata":

```C#
public override SolutionMetadata Metadata => new(Year.Year2015, Day.Day01, Part.Part1, Author.Jose);
```

## 🚀 How to run my solutions

- Run `AdventOfCode.Cli`. It's a command line app.
- Provide `Year`, `Day`, `Part`, and `Author`.
    - ℹ That can be done via a menu in the app or via command line arguments.
- Store your input from Advent of Code to obtain your answer.
    - ℹ Input is stored in `AdventOfCode.Solutions` / `Puzzles/Year{YYYY}/Day{DD}/Input/{AUTHOR}.dat`

## ⚙ How to test my solutions

- Run tests with `dotnet test` or your IDE's test explorer.
- Test data can be added by anyone. It is the same for everyone, so it only needs to be added once.
    - ℹ Test data is stored in classes in `AdventOfCode.Tests` / `Puzzles/Year{YYYY}/`.
- ℹ If using your IDE, group tests by traits or categories in the test explorer. This will make it easier to see what is tested.

How does the test system work in detail?

> Tests use reflection to discover all available implementations for a certain `Year`, `Day`, and `Part`. This relies on the metadata in your solution. For each implementation, the relevant test data is fetched using the same mechanism: metadata is used to locate available test data.

## ⌛ How to measure performance (run the benchmarks)

- Run `AdventOfCode.Benchmarks`. It's a command line app.
- ⚠ Only release builds are accepted: `dotnet run -c Release`.
- Select the class corresponding to the `Year`, `Day`, and `Part` you want to benchmark.
- In order to be able to benchmark, a benchmark class needs to exist for that particular `Year`, `Day`, and `Part`.
- Benchmark classes can be added by anyone. It needs to be done only once per combination of `Year`, `Day`, and `Part`.
    - ℹ Benchmark classes are stored in `AdventOfCode.Benchmarks` / `Puzzles/Year{YYYY}/`.
    - ℹ Benchmark classes only need to have the right metadata. Everything else is done behind the scenes thanks to the `BaseBenchmarks` class.
