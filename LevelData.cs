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
        public List<LevelElement> Elements
        {
            get { return _elements; }
        }

        public void Load(string fileName)
        {
            string levelPath = Path.Combine(AppContext.BaseDirectory, fileName);
            using (StreamReader sr = new StreamReader(levelPath))
            {
                int x = 0;
                int y = 2;

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
                        default: break;
                    }

                }
            }
        }

        public void DrawMap()
        {
            var player = _elements.OfType<Player>().FirstOrDefault();
            if (player is null)
                return;

            foreach (var element in _elements)
            {
                int dx = element.X - player.X;
                int dy = element.Y - player.Y;
                int distanceSquared = (int)Math.Sqrt((dx * dx) + (dy * dy));

                // Rita elementet inom synhåll och markera det som upptäckt.
                if (distanceSquared <= 4)
                {
                    element.Draw();
                    element.isDiscovered = true;
                }
                // Visa tidigare upptäckta väggar i mörkgrått även utanför synhåll.
                else if (element is Wall && element.isDiscovered)
                {
                    Console.SetCursorPosition(element.X, element.Y);
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.Write(element.Symbol);
                }
                // Rensa positionen när elementet inte ska visas.
                else
                {
                    Console.SetCursorPosition(element.X, element.Y);
                    Console.Write(' ');
                }
            }
        }

        public void Move(ConsoleKey key)
        {
            ClearCurrentConsoleLine(0);
            ClearCurrentConsoleLine(1);
            var player = _elements.OfType<Player>().FirstOrDefault();

            if (player is null || player.HP <= 0)
                return;

            player.Update(key, _elements);

            Enemy? engagedEnemy = player.EngagedEnemyThisTurn;

            // tar bort fienden, spelaren har stridit mot om det är inte null och HP <= 0
            if (engagedEnemy is not null && engagedEnemy.HP <= 0)
                RemoveEnemy(engagedEnemy);

            var enemies = _elements.OfType<Enemy>().ToList();

            // Går igenom fienderna och anropar deras Update-method efter att spelarens Update-metod har körts.
            foreach (var enemy in enemies)
            {
                if (player.HP <= 0)
                    break;

                // Hoppa över fienden som spelaren redan har stridit mot den här rundan.
                if (enemy == engagedEnemy)
                    continue;

                enemy.Update(player, _elements);

                if (enemy.HP <= 0)
                    RemoveEnemy(enemy);
            }

            // Ritar upp banan
            DrawMap();
        }


        private void RemoveEnemy(Enemy enemy)
        {
            Console.SetCursorPosition(enemy.X, enemy.Y);
            Console.Write(' ');
            _elements.Remove(enemy);
        }

        private void ClearCurrentConsoleLine(int y)
        {
            Console.SetCursorPosition(0, y);
            Console.Write(new string(' ', Console.WindowWidth));
        }
    }
}
