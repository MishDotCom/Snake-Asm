using System;
using System.Threading;

namespace Snake
{
    class Program
    {
        static int h = 25;
        static int w = 50;
        static int[] X = new int[250];
        static int[] Y = new int[250];
        static int fX;
        static int fY;
        static int bodyCnt = 3; //starting size
        static ConsoleKeyInfo keyInfo = new ConsoleKeyInfo();
        static char key = ' ';
        static bool gameOver = false;
        static bool gameStarted = false;
        static Random seed = new Random();
        static void Main()
        {
            Console.Title = "Snake";
            Console.WriteLine("Snake - mdc - v1.0");
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            Console.CursorVisible = false;
            Console.WindowWidth = 51;
            Console.WindowHeight = 26;
            Console.BufferWidth = Console.WindowWidth;
            Console.BufferHeight = Console.WindowHeight;
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write(Banners.snake);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\n\n               Press enter to start...");
            Console.ReadLine();
            //Console.ReadKey();
            RunGame();
        }

        static void GenerateCanvas()
        {
            //■
            Console.Clear();
            for(int i = 0; i < w; i++)
            {
                //top line
                Console.SetCursorPosition(i, 1);
                Console.Write("■");
            }
            for(int i = 1; i < h; i++)
            {
                Console.SetCursorPosition(0, i);
                Console.Write($"■");
            }
            for(int i = 1; i < h; i++)
            {
                Console.SetCursorPosition(w-1, i);
                Console.Write($"■");
            }
            for(int i = 1; i < w; i++)
            {
                Console.SetCursorPosition(i, h-1);
                Console.Write($"■");
            }
        }

        static void GenBrd()
        {
            Console.Clear();
        }

        public static void Input()
        {
            if(Console.KeyAvailable)
            {
                keyInfo = Console.ReadKey(true);
                key = keyInfo.KeyChar;
            }
        }

        public static void DrawPoint(int x, int y, string ch, ConsoleColor color)
        {
            Console.SetCursorPosition(x, y);
            Console.ForegroundColor = color;
            Console.Write(ch);
            Console.ForegroundColor = ConsoleColor.Cyan;
        }

        public static void Game()
        {
            try
            {
                Random seed = new Random();
                if(X[0] == fX && Y[0] == fY)
                {
                    bodyCnt++;
                    fX = seed.Next(2, (w - 2));
                    fY = seed.Next(2, (h - 2));
                }
                for(int i = bodyCnt; i > 1; i--)
                {
                    X[i - 1] = X[i - 2];
                    Y[i - 1] = Y[i - 2];
                }
                switch(key)
                {
                    case 'w':
                        Y[0]--;
                        gameStarted = true;
                        break;
                    case 'a':
                        X[0]--;
                        gameStarted = true;
                        break;
                    case 's':
                        Y[0]++;
                        gameStarted = true;
                        break;
                    case 'd':
                        X[0]++;
                        gameStarted = true;
                        break;
                }
                for(int i = 0; i <= (bodyCnt-1); i++)
                {
                    DrawPoint(X[i], Y[i], "■", ConsoleColor.Green);
                    DrawPoint(fX, fY, "▣", ConsoleColor.Red);
                }
                Thread.Sleep(50);
            }
            catch
            {
                GameOver();
            } 
        }

        public static void GameOver()
        {
            gameStarted = false;
            gameOver = true;
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(Banners.gameOver);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"\n\n                     Score: {bodyCnt}");
            Console.WriteLine("" + "               Process exited 0x00f...\n");
            Console.WriteLine("                       Menu :");
            Console.WriteLine("               - Press R to restart.");
            Console.WriteLine("               - Press E to exit.");
            Console.WriteLine("           Any other key will kill the task.");
            Console.CursorVisible = false;
            while(true)
            {
                keyInfo = Console.ReadKey(true);
                char key = keyInfo.KeyChar;
                if(key == 'r' || key == 'R')
                {
                    gameOver = false;
                    RunGame();
                    break;
                }
                else if(key == 'e' || key == 'E')
                {
                    Environment.Exit(0);
                }
            }
        }

        public static void RunGame()
        {
            X = new int[250];
            Y = new int[250];
            bodyCnt = 3;
            X[0] = 5;
            Y[0] = 5;
            fX = seed.Next(2, (w - 2));
            fY = seed.Next(2, (h - 2));
            keyInfo = new ConsoleKeyInfo();
            key = ' ';
            while(true)
            {
                if(gameOver)
                    break;
                //GenerateCanvas();
                GenBrd();
                Input();
                Game();
            }
        }
    }

    class Banners
    {
        public static string gameOver = "\n\n\n\n\n"+ 
                                        "  " +" _____                     _____\n"  +          
                                        "  " +"|   __| ___  _____  ___   |     | _ _  ___  ___ \n"+
                                        "  " +"|  |  || .'||     || -_|  |  |  || | || -_||  _|\n"+
                                       "  " +@"|_____||__,||_|_|_||___|  |_____| \_/ |___||_| " + "\n"  ;
        
        public static string snake = "\n\n            " +" _____            _\n" +    
                                    "            " +"|   __| ___  ___ | |_  ___\n" +
                                    "            " +"|__   ||   || .'|| '_|| -_|\n" +
                                    "            " +"|_____||_|_||__,||_,_||___|\n";
    }
}