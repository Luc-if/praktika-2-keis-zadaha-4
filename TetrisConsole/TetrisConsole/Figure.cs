using System;

namespace TetrisConsole
{
    class Figure
    {
        private int[][,] shapes = new int[][,]
        {
            new int[,] { {1, 1, 1, 1} }, // I
            new int[,] { {1, 1, 0}, {0, 1, 1} }, // Z
            new int[,] { {0, 1, 1}, {1, 1, 0} }, // S
            new int[,] { {1, 1}, {1, 1} }, // O
            new int[,] { {1, 1, 1}, {0, 1, 0} }, // T
            new int[,] { {1, 1, 1}, {1, 0, 0} }, // L
            new int[,] { {1, 1, 1}, {0, 0, 1} }  // J
        };

        public int[,] currentShape { get; private set; }
        public int X { get; private set; }
        public int Y { get; private set; }

        public Figure()
        {
            Random rand = new Random();
            currentShape = shapes[rand.Next(shapes.Length)];
            X = 3; //  X
            Y = 0; //  Y
        }

        public bool CanMove(int dx, int dy, GameField field)
        {
            for (int i = 0; i < currentShape.GetLength(0); i++)
            {
                for (int j = 0; j < currentShape.GetLength(1); j++)
                {
                    if (currentShape[i, j] == 1)
                    {
                        int newX = X + j + dx;
                        int newY = Y + i + dy;

                        if (newX < 0 || newX >= field.Width || newY >= field.Height)
                            return false;

                        if (field.Grid[newY, newX] == 1)
                            return false;
                    }
                }
            }
            return true;
        }

        public void Move(int dx, int dy, GameField field)
        {
            if (CanMove(dx, dy, field))
            {
                X += dx;
                Y += dy;
            }
        }

        public void Rotate(GameField field)
        {
            int rows = currentShape.GetLength(0);
            int cols = currentShape.GetLength(1);
            int[,] rotatedShape = new int[cols, rows];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    rotatedShape[j, rows - 1 - i] = currentShape[i, j];
                }
            }

            int oldX = X;
            int oldY = Y;
            currentShape = rotatedShape;

            if (!CanMove(0, 0, field))
            {
                currentShape = rotatedShape;
                currentShape = new int[rows, cols];
                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < cols; j++)
                    {
                        currentShape[i, j] = rotatedShape[j, rows - 1 - i];
                    }
                }
            }
        }
    }
}