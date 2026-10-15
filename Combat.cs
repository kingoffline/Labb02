using System;

namespace Labb02
{
    internal class Combat
    {
        public void EnemyCombat(Player player, Enemy enemy)
        {
            int damage = RollDamage(enemy.AttackDice, player.DefenseDice);
            player.HP = Math.Max(0, player.HP - damage);
            Report(enemy.Name, player.Name, damage,$"{enemy.Name}: {enemy.HP}  {player.Name}: {player.HP}", 0);

            if (player.HP > 0 && enemy.HP > 0)
            {
                damage = RollDamage(player.AttackDice, enemy.DefenseDice);
                enemy.HP = Math.Max(0, enemy.HP - damage);
                Report(player.Name, enemy.Name, damage,
                    $"{player.Name}: {player.HP}  {enemy.Name}: {enemy.HP}", 1);
            }
        }

        public void PlayerCombat(Player player, Enemy enemy)
        {
            int damage = RollDamage(player.AttackDice, enemy.DefenseDice);
            enemy.HP = Math.Max(0, enemy.HP - damage);
            Report(player.Name, enemy.Name, damage,
                $"{player.Name}: {player.HP}  {enemy.Name}: {enemy.HP}", 0);

            if (enemy.HP > 0 && player.HP > 0)
            {
                damage = RollDamage(enemy.AttackDice, player.DefenseDice);
                player.HP = Math.Max(0, player.HP - damage);
                Report(enemy.Name, player.Name, damage,
                    $"{enemy.Name}: {enemy.HP}  {player.Name}: {player.HP}", 1);
            }
        }

        private static int RollDamage(Dice attackDice, Dice defenseDice)
        {
            int attackRoll = attackDice.Throw();
            int defenseRoll = defenseDice.Throw();
            return Math.Max(0, attackRoll - defenseRoll);
        }

        private static void Report(string attacker, string defender,
                           int damage, string hpStatus, int reportLine)
        {
            string outcome;

            if (damage > 0)
            {
                outcome = $"{attacker} dealt {damage} damage to {defender}";
            }
            else
            {
                outcome = $"{defender} blocked {attacker}";
            }

            Console.SetCursorPosition(0, reportLine);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"{attacker} attacks {defender}: {outcome} ==> {hpStatus}");
        }
    }
}
