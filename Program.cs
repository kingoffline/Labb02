using Labb02;

LevelData levelData = new LevelData();
levelData.Load(@"C:\Users\arhai\source\repos\kingoffline\Labb02\Level1.txt");
var player = levelData.Elements.OfType<Player>().FirstOrDefault();
Console.CursorVisible = false;
levelData.DrawMap();
bool running = true;
while (running)
{
        ConsoleKey inputKey = Console.ReadKey().Key;

        if (inputKey == ConsoleKey.Escape)
        {
            running = false;
            continue;
        }

        if (inputKey == ConsoleKey.UpArrow ||
            inputKey == ConsoleKey.DownArrow ||
            inputKey == ConsoleKey.LeftArrow ||
            inputKey == ConsoleKey.RightArrow)
        {
            levelData.Move(inputKey);
            if (player is not null && player.HP <= 0)
            {
                Console.Clear();
                Console.SetCursorPosition(0, 0);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("GAME OVER");
                Console.ResetColor();
                running = false;
            }
        }
}
