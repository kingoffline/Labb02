using Labb02;

LevelData levelData = new LevelData();
levelData.Load(@"C:\Users\arhai\source\repos\Labb02\Level1.txt");
levelData.DrawMap();
bool running = true;
while (running)
{
    switch (Console.ReadKey().Key)
    {
        case ConsoleKey.UpArrow:
            levelData.PlayerMovement(ConsoleKey.UpArrow);
            levelData.DrawMap();
            break;
        case ConsoleKey.DownArrow:
            levelData.PlayerMovement(ConsoleKey.DownArrow);
            levelData.DrawMap();
            break;
        case ConsoleKey.LeftArrow:
            levelData.PlayerMovement(ConsoleKey.LeftArrow);
            levelData.DrawMap();
            break;
        case ConsoleKey.RightArrow:
            levelData.PlayerMovement(ConsoleKey.RightArrow);
            levelData.DrawMap();
            break;
        case ConsoleKey.Escape: 
            running = false;
            break;
    }
}