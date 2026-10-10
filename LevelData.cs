using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace Labb02
{
    internal class LevelData
    {
        private List<LevelElement> _elements = new List<LevelElement>();

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
            foreach (LevelElement element in _elements)
            {
                element.Draw();
            }
        }

        public void Move(ConsoleKey key)
        {
            var player = _elements.OfType<Player>().FirstOrDefault();
            var allEnemies = _elements.OfType<Enemy>();

            if (player == null) return;

            player.Update(key, _elements);
            player.Draw();

            foreach (var enemy in allEnemies)
            {
                enemy.Update(player, _elements);
                enemy.Draw();
            }
        }

    }
}
