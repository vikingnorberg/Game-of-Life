using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Threading;

namespace gameOfLifeV2
{

    class Program
    {
        public static int NeighbourCount(int[,] array, int x, int y)
        {
            int neighours = 0;

            if (array[x, y] != 3)
            {
                if (array[x + 1, y    ] == 1) { neighours++; }
                if (array[x + 1, y - 1] == 1) { neighours++; }
                if (array[x    , y - 1] == 1) { neighours++; }
                if (array[x - 1, y - 1] == 1) { neighours++; }
                if (array[x - 1, y    ] == 1) { neighours++; }
                if (array[x - 1, y + 1] == 1) { neighours++; }
                if (array[x    , y + 1] == 1) { neighours++; }
                if (array[x + 1, y + 1] == 1) { neighours++; }
            }
            return neighours;
        }

        static void Main(string[] args)
        {
            var ascii = new List<string>();

            Random rnd = new Random();

            int xDimension = 60;
            int yDimension = 30;
            int startAmount = (xDimension * yDimension) / 2;

            /* The first gen matrix */
            int[,] matrix = new int[yDimension, xDimension];

            /* The next gen matrix */
            int[,] nextGenMatrix = new int[yDimension, xDimension];

            /* Populates a given amout of cells*/
            for (int i = 0; i <= startAmount; i++)
            {
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

            // Prints the sexy logo. 
            Console.Clear();
            Console.WriteLine("  _____     _ _     _              _____     _                 _        ");
            Console.WriteLine(" |     |___| | |_ _| |___ ___     |  _  |_ _| |_ ___ _____ ___| |_ ___  ");
            Console.WriteLine(" |   --| -_| | | | | | .'|  _|    |     | | |  _| . |     | .'|  _| .'| ");
            Console.WriteLine(" |_____|___|_|_|___|_|__,|_|      |__|__|___|_| |___|_|_|_|__,|_| |__,| ");
            Console.WriteLine("                                                                        ");
            Console.WriteLine("           _____                      ___    __    _ ___                ");
            Console.WriteLine("          |   __|___ _____ ___    ___|  _|  |  |  |_|  _|___            ");
            Console.WriteLine("          |  |  | .'|     | -_|  | . |  _|  |  |__| |  _| -_|           ");
            Console.WriteLine("          |_____|__,|_|_|_|___|  |___|_|    |_____|_|_| |___|           ");
            Console.WriteLine("                                                                        ");
            Console.WriteLine("                                                                        ");
            Console.WriteLine(" Erik V. Norberg");
            Console.WriteLine(" GitHub: vikignnorberg");
            Console.WriteLine(" Tue, 29 Sep 2020");
            Console.WriteLine(" ");
            Console.WriteLine(" ");

            bool running = true;

            while (running)
            {
                /* Makes and resets the ghost cells */
                for (int i = 0; i < xDimension; i++)
                {
                    matrix[0, i] = 3;
                    matrix[yDimension - 1, i] = 3;
                }
                for (int i = 0; i < yDimension; i++)
                {
                    matrix[i, 0] = 3;
                    matrix[i, xDimension - 1] = 3;
                }

                /* Creates the new generation in newGenMatrix */
                for (int i = 0; i < yDimension; i++)
                {
                    for (int j = 0; j < xDimension; j++)
                    {
                        var neighbours = NeighbourCount(matrix, i, j);

                        if (matrix[i, j] == 1)
                        {
                            if (neighbours < 2) { nextGenMatrix[i, j] = 0; }
                            else if (neighbours == 2 || neighbours == 3) { nextGenMatrix[i, j] = 1; }
                            else if (neighbours > 3) { nextGenMatrix[i, j] = 0; }
                        }
                        else if (matrix[i, j] == 0)
                        {
                            if (neighbours == 3) { nextGenMatrix[i, j] = 1; }
                        }
                        else
                        {
                            nextGenMatrix[i, j] = 3;
                        }

                    }
                }

                /* Prints the new generation matrix */
                for (int i = 0; i < yDimension; i++)
                {
                    for (int j = 0; j < xDimension; j++)
                    {
                        if (nextGenMatrix[i, j] == 0)
                        {
                            ascii.Add(" ");
                        }
                        else if (nextGenMatrix[i, j] == 1)
                        {
                            ascii.Add("█");
                        }
                        else
                        {
                            ascii.Add("#");
                        }
                    }
                    Console.WriteLine(string.Join(" ", ascii));
                    ascii.Clear();
                }

                /* Copys over the new generation to the old generation */
                for (int i = 0; i < yDimension; i++)
                {
                    for (int j = 0; j < xDimension; j++)
                    {
                        matrix[i, j] = nextGenMatrix[i, j];
                    }
                }
                Console.CursorVisible = false;
                Console.SetCursorPosition(0, 16);
                Thread.Sleep(500);
            }
        }
    }
}
