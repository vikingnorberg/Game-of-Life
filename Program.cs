using System;
using System.Collections.Generic;
using System.Threading;

namespace test
{
    class Program
    {
        public static int neighbourCount(int[,] array, int CellX, int CellY)
        {
            int count = 0;

            int minX = Math.Max(CellX - 1, array.GetLowerBound(0));
            int maxX = Math.Min(CellX + 1, array.GetUpperBound(0));
            int minY = Math.Max(CellY - 1, array.GetLowerBound(1));
            int maxY = Math.Min(CellY + 1, array.GetUpperBound(1));

            System.Diagnostics.Debug.WriteLine(string.Format("[{0},{1}] : [{2},{3}] - [{4},{5}]", CellX, CellY, minX, maxX, minY, maxY), "NeighbourCount");

            for (int x = minX; x <= maxX; x++)
            {
                for (int y = minY; y <= maxY; y++)
                {
                    if (array[x, y] == 1)
                        count++;
                }
            }
            return count;
        }


        static void Main(string[] args)
        {
            Random rnd = new Random();

            // The dimensions of the matrix.
            int xDimension = 60; //60
            int yDimension = 30; //30

            // The printing matrix.
            int[,] matrix = new int[yDimension, xDimension];

            // The copy matrix.
            int[,] copyMatrix = new int[yDimension, xDimension];

            // The number of random live cells at the start of in the simulation.
            int cellsToPopulate = 750; //750

            // The loop that populates the matrix.
            for (int i = 0; i <= cellsToPopulate; i++)
            {
                // Generates a random x and y for the live cell.
                int randomX = rnd.Next(0, xDimension);
                int randomY = rnd.Next(0, yDimension);

                // Logic that makes sure it doesnt place two live cells on eatch other.
                if (matrix[randomY, randomX] == 0)
                {
                    matrix[randomY, randomX] = 1;
                }
                else
                {
                    i--;
                }
            }

            bool running = true;

            while (running)
            {
                // Prints the sexy logo. 
                Console.WriteLine("  _____     _ _     _              _____     _                 _        ");
                Console.WriteLine(" |     |___| | |_ _| |___ ___     |  _  |_ _| |_ ___ _____ ___| |_ ___  ");
                Console.WriteLine(" |   --| -_| | | | | | .'|  _|    |     | | |  _| . |     | .'|  _| .'| ");
                Console.WriteLine(" |_____|___|_|_|___|_|__,|_|      |__|__|___|_| |___|_|_|_|__,|_| |__,| ");
                Console.WriteLine(" ");
                Console.WriteLine(" Erik V. Norberg");
                Console.WriteLine(" GitHub: vikignnorberg");
                Console.WriteLine(" Tue, 29 Sep 2020");
                Console.WriteLine(" ");
                Console.WriteLine(" ");
                Console.WriteLine(" ");

                // Prints the matrix in ascii.
                var ascii = new List<string>();

                for (int i = 0; i < matrix.GetLength(0); i++)
                {
                    for (int j = 0; j < matrix.GetLength(1); j++)
                    {
                        if (matrix[i, j] == 0)
                        {
                            ascii.Add(" ");
                        }
                        else if (matrix[i, j] == 1)
                        {
                            ascii.Add("█");
                        }
                    }
                    Console.WriteLine(string.Join(" ", ascii));
                    ascii.Clear();
                }

                Console.WriteLine(" ");
                Console.WriteLine(" ");
                Console.WriteLine(" ");

                // Generates the new generation in the copy matrix.
                for (int i = 0; i < matrix.GetLength(0); i++)
                {
                    for (int j = 0; j < matrix.GetLength(1); j++)
                    {
                        if (matrix[i, j] == 1)
                        {
                            // Each cell with one or no neighbors dies.
                            if (neighbourCount(matrix, i, j) == 1 || neighbourCount(matrix, i, j) == 0) { copyMatrix[i, j] = 0; }

                            // Each cell with four or more neighbors dies.
                            if (neighbourCount(matrix, i, j) >= 4) { copyMatrix[i, j] = 0; }

                            // Each cell with two or three neighbors survives.
                            if (neighbourCount(matrix, i, j) == 2 || neighbourCount(matrix, i, j) == 3) { copyMatrix[i, j] = 1; }
                        }
                        else if (matrix[i, j] == 0)
                        {
                            // Each cell with three neighbors becomes populated
                            if (neighbourCount(matrix, i, j) == 3) { copyMatrix[i, j] = 1; }
                        }
                    }
                }

                // Clears the matrix
                for (int i = 0; i < matrix.GetLength(0); i++)
                {
                    for (int j = 0; j < matrix.GetLength(1); j++)
                    {
                        matrix[i, j] = 0;
                    }
                }

                // Replaces matrix with the copy matrix
                for (int i = 0; i < matrix.GetLength(0); i++)
                {
                    for (int j = 0; j < matrix.GetLength(1); j++)
                    {
                        if (copyMatrix[i, j] == 0)
                        {
                            matrix[i, j] = 0;
                        }
                        else if (copyMatrix[i, j] == 1)
                        {
                            matrix[i, j] = 1;
                        }
                    }
                }

                // Clears the copy matrix
                for (int i = 0; i < matrix.GetLength(0); i++)
                {
                    for (int j = 0; j < matrix.GetLength(1); j++)
                    {
                        copyMatrix[i, j] = 0;
                    }
                }

                Thread.Sleep(200);
                Console.SetCursorPosition(0, 0);
            }
        }
    }
}
