using AdventOfCode.Solutions.Library.Metadata;

namespace AdventOfCode.Cli;

public static class CliHelper
{
    public const string Title = @"
----------------------------------------------------------------------
  ___      _                 _            __   _____           _      
 / _ \    | |               | |          / _| /  __ \         | |     
/ /_\ \ __| |_   _____ _ __ | |_    ___ | |_  | /  \/ ___   __| | ___ 
|  _  |/ _` \ \ / / _ \ '_ \| __|  / _ \|  _| | |    / _ \ / _` |/ _ \
| | | | (_| |\ V /  __/ | | | |_  | (_) | |   | \__/\ (_) | (_| |  __/
\_| |_/\__,_| \_/ \___|_| |_|\__|  \___/|_|    \____/\___/ \__,_|\___|
                                                                      
                      Umbraco Cloud Feature Team
----------------------------------------------------------------------
";

    public static int GetValidIntFromConsole(string prompt, string errorMessage, Func<int, bool> isValid)
    {
        var answer = -1;

        while (true)
        {
            var rawAnswer = string.Empty;

            while (true)
            {
                Console.Write(prompt);
                rawAnswer = Console.ReadLine() ?? string.Empty;

                if (int.TryParse(rawAnswer, out answer))
                {
                    break;
                }

                Console.WriteLine(errorMessage);
                Console.WriteLine();
            }

            if (isValid(answer))
            {
                Console.WriteLine();
                break;
            }

            Console.WriteLine(errorMessage);
            Console.WriteLine();
        }

        return answer;
    }

    public static string GetValidStringFromConsole(string prompt, string errorMessage, Func<string, bool> isValid)
    {
        var answer = string.Empty;

        while (true)
        {
            Console.Write(prompt);
            answer = Console.ReadLine() ?? string.Empty;

            if (isValid(answer))
            {
                Console.WriteLine();
                break;
            }

            Console.WriteLine(errorMessage);
            Console.WriteLine();
        }

        return answer;
    }

    public static void EnsureOptionsHaveValidValues(Options options)
    {
        Func<int, bool> isValidYear = x => Enum.IsDefined((Year)x);
        Func<int, bool> isValidDay = x => Enum.IsDefined((Day)x);
        Func<int, bool> isValidPart = x => Enum.IsDefined((Part)x);
        Func<string, bool> isValidAuthor = x => !int.TryParse(x, out _) && Enum.TryParse<Author>(x, out _);

        while (options.Year == null || !isValidYear(options.Year.Value))
        {
            options.Year = GetValidIntFromConsole(" > Year: ", " > Invalid value for year.", isValidYear);
        }

        while (options.Day == null || !isValidDay(options.Day.Value))
        {
            options.Day = GetValidIntFromConsole(" > Day: ", " > Invalid value for day.", isValidDay);
        }

        while (options.Part == null || !isValidPart(options.Part.Value))
        {
            options.Part = GetValidIntFromConsole(" > Part: ", " > Invalid value for part.", isValidPart);
        }

        while (options.Author == null || !isValidAuthor(options.Author))
        {
            options.Author = GetValidStringFromConsole(" > Author: ", " > Invalid value for author.", isValidAuthor);
        }
    }

    public static (Year, Day, Part, Author) MapOptions(Options options)
    {
        return ((Year)options.Year!, (Day)options.Day!, (Part)options.Part!, Enum.Parse<Author>(options.Author!));
    }
}
