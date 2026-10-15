using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using System.Linq;
using System.Numerics;

namespace Labb02
{
    abstract class LevelElement
    {
        public int X { get; set; }
        public int Y { get; set; }
        public char Symbol { get; set; }
        public ConsoleColor ColorOfSymbol { get; set; }
        public bool isDiscovered { get; set; } = false;

        public LevelElement(int x, int y, char symbol, ConsoleColor colorOfSymbol)
        {
            X = x;
            Y = y;
            Symbol = symbol;
            ColorOfSymbol = colorOfSymbol;
            isDiscovered = false;

        }

        public void Draw()
        {
            Console.SetCursorPosition(X, Y);
            Console.ForegroundColor = ColorOfSymbol;
            Console.Write(Symbol);
        }
    }

    abstract class Enemy : LevelElement
    {
        public string Name { get; set; }
        public int HP { get; set; }
        public Dice AttackDice { get; set; }
        public Dice DefenseDice { get; set; }
        public Combat Combat { get; set; }
        public Enemy(int x, int y, string name, int hp, ConsoleColor color, char symbol) : base(x, y, symbol, color)
        {
            Name = name;
            HP = hp;
            Combat = new Combat();
        }

        public abstract void Update(Player? player, List<LevelElement> elements);

    }
}
