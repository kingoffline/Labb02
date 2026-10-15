namespace Labb02
{
    class Rat : Enemy
    {
        private static readonly Random rn = new Random();
        public Rat(int x, int y) : base(x, y, "Rat", 10, ConsoleColor.Red, 'r')
        {
            AttackDice = new Dice(1, 6, 3);
            DefenseDice = new Dice(1, 6, 1);
        }
        public override void Update(Player? player, List<LevelElement> elements)
        {
            if (player is null || HP <= 0)
                return;

            int randomMove = rn.Next(1, 5);
            int targetX = X;
            int targetY = Y;

            switch (randomMove)
            {
                case 1: targetY--; break;
                case 2: targetY++; break;
                case 3: targetX++; break;
                case 4: targetX--; break;
            }

            if (targetX == player.X && targetY == player.Y)
            {
                Combat.EnemyCombat(player, this);
                return;
            }

            bool isBlocked = elements.Any(e => e.X == targetX && e.Y == targetY);

            if (!isBlocked)
            {
                Console.SetCursorPosition(X, Y);
                Console.Write(' ');

                X = targetX;
                Y = targetY;
            }
        }
    }
}
