using System;

namespace TetrisConsole
{
    class GameField
    {
        public int[,] Grid { get; private set; }
        public int Width { get; private set; }
        public int Height { get; private set; }

        public GameField(int width, int height)
        {
            Width = width;
            Height = height;
            Grid = new int[height, width];
        }

        public void Draw(Figure currentFigure)
        {
            Console.Clear();
            int[,] tempGrid = (int[,])Grid.Clone();

            for (int i = 0; i < currentFigure.currentShape.GetLength(0); i++)
            {
                for (int j = 0; j < currentFigure.currentShape.GetLength(1); j++)
                {
                    if (currentFigure.currentShape[i, j] == 1)
                    {
                        if (currentFigure.Y + i >= 0 && currentFigure.Y + i < Height &&
                            currentFigure.X + j >= 0 && currentFigure.X + j < Width)
                        {
                            tempGrid[currentFigure.Y + i, currentFigure.X + j] = 1;
                        }
                    }
                }
            }

            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    Console.Write(tempGrid[i, j] == 1 ? "■ " : ". ");
                }
                Console.WriteLine();
            }
        }

        public void AddFigure(Figure figure)
        {
            for (int i = 0; i < figure.currentShape.GetLength(0); i++)
            {
                for (int j = 0; j < figure.currentShape.GetLength(1); j++)
                {
                    if (figure.currentShape[i, j] == 1)
                    {
                        Grid[figure.Y + i, figure.X + j] = 1;
                    }
                }
            }
        }

        public int CheckForLines()
        {
            int clearedLines = 0;
            for (int i = Height - 1; i >= 0; i--)
            {
                bool isFull = true;
                for (int j = 0; j < Width; j++)
                {
                    if (Grid[i, j] == 0)
                    {
                        isFull = false;
                        break;
                    }
                }

                if (isFull)
                {
                    for (int k = i; k > 0; k--)
                    {
                        for (int l = 0; l < Width; l++)
                        {
                            Grid[k, l] = Grid[k - 1, l];
                        }
                    }
                    for (int l = 0; l < Width; l++)
                    {
                        Grid[0, l] = 0;
                    }
                    clearedLines++;
                    i++; 
                }
            }
            return clearedLines;
        }
    }
}