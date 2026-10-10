using System;
using System.Collections.Generic;
using System.Text;

namespace Labb02
{
    internal class Dice
    {
        public int Sides { get; set; }
        public int NumberOfDices { get; set; }
        public int Modifier { get; set; }
        public Dice(int numberOfDices, int sides, int modifier)
        {
            NumberOfDices = numberOfDices;
            Sides = sides;
            Modifier = modifier;
        }

        public int Throw()
        {
            int total = 0;
            Random random = new Random();
            for (int i = 0; i < NumberOfDices; i++)
            {
                total += random.Next(1, Sides + 1);
            }
            return total + Modifier;
        }

        public override string ToString()
        {
            return $"{NumberOfDices}d{Sides}{(Modifier >= 0 ? "+" : "")}{Modifier}";
        }
    }
}
