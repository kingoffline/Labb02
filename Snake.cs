namespace Labb02
{
    class Snake : Enemy
    {
        public Snake(int x, int y) : base(x, y, "Snake", 25, ConsoleColor.Green, 's')
        {
            AttackDice = new Dice(3, 4, 2);
            DefenseDice = new Dice(1, 8, 5);
        }
        public override void Update(Player? player, List<LevelElement> elements)
        {
            if (player is null || HP <= 0)
                return;

            int distanceOnXAxel = X - player.X;
            int distanceOnYAxel = Y - player.Y;
            int radialDistance = (int)Math.Sqrt(distanceOnXAxel * distanceOnXAxel + distanceOnYAxel * distanceOnYAxel);

            if (radialDistance > 2) return;

            int targetX = X;
            int targetY = Y;

            if (Math.Abs(distanceOnXAxel) >= Math.Abs(distanceOnYAxel))
            {
                if (distanceOnXAxel > 0)
                {
                    targetX++;
                }
                else targetX--;
            }
            else
            {
                if (distanceOnYAxel > 0)
                {
                    targetY++;
                }
                else targetY--;
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
