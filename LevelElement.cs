using System;
using System.Collections.Generic;
using System.Text;

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

        public abstract void Update();
    }

    class Rat : Enemy
    {
        
        public Rat(int x, int y) : base(x, y, "Rat", 10, ConsoleColor.Red, 'r')
        {
            AttackDice = new Dice(1, 6, 3);
            DefenseDice = new Dice(1, 6, 1);
        }
        public override void Update()
        {
           
        }
    }
    
    class Snake : Enemy
    {
        public Snake(int x, int y) : base(x, y, "Snake", 25, ConsoleColor.Green, 's')
        {
            AttackDice = new Dice(3, 4, 2);
            DefenseDice = new Dice(1, 8, 5);
        }
        public override void Update()
        {
        }
    }

    class Player : Enemy
    {
        public Player(int x, int y) : base (x, y, "Player", 100, ConsoleColor.White, '@')
        {
            AttackDice = new Dice(2, 6, 2);
            DefenseDice = new Dice(2, 6, 0);
        }

        public override void Update()
        {

        }

    }

}
