using System;
using System.Threading;

namespace TetrisConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Tetris";
            Console.CursorVisible = false;

            while (true)
            {
                GameField field = new GameField(10, 20);
                Figure currentFigure = new Figure();
                int score = 0;
                bool gameOver = false;

                while (!gameOver)
                {
                    if (Console.KeyAvailable)
                    {
                        var key = Console.ReadKey(true).Key;
                        switch (key)
                        {
                            case ConsoleKey.LeftArrow:
                                currentFigure.Move(-1, 0, field);
                                break;
                            case ConsoleKey.RightArrow:
                                currentFigure.Move(1, 0, field);
                                break;
                            case ConsoleKey.DownArrow:
                                currentFigure.Move(0, 1, field);
                                break;
                            case ConsoleKey.UpArrow:
                                currentFigure.Rotate(field);
                                break;
                        }
                    }

                    if (currentFigure.CanMove(0, 1, field))
                    {
                        currentFigure.Move(0, 1, field);
                    }
                    else
                    {
                        field.AddFigure(currentFigure);
                        int clearedLines = field.CheckForLines();
                        score += clearedLines * 100;

                        currentFigure = new Figure();
                        if (!currentFigure.CanMove(0, 0, field))
                        {
                            gameOver = true;
                        }
                    }

                    field.Draw(currentFigure);
                    Console.WriteLine($"Счет: {score}");
                    Thread.Sleep(300);
                }

                Console.Clear();
                Console.WriteLine("Игра окончена! Ваш счет: " + score);
                Console.WriteLine("Нажмите Enter, чтобы начать заново...");
                Console.ReadLine();
            }
        }
    }
}