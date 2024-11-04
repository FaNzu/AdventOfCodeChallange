using AdventOfCode.Solutions.Library.Metadata;
using AdventOfCode.Solutions.Library;
using System.Text.Json.Nodes;

namespace AdventOfCode.Solutions.Puzzles.Year2015.Day12.Part2.Anna
{
    public class Solution : BaseSolution
    {
        public override SolutionMetadata Metadata => new(Year.Year2015, Day.Day12, Part.Part2, Author.Anna);

        public override Task<string> Solve(string input)
        {
            var json = JsonNode.Parse(input);
            var result = SumJson(json);
            return Task.FromResult(result.ToString());
        }

        private int SumJson(JsonNode json)
        {
            var result = 0;

            if (json is JsonArray jsonArray)
            {
                foreach (var item in jsonArray)
                {
                    result += SumJson(item);
                }
            }
            else if (json is JsonObject jsonObject)
            {
                var objectResult = 0;
                var includeObject = true;
                foreach(var item in jsonObject)
                {
                    if(item.Value is JsonValue itemValue)
                    {
                        itemValue.TryGetValue<string>(out var value);
                        if (value is "red")
                        {
                            includeObject = false;
                            break;
                        }
                    }
                    objectResult += SumJson(item.Value);
                }
                if(includeObject)
                {
                    result += objectResult;
                }
            }
            else if (json is JsonValue jsonValue)
            {
                jsonValue.TryGetValue<int>(out var value);
                result = value;
            }

            return result;
        }
    }
}
