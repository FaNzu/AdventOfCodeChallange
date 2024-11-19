using AdventOfCode.Solutions.Library;
using AdventOfCode.Solutions.Library.Metadata;

namespace AdventOfCode.Solutions.Puzzles.Year2015.Day22.Part2.Anna
{
    public class Solution : BaseSolution
    {
        public override SolutionMetadata Metadata => new(Year.Year2015, Day.Day22, Part.Part2, Author.Anna);

        public record Player(int Health, int Armor, int Mana);
        public record Enemy(int Health, int Damage);
        public record PlayState(Player Player, Enemy Enemy, Dictionary<string, int> Effects);

        public static Dictionary<string, int> ManaCosts = new()
        {
            { "Magic Missile", 53 },
            { "Drain", 73 },
            { "Shield", 113 },
            { "Poison", 173 },
            { "Recharge", 229 },
        };

        public bool continueSpells = true;
        public int minimumMana = int.MaxValue;

        public override Task<string> Solve(string input)
        {
            var split = input.Split('\n');
            var enemyHealth = int.Parse(split[0].Trim().Split(' ')[2]);
            var enemyDamage = int.Parse(split[1].Trim().Split(' ')[1]);

            const int PlayerHealth = 50;
            const int PlayerMana = 500;

            var initState = new PlayState(new Player(PlayerHealth, 0, PlayerMana),
                new Enemy(enemyHealth, enemyDamage), new());

            var previousStates = new Dictionary<string, PlayState>
            {
                { "", initState },
            };

            foreach (var (cost, spells) in GetSpells())
            {
                var newSpell = spells.Last();
                var previousState = previousStates[string.Join("", spells.SkipLast(1))];
                (var playerWon, continueSpells, var newState) = SimulateTurn(previousState, newSpell, cost);
                if (continueSpells)
                {
                    previousStates.Add(string.Join("", spells), newState);
                }
            }

            return Task.FromResult(minimumMana.ToString());
        }

        private IEnumerable<(int, List<string>)> GetSpells()
        {
            var possibleExpansions = new Stack<(int cost, List<string> spells)>();
            foreach (var (spell, cost) in ManaCosts)
            {
                possibleExpansions.Push((cost, [spell]));
            }

            while (possibleExpansions.Count > 0)
            {
                var smallestCost = possibleExpansions.Pop();
                yield return smallestCost;

                if (continueSpells)
                {
                    foreach (var (spell, cost) in ManaCosts)
                    {
                        var newCost = smallestCost.cost + cost;
                        if (newCost < minimumMana)
                        {
                            var newSpells = new List<string>(smallestCost.spells) { spell };
                            possibleExpansions.Push((newCost, newSpells));
                        }
                    }
                }
            }

        }

        public (bool, bool, PlayState?) SimulateTurn(PlayState state, string spell, int cost)
        {
            var player = state.Player;
            var playerHealth = player.Health;
            var playerArmor = player.Armor;
            var playerMana = player.Mana;

            var enemy = state.Enemy;
            var enemyHealth = enemy.Health;
            var enemyDamage = enemy.Damage;

            var effects = new Dictionary<string, int>(state.Effects);

            playerHealth--;
            if (playerHealth <= 0)
            {
                //Console.WriteLine("Player died");
                return (false, false, new PlayState(new Player(playerHealth, playerArmor, playerMana), new Enemy(enemyHealth, enemyDamage), new Dictionary<string, int>(effects)));
            }

            foreach (var (effect, turns) in effects)
            {
                if (effect == "Poison")
                {
                    enemyHealth -= 3;
                }
                else if (effect == "Recharge")
                {
                    playerMana += 101;
                }
            }
            foreach (var effect in effects.Keys)
            {
                effects[effect] -= 1;
                if (effects[effect] == 0)
                {
                    effects.Remove(effect);
                    if (effect == "Shield")
                    { playerArmor -= 7; }
                }
            }

            if (enemyHealth <= 0)
            {
                //Console.WriteLine("Enemy died");
                minimumMana = Math.Min(cost, minimumMana);
                return (true, true, new PlayState(new Player(playerHealth, playerArmor, playerMana), new Enemy(enemyHealth, enemyDamage), new Dictionary<string, int>(effects)));
            }

            playerMana -= ManaCosts[spell];
            if (playerMana < 0)
            {
                //Console.WriteLine("Player ran out of mana");
                return (false, false, new PlayState(new Player(playerHealth, playerArmor, playerMana), new Enemy(enemyHealth, enemyDamage), new Dictionary<string, int>(effects)));
            }
            if (effects.ContainsKey(spell))
            {
                //Console.WriteLine("Applied existing spell");
                return (false, false, new PlayState(new Player(playerHealth, playerArmor, playerMana), new Enemy(enemyHealth, enemyDamage), new Dictionary<string, int>(effects)));
            }

            if (spell == "Magic Missile")
            {
                enemyHealth -= 4;
            }
            else if (spell == "Drain")
            {
                enemyHealth -= 2; playerHealth += 2;
            }
            else if (spell == "Shield")
            {
                effects.Add(spell, 6); playerArmor += 7;
            }
            else if (spell == "Poison")
            {
                effects.Add(spell, 6);
            }
            else if (spell == "Recharge")
            {
                effects.Add(spell, 5);
            }

            if (enemyHealth <= 0)
            {
                //Console.WriteLine("Enemy died");
                minimumMana = Math.Min(cost, minimumMana);
                return (true, true, new PlayState(new Player(playerHealth, playerArmor, playerMana), new Enemy(enemyHealth, enemyDamage), new Dictionary<string, int>(effects)));
            }

            foreach (var (effect, turns) in effects)
            {
                if (effect == "Poison")
                {
                    enemyHealth -= 3;
                }
                else if (effect == "Recharge")
                {
                    playerMana += 101;
                }
            }
            foreach (var effect in effects.Keys)
            {
                effects[effect] -= 1;
                if (effects[effect] == 0)
                {
                    effects.Remove(effect);
                    if (effect == "Shield")
                    { playerArmor -= 7; }
                }
            }

            if (enemyHealth <= 0)
            {
                //Console.WriteLine("Enemy died");
                minimumMana = Math.Min(cost, minimumMana);
                return (true, true, new PlayState(new Player(playerHealth, playerArmor, playerMana), new Enemy(enemyHealth, enemyDamage), new Dictionary<string, int>(effects)));
            }

            var enemyHit = Math.Max(1, enemyDamage - playerArmor);
            playerHealth -= enemyHit;

            if (playerHealth <= 0)
            {
                //Console.WriteLine("Player died");
                return (false, false, new PlayState(new Player(playerHealth, playerArmor, playerMana), new Enemy(enemyHealth, enemyDamage), new Dictionary<string, int>(effects)));
            }

            return (false, true, new PlayState(new Player(playerHealth, playerArmor, playerMana), new Enemy(enemyHealth, enemyDamage), new Dictionary<string, int>(effects)));
        }

    }
}
