using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using System.Linq;

namespace Labb02
{
    abstract class LevelElement
    {
        public int X { get; set; }
        public int Y { get; set; }
        public char Symbol { get; set; }
        public ConsoleColor ColorOfSymbol { get; set; }

        public LevelElement(int x, int y, char symbol, ConsoleColor colorOfSymbol)
        {
            X = x;
            Y = y;
            Symbol = symbol;
            ColorOfSymbol = colorOfSymbol;
        }

        public void Draw()
        {
            Console.SetCursorPosition(X, Y);
            Console.ForegroundColor = ColorOfSymbol;
            Console.Write(Symbol);
        }
    }

    class Wall : LevelElement
    {
        public Wall(int x, int y) : base(x, y, '#', ConsoleColor.Gray)
        {
        }
    }

    abstract class Enemy : LevelElement
    {
        public string Name { get; set; }
        public int HP { get; set; }
        public Dice AttackDice { get; set; }
        public Dice DefenseDice { get; set; }
        public Enemy(int x, int y, string name, int hp, ConsoleColor color, char symbol) : base(x, y, symbol, color)
        {
            Name = name;
            HP = hp;
        }

        public abstract void Update(Player player, List<LevelElement> elements);
    }

    class Rat : Enemy
    {
        private static readonly Random rn = new Random();
        public Rat(int x, int y) : base(x, y, "Rat", 10, ConsoleColor.Red, 'r')
        {
            AttackDice = new Dice(1, 6, 3);
            DefenseDice = new Dice(1, 6, 1);
        }
        public override void Update(Player player, List<LevelElement> elements)
        {
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

    class Snake : Enemy
    {
        public Snake(int x, int y) : base(x, y, "Snake", 25, ConsoleColor.Green, 's')
        {
            AttackDice = new Dice(3, 4, 2);
            DefenseDice = new Dice(1, 8, 5);
        }
        public override void Update(Player player, List<LevelElement> elements)
        {

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

    class Player : LevelElement
    {
        public string Name { get; set; }
        public int HP { get; set; }
        public Dice AttackDice { get; set; }
        public Dice DefenseDice { get; set; }

        public Player(int x, int y) : base(x, y, '@', ConsoleColor.White)
        {
            Name = "Player";
            HP = 100;
            AttackDice = new Dice(2, 6, 2);
            DefenseDice = new Dice(2, 6, 0);
        }

        public void Update(ConsoleKey key, List<LevelElement> elements)
        {
            int targetX = X;
            int targetY = Y;

            switch (key)
            {
                case ConsoleKey.UpArrow: targetY = Y - 1;
                    break;
                case ConsoleKey.DownArrow: targetY = Y + 1;
                    break;
                case ConsoleKey.LeftArrow: targetX = X - 1;
                    break;
                case ConsoleKey.RightArrow: targetX = X + 1;
                    break;
                default: return;
            }

            bool isBlocked = elements.Any(e => e != this && e.X == targetX && e.Y == targetY);

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
