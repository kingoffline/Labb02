using Labb02;

LevelData levelData = new LevelData();
levelData.Load(@"C:\Users\arhai\source\repos\Labb02\Level1.txt");
Console.CursorVisible = false;
levelData.DrawMap();
bool running = true;
while (running)
{
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
        }
    }
}