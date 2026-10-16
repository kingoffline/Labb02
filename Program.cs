using Labb02;
using Spectre.Console;
using DrawingColor = System.Drawing.Color;

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
            var gameOver = new FigletText("GAME OVER")
            {
                Color = Spectre.Console.Color.Red,
                Justification = Justify.Center,
            };
            AnsiConsole.Write(gameOver);
            Console.ResetColor();
            running = false;
        }
    }
}
