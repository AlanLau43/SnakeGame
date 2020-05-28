using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    class Menu
    {
        private int difficulty;

        public Menu()
        {
            difficulty = 0;
        }

        public void DrawMenu()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition((Console.WindowWidth / 2)-10, (Console.WindowHeight / 4) - 6);
            Console.Write("Snake Game");
            Console.SetCursorPosition((Console.WindowWidth / 2)-10, (Console.WindowHeight / 4) - 4);
            Console.Write("How to play: ");
            Console.SetCursorPosition((Console.WindowWidth / 2) - 10, (Console.WindowHeight / 4) - 2);
            Console.Write("\u005E Move Up");
            Console.SetCursorPosition((Console.WindowWidth / 2) - 10, (Console.WindowHeight / 4) - 1);
            Console.Write("\u02C5 Move Down");
            Console.SetCursorPosition((Console.WindowWidth / 2) - 10, (Console.WindowHeight / 4) );
            Console.Write("\u003C Move Left");
            Console.SetCursorPosition((Console.WindowWidth / 2) - 10, (Console.WindowHeight / 4) + 1);
            Console.Write("\u003E Move Right");
            Console.SetCursorPosition((Console.WindowWidth / 2) - 10, (Console.WindowHeight / 4) + 2);
            Console.Write("r Restart");
            Console.SetCursorPosition((Console.WindowWidth / 2) - 15, (Console.WindowHeight / 4) + 4);
            Console.Write("Difficulty Selection: ");
            if (difficulty == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.White;
            }

            Console.SetCursorPosition((Console.WindowWidth / 2) - 8, (Console.WindowHeight / 2) - 2);
            Console.WriteLine("Easy");
            if (difficulty == 1)
            {
                Console.ForegroundColor = ConsoleColor.Red;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.White;
            }
            Console.SetCursorPosition((Console.WindowWidth / 2) - 8, (Console.WindowHeight / 2));
            Console.WriteLine("Normal");
            if (difficulty == 2)
            {
                Console.ForegroundColor = ConsoleColor.Red;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.White;
            }
            Console.SetCursorPosition((Console.WindowWidth / 2) - 8, (Console.WindowHeight / 2) + 2);
            Console.WriteLine("Hard");
        }

        public void SelectDiff(ConsoleKeyInfo x)
        {
            if (x.Key == ConsoleKey.UpArrow)
            {
                if (difficulty == 0)
                {
                    difficulty = 2;
                }
                else
                {
                    difficulty -= 1;
                }

            }
            else if (x.Key == ConsoleKey.DownArrow)
            {
                if (difficulty == 2)
                {
                    difficulty = 0;
                }
                else
                {
                    difficulty += 1;
                }
            }
        }
        public int GetDiff()
        {
            return difficulty;
        }
    }
}
