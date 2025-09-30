using System;
using System.Threading;

class Program
{
    static void Main()
    {
        int width = 30;
        int height = 20;
        int birdY = height / 2;
        int birdX = 5;
        int score = 0;
        int gravity = 1;
        int velocity = 0;

        Random rnd = new Random();
        int[] pipeX = { width, width + 15 };
        int[] pipeHeight = { rnd.Next(3, height - 5), rnd.Next(3, height - 5) };

        Console.CursorVisible = false;

        while (true)
        {
            // Kullanıcı girişi
            if (Console.KeyAvailable)
            {
                var key = Console.ReadKey(true).Key;
                if (key == ConsoleKey.Spacebar)
                {
                    velocity = -3; // zıplama
                }
            }

            // Kuş hareketi
            velocity += gravity;
            birdY += velocity;
            if (birdY < 0) birdY = 0;
            if (birdY >= height) break; // yere çarptı = oyun bitti

            // Boru hareketi
            for (int i = 0; i < pipeX.Length; i++)
            {
                pipeX[i]--;
                if (pipeX[i] < 0)
                {
                    pipeX[i] = width;
                    pipeHeight[i] = rnd.Next(3, height - 5);
                    score++;
                }
            }

            // Çarpışma kontrolü
            for (int i = 0; i < pipeX.Length; i++)
            {
                if (birdX == pipeX[i] && (birdY < pipeHeight[i] || birdY > pipeHeight[i] + 5))
                {
                    goto GameOver;
                }
            }

            // Ekranı temizle ve çiz
            Console.Clear();
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    bool printed = false;

                    // Kuş
                    if (x == birdX && y == birdY)
                    {
                        Console.Write("O");
                        printed = true;
                    }

                    // Borular
                    for (int i = 0; i < pipeX.Length; i++)
                    {
                        if (x == pipeX[i] && (y < pipeHeight[i] || y > pipeHeight[i] + 5))
                        {
                            Console.Write("|");
                            printed = true;
                        }
                    }

                    if (!printed) Console.Write(" ");
                }
                Console.WriteLine();
            }

            Console.WriteLine("Score: " + score);
            Thread.Sleep(100);
        }

    GameOver:
        Console.Clear();
        Console.WriteLine("Oyun Bitti! Skorunuz: " + score);
        Console.ReadKey();
    }
}
