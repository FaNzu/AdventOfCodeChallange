using AdventOfCode.Solutions.Library;
using AdventOfCode.Solutions.Library.Metadata;

namespace AdventOfCode.Solutions.Puzzles.Year2015.Day21.Part1.Anna
{
    public class Solution : BaseSolution
    {
        public override SolutionMetadata Metadata => new(Year.Year2015, Day.Day21, Part.Part1, Author.Anna);

        public Dictionary<string, int> Weapons = new()
        {
            { "Dagger", 4 },
            { "Shortsword", 5 },
            { "Warhammer", 6 },
            { "Longsword", 7 },
            { "Greataxe", 8 },

        };
        public Dictionary<string, int> Armor = new()
        {
            { "Leather", 1 },
            { "Chainmail", 2 },
            { "Splintmail", 3 },
            { "Bandedmail", 4 },
            { "Platemail", 5 },
        };
        public Dictionary<string, (int, int)> Rings = new()
        {
            { "Damage+1", (1,0) },
            { "Damage+2", (2,0) },
            { "Damage+3", (3,0) },
            { "Defense+1", (0,1) },
            { "Defense+2", (0,2) },
            { "Defense+3", (0,3) },
        };
        public Dictionary<string, int> Costs = new()
        {
            { "Dagger", 8 },
            { "Shortsword", 10 },
            { "Warhammer", 25 },
            { "Longsword", 40 },
            { "Greataxe", 74 },
            { "Leather", 13 },
            { "Chainmail", 31 },
            { "Splintmail", 53 },
            { "Bandedmail", 75 },
            { "Platemail", 102 },
            { "Damage+1", 25 },
            { "Damage+2", 50 },
            { "Damage+3", 100 },
            { "Defense+1", 20 },
            { "Defense+2", 40 },
            { "Defense+3", 80 },
        };

        public override Task<string> Solve(string input)
        {
            var split = input.Split('\n');
            var enemyHealth = int.Parse(split[0].Trim().Split(' ')[2]);
            var enemyDamage = int.Parse(split[1].Trim().Split(' ')[1]);
            var enemyArmor = int.Parse(split[2].Trim().Split(' ')[1]);

            var possibleOutfits = EnumerateOutfits().OrderBy(o => o.Item1);
            foreach (var (cost, playerDamage, playerArmor) in possibleOutfits)
            {
                var playerHealth = 100;
                var playerWon = SimulateBattle(playerHealth, playerDamage, playerArmor, enemyHealth, enemyDamage, enemyArmor);
                if (playerWon)
                { return Task.FromResult(cost.ToString()); }
            }

            return Task.FromResult("Cannot win");
        }

        private List<(int, int, int)> EnumerateOutfits()
        {
            var result = new List<(int, int, int)> ();
            foreach (var (weapon, _) in Weapons)
            {
                foreach (var armorList in ChooseUpToI(Armor.Keys.ToHashSet(), 1))
                {
                    foreach (var ringList in ChooseUpToI(Rings.Keys.ToHashSet(), 2))
                    {
                        var cost = Costs[weapon];
                        var damage = Weapons[weapon];
                        var defense = 0;
                        foreach (var armor in armorList)
                        { cost += Costs[armor]; defense += Armor[armor]; }
                        foreach (var ring in ringList)
                        {
                            cost += Costs[ring];
                            var (ringDamage, ringDefense) = Rings[ring];
                            damage += ringDamage;
                            defense += ringDefense;
                        }
                        result.Add((cost, damage, defense));
                    }
                }
            }

            return result;
        }

        public List<List<string>> ChooseUpToI(HashSet<string> choices, int i)
        {
            if (i == 0)
            { return [[]]; }

            var smallerLists = ChooseUpToI(choices, i-1);
            var result = new List<List<string>>();
            foreach (var list in smallerLists)
            {
                result.Add(list);
                foreach (var choice in choices)
                {
                    if(!list.Contains(choice))
                    {
                        var newList = list.Append(choice).ToList();
                        result.Add(newList);
                    }
                }
            }
            return result;
        }

        private bool SimulateBattle(
            int playerHealth,
            int playerDamage,
            int playerArmor,
            int enemyHealth,
            int enemyDamage,
            int enemyArmor)
        {
            while (playerHealth > 0 && enemyHealth > 0)
            {
                var playerHit = Math.Max(1, playerDamage - enemyArmor);
                enemyHealth -= playerHit;

                if (enemyHealth <= 0)
                { break; }

                var enemyHit = Math.Max(1, enemyDamage - playerArmor);
                playerHealth -= enemyHit;
            }
            return playerHealth > 0;
        }
    }
}
