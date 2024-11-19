using AdventOfCode.Solutions.Library.Metadata;
using AdventOfCode.Solutions.Library;

namespace AdventOfCode.Solutions.Puzzles.Year2015.Day15.Part1.Anna
{
    public class Solution : BaseSolution
    {
        public override SolutionMetadata Metadata => new(Year.Year2015, Day.Day15, Part.Part1, Author.Anna);

        public override Task<string> Solve(string input)
        {
            var ingredients = new List<string>();
            var capacities = new Dictionary<string, int>();
            var durabilities = new Dictionary<string, int>();
            var flavors = new Dictionary<string, int>();
            var textures = new Dictionary<string, int>();

            foreach (var l in input.Split('\n'))
            {
                var split = l.Trim().Split(' ');

                var ingredient = split[0].Trim(':');
                var capacity = int.Parse(split[2].Trim(','));
                var durability = int.Parse(split[4].Trim(','));
                var flavor = int.Parse(split[6].Trim(','));
                var texture = int.Parse(split[8].Trim(','));

                ingredients.Add(ingredient);
                capacities.Add(ingredient, capacity);
                durabilities.Add(ingredient, durability);
                flavors.Add(ingredient, flavor);
                textures.Add(ingredient, texture);
            }

            var distributions = GetAllDistributions(ingredients, 100);
            var largestScore = 0;
            foreach (var distribution in distributions)
            {
                var totalCapacity = 0;
                var totalDurability = 0;
                var totalFlavor = 0;
                var totalTexture = 0;

                foreach (var (ingredient, teaspoons) in distribution)
                {
                    totalCapacity += teaspoons * capacities[ingredient];
                    totalDurability += teaspoons * durabilities[ingredient];
                    totalFlavor += teaspoons * flavors[ingredient];
                    totalTexture += teaspoons * textures[ingredient];
                }
                if(totalCapacity <= 0) totalCapacity = 0;
                if(totalDurability <= 0) totalDurability = 0;
                if(totalFlavor <= 0) totalFlavor = 0;
                if(totalTexture <= 0) totalTexture = 0;

                var score = totalCapacity * totalDurability * totalFlavor * totalTexture;
                if (score > largestScore)
                    largestScore = score;
            }

            return Task.FromResult(largestScore.ToString());
        }

        private List<Dictionary<string, int>> GetAllDistributions(List<string> ingredients, int total)
        {
            if (ingredients.Count == 1)
            {
                var distribution = new Dictionary<string, int>
                {
                    { ingredients[0], total }
                };
                return new List<Dictionary<string, int>> { distribution };
            }

            var firstIngredient = ingredients[0];
            var distributions = new List<Dictionary<string, int>>();
            for (int i = 0; i <= total; i++)
            {
                var smallerDistributions = GetAllDistributions(ingredients[1..], total - i);
                smallerDistributions.ForEach(d =>
                {
                    d.Add(firstIngredient, i);
                });
                distributions.AddRange(smallerDistributions);
            }

            return distributions;
        }
    }
}
