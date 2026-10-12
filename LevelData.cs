using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using System.Xml.Linq;

namespace Labb02
{
    internal class LevelData
    {
        private List<LevelElement> _elements = new List<LevelElement>();
        public bool isDiscovered { get; set; } = false;
        public List<LevelElement> Elements
        {
            get { return _elements; }
        }

        public void Load(string fileName)
        {
            using (StreamReader sr = new StreamReader(fileName))
            {
                int x = 0;
                int y = 0;

                for (int i = sr.Read(); i != -1; i = sr.Read())
                {
                    char c = (char)i;
                    if (c == '\n')
                    {
                        x = 0;
                        y++;
                        continue;
                    }
                    switch (c)
                    {
                        case '#':
                            _elements.Add(new Wall(x, y));
                            x++;
                            break;
                        case 'r':
                            _elements.Add(new Rat(x, y));
                            x++;
                            break;
                        case 's':
                            _elements.Add(new Snake(x, y));
                            x++;
                            break;
                        case '@':
                            _elements.Add(new Player(x, y));
                            x++;
                            break;
                        case ' ':
                            x++;
                            break;
                        default : break;
                    }

                }
            }
        }

        public void DrawMap()
        {
            var player = _elements.OfType<Player>().FirstOrDefault();

            foreach (var element in _elements)
            {
                int distanceOnXAxel = element.X - player.X;
                int distanceOnYAxel = element.Y - player.Y;
                int radialDistance = (int)Math.Sqrt(distanceOnXAxel * distanceOnXAxel + distanceOnYAxel * distanceOnYAxel);

                if (radialDistance < 5)
                {
                    isDiscovered = true;
                    element.Draw();
                } else if (element.Symbol == '#' && isDiscovered)
                {
                    Console.SetCursorPosition(element.X, element.Y);
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                } else
                {
                    Console.SetCursorPosition(element.X, element.Y);
                    Console.Write(' ');
                }
            }
        }

        public void Move(ConsoleKey key)
        {
            var player = _elements.OfType<Player>().FirstOrDefault();
            var allEnemies = _elements.OfType<Enemy>();

            player?.Update(key, _elements);

            foreach (var enemy in allEnemies)
            {
                enemy.Update(player, _elements);
            }

            DrawMap();
        }

    }
}
