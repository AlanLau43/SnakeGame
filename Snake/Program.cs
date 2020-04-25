using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Threading;
using System.IO;

namespace Snake
{
    struct Position
    {
        public int row;
        public int col;
        public Position(int row, int col)
        {
            this.row = row;
            this.col = col;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            System.Media.SoundPlayer player = new System.Media.SoundPlayer();
            player.SoundLocation = "../../music/backgroundmusic.wav";
            player.Play();

            Console.SetWindowSize(120, 30);
            /*
            byte right = 0;
            byte left = 1;
            byte down = 2;
            byte up = 3;
            */
            //Create a snake object
            Snake snake = new Snake();

            //Create obstacles object
            Obstacle obstacle = new Obstacle();

            //Create a Food Object 
            Food food = new Food();
            int lastFoodTime = 0;
            int foodDissapearTime = 14000; //extended the time
            int negativePoints = 0;
            int userPoints = 0;

            double sleepTime = 100;
            Random randomNumbersGenerator = new Random();
            Random randomNumbersGenerator2 = new Random();
            int x;
            int y;
            Console.BufferHeight = Console.WindowHeight;
            lastFoodTime = Environment.TickCount;

            /*
            List<Position> obstacles = new List<Position>()
            {
                new Position(randomNumbersGenerator.Next(0, 30),
                            randomNumbersGenerator.Next(0, 120)),
                new Position(randomNumbersGenerator.Next(0, 30),
                            randomNumbersGenerator.Next(0, 120)),
                new Position(randomNumbersGenerator.Next(0, 30),
                            randomNumbersGenerator.Next(0, 120)),
                new Position(randomNumbersGenerator.Next(0, 30),
                            randomNumbersGenerator.Next(0, 120)),
                new Position(randomNumbersGenerator.Next(0, 30),
                            randomNumbersGenerator.Next(0, 120)),
            };
            */
            foreach (Position ob in obstacle.GetPositions())
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.SetCursorPosition(ob.col, ob.row);
                Console.Write("=");
            }

            Position fd = new Position();
            while (snake.GetSnakeElements().Contains(fd) || obstacle.GetPositions().Contains(fd))
            {
                x = randomNumbersGenerator.Next(0, 30);
                y = randomNumbersGenerator.Next(0, 120);
                fd = food.Generate(x, y);
            }
            
            Console.SetCursorPosition(fd.col, fd.row);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("@");

            snake.SnakeBody();
            
            while (true)
            {
                negativePoints++;
                Console.SetCursorPosition(0,0);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Score: " + (userPoints).ToString());
                Console.WriteLine("Goal: " + 4000);
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo userInput = Console.ReadKey();
                    if (userInput.Key == ConsoleKey.LeftArrow)
                    {
                        if (snake.GetDirection() != Direction.right)
                        {
                            snake.SetDirection(Direction.left);
                        }
                    }
                    else if (userInput.Key == ConsoleKey.RightArrow)
                    {
                        if (snake.GetDirection() != Direction.left)
                        {
                            snake.SetDirection(Direction.right);
                        }
                    }
                    else if (userInput.Key == ConsoleKey.UpArrow)
                    {
                        if (snake.GetDirection() != Direction.down)
                        {
                            snake.SetDirection(Direction.up);
                        }
                    }
                    else if (userInput.Key == ConsoleKey.DownArrow)
                    {
                        if (snake.GetDirection() != Direction.up)
                        {
                            snake.SetDirection(Direction.down);
                        }
                    }
                   
                   }
                /*
                Position snakeHead = snake.GetSnakeElements().Last();
                Position nextDirection = directions[direction];

                Position snakeNewHead = new Position(snakeHead.row + nextDirection.row,
                    snakeHead.col + nextDirection.col);

                if (snakeNewHead.col < 0) snakeNewHead.col = Console.WindowWidth - 1;
                if (snakeNewHead.row < 0) snakeNewHead.row = Console.WindowHeight - 1;
                if (snakeNewHead.row >= Console.WindowHeight) snakeNewHead.row = 0;
                if (snakeNewHead.col >= Console.WindowWidth) snakeNewHead.col = 0;
                */

                Position snakeHead = snake.GetSnakeElements().Last();
                Position snakeNewHead = snake.Move_Snake(snakeHead);

                if (snake.GetSnakeElements().Contains(snakeNewHead) || obstacle.GetPositions().Contains(snakeNewHead) || userPoints == 4000)
                {
                    string path = "../../Score.txt";
                    Console.SetCursorPosition((Console.WindowWidth / 2) - 8, (Console.WindowHeight / 2) - 2);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Game over!");
                    // userPoints = (snakeElements.Count - 6) * 100; //removed negativePoints because it's decreasing too fast.
                    //if (userPoints < 0) userPoints = 0;
                    userPoints = Math.Max(userPoints, 0);
                    Console.WriteLine("\t\t\t\t\t\tYour points are: {0}", userPoints);
                    string exit = "\t\t\t\t\t\tPress Enter to Exit";
                    Console.WriteLine(exit);
                    Console.ForegroundColor = ConsoleColor.Black;
                    if (!File.Exists(path))
                    {
                        // Create a file to write to.
                        Console.WriteLine("Can't Open the File, Please try contact the Developer!! Fk you");
                    }
                    else
                    {
                        using (StreamWriter sw = File.AppendText(path))
                        {
                            sw.WriteLine(userPoints);
                        }
                    }
                    ConsoleKeyInfo userInput = Console.ReadKey();
                    while (true)
                    {
                        if (userInput.Key == ConsoleKey.Enter)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            System.Environment.Exit(1);
                        }
                        
                        userInput = Console.ReadKey();
                        
                    }
                    
                }

                Console.SetCursorPosition(snakeHead.col, snakeHead.row);
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write("*");

                snake.GetSnakeElements().Enqueue(snakeNewHead);
                Console.SetCursorPosition(snakeNewHead.col, snakeNewHead.row);
                Console.ForegroundColor = ConsoleColor.Gray;
                string head = snake.SnakeHead();
                Console.Write(head);


                if (snakeNewHead.col == fd.col && snakeNewHead.row == fd.row)
                {
                    System.Media.SoundPlayer player2 = new System.Media.SoundPlayer();
                    player2.SoundLocation = "../../music/soundeffect.wav";
                    player2.Play();

                    userPoints += 100;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.SetCursorPosition(0, 0);
                    Console.Write("Score :" + (userPoints).ToString());
                    
                    // feeding the snake
                    while (snake.GetSnakeElements().Contains(fd) || obstacle.GetPositions().Contains(fd))
                    {
                        x = Math.Abs(randomNumbersGenerator2.Next(0, 30) - randomNumbersGenerator.Next(0, 30));
                        y = Math.Abs(randomNumbersGenerator2.Next(0, 120) - randomNumbersGenerator.Next(0, 120));
                        fd = food.Generate(x, y);
                    }
                    lastFoodTime = Environment.TickCount;
                    Console.SetCursorPosition(fd.col, fd.row);
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write("@");
                    sleepTime--;
                    

                    //Position obstacles = new Position();
                    Position ob = new Position();
                    while (snake.GetSnakeElements().Contains(ob) ||
                        obstacle.GetPositions().Contains(ob) ||
                        (fd.row != ob.row && fd.col != ob.row))
                    {
                        x = randomNumbersGenerator.Next(0, 30) - 1;
                        y = randomNumbersGenerator.Next(0, 120) - -1;
                        ob = obstacle.Generate(x,y);
                    }
                    
                    obstacle.GetPositions().Add(ob);
                    Console.SetCursorPosition(ob.col, ob.row);
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write("=");
                }
                else
                {
                    // moving...
                    Position last = snake.Moving();
                    Console.SetCursorPosition(last.col, last.row);
                    Console.Write(" ");
                }

                if (Environment.TickCount - lastFoodTime >= foodDissapearTime)
                {
                    negativePoints = negativePoints + 50;
                    Console.SetCursorPosition(fd.col, fd.row);
                    Console.Write(" ");
                    while (snake.GetSnakeElements().Contains(fd) || obstacle.GetPositions().Contains(fd))
                    {
                        x = Math.Abs(randomNumbersGenerator2.Next(0, 30) - randomNumbersGenerator.Next(0, 30));
                        y = Math.Abs(randomNumbersGenerator2.Next(0, 120) - randomNumbersGenerator.Next(0, 120));
                        fd = food.Generate(x, y);
                    }
                   
                    lastFoodTime = Environment.TickCount;
                }

                Console.SetCursorPosition(fd.col, fd.row);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("@");
                Console.ForegroundColor = ConsoleColor.Black; //hide text if clicked others button
                Console.CursorVisible = false;

                sleepTime -= 0.01;

                Thread.Sleep((int)sleepTime);

            }
        }
    }
}
