namespace Labb02
{
    class Player : LevelElement
    {
        public string Name { get; set; }
        public int HP { get; set; }
        public Dice AttackDice { get; set; }
        public Dice DefenseDice { get; set; }
        public Combat Combat { get; set; }
        public Enemy? EngagedEnemyThisTurn { get; private set; }

        public Player(int x, int y) : base(x, y, '@', ConsoleColor.White)
        {
            Name = "Player";
            HP = 100;
            AttackDice = new Dice(2, 6, 2);
            DefenseDice = new Dice(2, 6, 0);
            Combat = new Combat();
        }

        public void Update(ConsoleKey key, List<LevelElement> elements)
        {
            EngagedEnemyThisTurn = null;
            int targetX = X;
            int targetY = Y;

            switch (key)
            {
                case ConsoleKey.UpArrow:
                    targetY = Y - 1;
                    break;
                case ConsoleKey.DownArrow:
                    targetY = Y + 1;
                    break;
                case ConsoleKey.LeftArrow:
                    targetX = X - 1;
                    break;
                case ConsoleKey.RightArrow:
                    targetX = X + 1;
                    break;
                default: return;
            }

            var targetElement = elements.FirstOrDefault(e => e.X == targetX && e.Y == targetY);

            Enemy? enemy = targetElement as Enemy;

            if (targetElement is null)
            {
                Console.SetCursorPosition(X, Y);
                Console.Write(' ');

                X = targetX;
                Y = targetY;
            }

            // EngagedEnemyThisTurn lagrar vilken fiende spelaren har stridit mot den här rundan.
            if (enemy is not null && HP > 0)
            {
                Combat.PlayerCombat(this, enemy);
                EngagedEnemyThisTurn = enemy;
            }
        }
    }
}
