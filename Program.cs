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
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            bool events = false;
            int countdown = 9;
            int goal;
            System.Media.SoundPlayer player = new System.Media.SoundPlayer();
            player.SoundLocation = "../../music/backgroundmusic.wav";
            player.Play();

            Menu menu = new Menu();
            menu.DrawMenu();
            Console.CursorVisible = false;
            ConsoleKeyInfo userinput = Console.ReadKey();
            while(userinput.Key != ConsoleKey.Enter)
            {
                menu.SelectDiff(userinput);
                menu.DrawMenu();
                
                userinput = Console.ReadKey();
            }
            Console.Clear();
            Console.SetWindowSize(120, 30);

            if (menu.GetDiff() == 0)
            {
                goal = 1000;
            }else if(menu.GetDiff() == 1)
            {
                goal = 2000;
            }
            else
            {
                goal = 4000;
            }
            
            //Create a snake object
            Snake snake = new Snake();

            //Create obstacles object
            Obstacle obstacle = new Obstacle();

            //Create a Food Object 
            Food food = new Food();

            //Extra Food
            int z = 1;
            Position extra = food.GetExFood();
            bool spawn = false;

            int lastFoodTime = 0;

            int foodDissapearTime = 14000; //extended the time
            if(menu.GetDiff() == 1){
                foodDissapearTime = 10000;
            }
            else if (menu.GetDiff() == 2){
                foodDissapearTime = 8000;
            }

            int negativePoints = 0;
            int userPoints = 0;

            double sleepTime = 100;
            Random randomNumbersGenerator = new Random();
            Random randomNumbersGenerator2 = new Random();
            int x;
            int y;

            int SnakeColor;
            //Snake Initial Colour
            SnakeColor = randomNumbersGenerator.Next(0, 5);
            Console.BufferHeight = Console.WindowHeight;
            lastFoodTime = Environment.TickCount;

            foreach (Position ob in obstacle.GetPositions())
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.SetCursorPosition(ob.col, ob.row);
                Console.Write("\u2592");
            }

            x = randomNumbersGenerator.Next(2, 30);
            y = randomNumbersGenerator.Next(0, 120);

            Position fd = food.GetFood();
            Position fd1 = food.Generate(x, y + 1);
            while (snake.GetSnakeElements().Contains(fd) || obstacle.GetPositions().Contains(fd) || snake.GetSnakeElements().Contains(fd1) || obstacle.GetPositions().Contains(fd1))
            {
                x = randomNumbersGenerator.Next(2, 30);
                y = randomNumbersGenerator.Next(0, 120);
                fd = food.Generate(x, y);
                fd1 = food.Generate(x, y+1);
            }
            
            Console.SetCursorPosition(fd.col, fd.row);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("\u2665\u2665");

            snake.SnakeBody();

            
            while (true)
            {
                Position snakeHead;
                Position snakeNewHead;
                Console.SetCursorPosition(0, 0);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Score: " + (userPoints).ToString());
                Console.WriteLine("Goal: " + goal);
                Console.ForegroundColor = ConsoleColor.Black;
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo userInput = Console.ReadKey();
                    if (events) {
                        if (userInput.Key == ConsoleKey.LeftArrow)
                        {
                            if (snake.GetDirection() != Direction.left)
                            {
                               snake.SetDirection(Direction.right);
                            }
                        }
                        else if (userInput.Key == ConsoleKey.RightArrow)
                        {
                            if (snake.GetDirection() != Direction.right)
                            {
                                snake.SetDirection(Direction.left);
                            }
                        }
                        else if (userInput.Key == ConsoleKey.UpArrow)
                        {
                            if (snake.GetDirection() != Direction.up)
                            {
                                snake.SetDirection(Direction.down);
                            }
                        }
                        else if (userInput.Key == ConsoleKey.DownArrow)
                        {
                            if (snake.GetDirection() != Direction.down)
                            {
                                snake.SetDirection(Direction.up);
                            }
                        }
                    }
                    else
                    {
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
                    if(userInput.Key == ConsoleKey.R)
                    {
                        Console.Clear();
                        menu.DrawMenu();
                        Console.CursorVisible = false;
                        userinput = Console.ReadKey();
                        while (userinput.Key != ConsoleKey.Enter)
                        {
                            menu.SelectDiff(userinput);
                            menu.DrawMenu();

                            userinput = Console.ReadKey();

                        }
                        Console.Clear();
                        if (menu.GetDiff() == 0)
                        {
                            goal = 1000;
                        }
                        else if (menu.GetDiff() == 1)
                        {
                            goal = 2000;
                        }
                        else
                        {
                            goal = 4000;
                        }
                        snake = new Snake();

                        //Create obstacles object
                        obstacle = new Obstacle();

                        //Create a Food Object 
                        food = new Food();

                        //Extra Food
                        z = 1;
                        extra = food.GetExFood();
                        spawn = false;

                        foodDissapearTime = 14000; //extended the time
                        if (menu.GetDiff() == 1)
                        {
                            foodDissapearTime = 10000;
                        }
                        else if (menu.GetDiff() == 2)
                        {
                            foodDissapearTime = 8000;
                        }

                        negativePoints = 0;
                        userPoints = 0;

                        sleepTime = 100;
                        randomNumbersGenerator = new Random();
                        randomNumbersGenerator2 = new Random();

                        Console.BufferHeight = Console.WindowHeight;
                        lastFoodTime = Environment.TickCount;

                        foreach (Position ob in obstacle.GetPositions())
                        {
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.SetCursorPosition(ob.col, ob.row);
                            Console.Write("\u2592");
                        }

                        x = randomNumbersGenerator.Next(2, 30);
                        y = randomNumbersGenerator.Next(0, 120);

                        fd = food.GetFood();
                        fd1 = food.Generate(x, y + 1);
                        while (snake.GetSnakeElements().Contains(fd) || obstacle.GetPositions().Contains(fd) || snake.GetSnakeElements().Contains(fd1) || obstacle.GetPositions().Contains(fd1))
                        {
                            x = randomNumbersGenerator.Next(2, 30);
                            y = randomNumbersGenerator.Next(0, 120);
                            fd = food.Generate(x, y);
                            fd1 = food.Generate(x, y+1);
                        }

                        Console.SetCursorPosition(fd.col, fd.row);
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write("\u2665\u2665");

                        snake.SnakeBody();

                        snakeHead = snake.GetSnakeElements().Last();
                        snakeNewHead = snake.Move_Snake(snakeHead);
                    }
                   
                   }
                
                snakeHead = snake.GetSnakeElements().Last();
                snakeNewHead = snake.Move_Snake(snakeHead);
                //Winning Requirement
                if (userPoints == goal)
                {
                    Console.SetCursorPosition(Console.WindowWidth - 30, 0);
                    Console.Write(new string(' ', 30));
                    string path = "../../Score.txt";
                    Console.SetCursorPosition((Console.WindowWidth / 2) - 14, (Console.WindowHeight / 2) - 2);
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Successfull Clear Stage!!!");
                    Console.ForegroundColor = ConsoleColor.Red;
                    string exit = "\t\t\t\t\t\tPress Enter to Exit";
                    Console.WriteLine(exit);
                    Console.ForegroundColor = ConsoleColor.Black;
                    if (!File.Exists(path))
                    {
                        // Create a file to write to.
                        Console.WriteLine("Can't Open the File, Please try again");
                    }
                    else
                    {
                        using (StreamWriter sw = File.AppendText(path))
                        {
                            sw.WriteLine("Clear Stage");
                        }
                    }
                    ConsoleKeyInfo userInput = Console.ReadKey();
                    while (true)
                    {
                        if (userInput.Key == ConsoleKey.R)
                        {
                            Console.Clear();
                            menu.DrawMenu();
                            Console.CursorVisible = false;
                            userinput = Console.ReadKey();
                            while (userinput.Key != ConsoleKey.Enter)
                            {
                                menu.SelectDiff(userinput);
                                menu.DrawMenu();

                                userinput = Console.ReadKey();

                            }
                            Console.Clear();
                            if (menu.GetDiff() == 0)
                            {
                                goal = 1000;
                            }
                            else if (menu.GetDiff() == 1)
                            {
                                goal = 2000;
                            }
                            else
                            {
                                goal = 4000;
                            }
                            snake = new Snake();

                            //Create obstacles object
                            obstacle = new Obstacle();

                            //Create a Food Object 
                            food = new Food();

                            //Extra Food
                            z = 1;
                            extra = food.GetExFood();
                            spawn = false;

                            foodDissapearTime = 14000; //extended the time
                            if (menu.GetDiff() == 1)
                            {
                                foodDissapearTime = 10000;
                            }
                            else if (menu.GetDiff() == 2)
                            {
                                foodDissapearTime = 8000;
                            }

                            negativePoints = 0;
                            userPoints = 0;

                            sleepTime = 100;
                            randomNumbersGenerator = new Random();

                            Console.BufferHeight = Console.WindowHeight;
                            lastFoodTime = Environment.TickCount;

                            foreach (Position ob in obstacle.GetPositions())
                            {
                                Console.ForegroundColor = ConsoleColor.Cyan;
                                Console.SetCursorPosition(ob.col, ob.row);
                                Console.Write("=");
                            }

                            fd = food.GetFood();
                            fd1 = food.Generate(x, y + 1);
                            while (snake.GetSnakeElements().Contains(fd) || obstacle.GetPositions().Contains(fd) || snake.GetSnakeElements().Contains(fd1) || obstacle.GetPositions().Contains(fd1))
                            {
                                x = randomNumbersGenerator.Next(2, 30);
                                y = randomNumbersGenerator.Next(0, 120);
                                fd = food.Generate(x, y);
                                fd1 = food.Generate(x, y+1);
                            }

                            Console.SetCursorPosition(fd.col, fd.row);
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.Write("\u2665\u2665");

                            snake.SnakeBody();
                            snakeHead = snake.GetSnakeElements().Last();
                            snakeNewHead = snake.Move_Snake(snakeHead);

                            break;
                        }

                        if (userInput.Key == ConsoleKey.Enter)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            System.Environment.Exit(1);
                        }

                        userInput = Console.ReadKey();

                    }

                }

                if (snake.GetSnakeElements().Contains(snakeNewHead) || obstacle.GetPositions().Contains(snakeNewHead))
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
                        Console.WriteLine("Can't Open the File, Please try again");
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
                        if (userInput.Key == ConsoleKey.R)
                        {
                            Console.Clear();
                            menu.DrawMenu();
                            Console.CursorVisible = false;
                            userinput = Console.ReadKey();
                            while (userinput.Key != ConsoleKey.Enter)
                            {
                                menu.SelectDiff(userinput);
                                menu.DrawMenu();

                                userinput = Console.ReadKey();

                            }
                            Console.Clear();
                            if (menu.GetDiff() == 0)
                            {
                                goal = 1000;
                            }
                            else if (menu.GetDiff() == 1)
                            {
                                goal = 2000;
                            }
                            else
                            {
                                goal = 4000;
                            }

                            snake = new Snake();
                            //Create obstacles object
                            obstacle = new Obstacle();

                            //Create a Food Object 
                            food = new Food();

                            //Extra Food
                            z = 1;
                            extra = food.GetExFood();
                            spawn = false;

                            foodDissapearTime = 14000; //extended the time
                            if (menu.GetDiff() == 1)
                            {
                                foodDissapearTime = 10000;
                            }
                            else if (menu.GetDiff() == 2)
                            {
                                foodDissapearTime = 8000;
                            }

                            negativePoints = 0;
                            userPoints = 0;

                            sleepTime = 100;
                            randomNumbersGenerator = new Random();

                            Console.BufferHeight = Console.WindowHeight;
                            lastFoodTime = Environment.TickCount;

                            foreach (Position ob in obstacle.GetPositions())
                            {
                                Console.ForegroundColor = ConsoleColor.Cyan;
                                Console.SetCursorPosition(ob.col, ob.row);
                                Console.Write("\u2592");
                            }


                            fd = food.GetFood();
                            fd1 = food.Generate(x, y + 1);
                            while (snake.GetSnakeElements().Contains(fd) || obstacle.GetPositions().Contains(fd) || snake.GetSnakeElements().Contains(fd1) || obstacle.GetPositions().Contains(fd1))
                            {
                                x = randomNumbersGenerator.Next(0, 30);
                                y = randomNumbersGenerator.Next(0, 120);
                                fd = food.Generate(x, y);
                                fd1 = food.Generate(x , y+1);
                            }

                            Console.SetCursorPosition(fd.col, fd.row);
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.Write("\u2665\u2665");

                            snake.SnakeBody();
                            snakeHead = snake.GetSnakeElements().Last();
                            snakeNewHead = snake.Move_Snake(snakeHead);
                            break;
                        }
                        if (userInput.Key == ConsoleKey.Enter)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            System.Environment.Exit(1);
                        }
                        
                        userInput = Console.ReadKey();
                        
                    }
                    
                }

                Console.SetCursorPosition(snakeHead.col, snakeHead.row);
                if(SnakeColor == 0)
                {
                    Console.ForegroundColor = ConsoleColor.DarkBlue;
                }else if(SnakeColor == 1)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                }
                else if (SnakeColor == 2)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                }
                else if (SnakeColor == 3)
                {
                    Console.ForegroundColor = ConsoleColor.Magenta;
                }
                else if (SnakeColor == 4)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                }
                else if (SnakeColor == 5)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                }

                Console.Write("*");

                snake.GetSnakeElements().Enqueue(snakeNewHead);
                Console.SetCursorPosition(snakeNewHead.col, snakeNewHead.row);
                Console.ForegroundColor = ConsoleColor.Gray;
                string head = snake.SnakeHead();
                Console.Write(head);


                // feeding the snake
                if ((snakeNewHead.col == fd.col && snakeNewHead.row == fd.row) || (snakeNewHead.col == fd1.col && snakeNewHead.row == fd1.row))
                {
                    System.Media.SoundPlayer player2 = new System.Media.SoundPlayer();
                    player2.SoundLocation = "../../music/soundeffect.wav";
                    player2.Play();
                    Console.SetCursorPosition(fd.col, fd.row);
                    Console.Write("  ");
                    SnakeColor = randomNumbersGenerator.Next(0, 5);
                    userPoints += 100;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.SetCursorPosition(0, 0);
                    Console.Write("Score :" + (userPoints).ToString());

                    
                    while (snake.GetSnakeElements().Contains(fd) || obstacle.GetPositions().Contains(fd) || snake.GetSnakeElements().Contains(fd1) || obstacle.GetPositions().Contains(fd1))
                    {
                        x = randomNumbersGenerator.Next(2, 30);
                        y = randomNumbersGenerator.Next(0, 120);
                        fd = food.Generate(x, y);
                        fd1 = food.Generate(x , y + 1);
                    }
                    z = randomNumbersGenerator.Next(0, 1);
                    lastFoodTime = Environment.TickCount;
                    Console.SetCursorPosition(fd.col, fd.row);
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write("\u2665\u2665");
                    if (menu.GetDiff() == 0)
                    {
                        sleepTime -= 0.01;
                    }
                    else if (menu.GetDiff() == 1)
                    {
                        sleepTime -= 0.02;
                    }
                    else
                    {
                        sleepTime -= 0.04;
                    }


                    //Position obstacles = new Position();
                    Position ob = new Position();
                    while (snake.GetSnakeElements().Contains(ob) ||
                        obstacle.GetPositions().Contains(ob) ||
                        (fd.row != ob.row && fd.col != ob.row))
                    {
                        x = randomNumbersGenerator.Next(2, Console.WindowHeight);
                        y = randomNumbersGenerator.Next(0, Console.WindowWidth);
                        ob = obstacle.Generate(x,y);
                    }

                    obstacle.GetPositions().Add(ob);
                    Console.SetCursorPosition(ob.col, ob.row);
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write("\u2592");

                    if (menu.GetDiff() == 2)
                    {
                        while (snake.GetSnakeElements().Contains(ob) ||
                        obstacle.GetPositions().Contains(ob) ||
                        (fd.row != ob.row && fd.col != ob.row))
                        {
                            x = randomNumbersGenerator.Next(2, Console.WindowHeight);
                            y = randomNumbersGenerator.Next(0, Console.WindowWidth);
                            ob = obstacle.Generate(x, y);
                        }

                        obstacle.GetPositions().Add(ob);
                        Console.SetCursorPosition(ob.col, ob.row);
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write("\u2592");
                    }
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
                    Console.SetCursorPosition(fd.col, fd.row);

                    do
                    {
                        x = randomNumbersGenerator.Next(2, Console.WindowHeight);
                        y = randomNumbersGenerator.Next(0, Console.WindowWidth);
                        fd = food.Generate(x, y);
                        fd1 = food.Generate(x, y + 1);
                        Console.WriteLine("  ");
                    }
                    while (snake.GetSnakeElements().Contains(fd) || obstacle.GetPositions().Contains(fd) || snake.GetSnakeElements().Contains(fd1) || obstacle.GetPositions().Contains(fd1));
                    lastFoodTime = Environment.TickCount;

                    lastFoodTime = Environment.TickCount;
                    Console.SetCursorPosition(fd.col, fd.row);
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write("\u2665\u2665");
                }

                if (menu.GetDiff() == 2)
                {
                    if (z == 0)
                    {
                        do
                        {
                            x = randomNumbersGenerator.Next(2, Console.WindowHeight);
                            y = randomNumbersGenerator.Next(0, Console.WindowWidth);
                            extra = food.Generate(x, y);

                        }
                        while (snake.GetSnakeElements().Contains(extra) || obstacle.GetPositions().Contains(extra));
                        Console.SetCursorPosition(extra.col, extra.row);
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write("Q");

                        z += 1;
                        spawn = true;
                    }

                    if (spawn)
                    {
                        if (Environment.TickCount - lastFoodTime >= 4000)
                        {
                            Console.SetCursorPosition(extra.col, extra.row);
                            Console.WriteLine(" ");
                            lastFoodTime = Environment.TickCount;
                            spawn = false;
                        }
                        else if (snake.GetSnakeElements().Contains(extra))
                        {
                            userPoints += 1000;
                            Console.SetCursorPosition(extra.col, extra.row);
                            Console.WriteLine(" ");
                            spawn = false;
                        }
                    }
                }
                if (userPoints > 0 && userPoints % 500 == 0 && events == false)
                {
                    events = true;
                    countdown = 9;
                }

                if (events)
                {
                    Console.SetCursorPosition(Console.WindowWidth-30, 0);
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("Upside Down Event!!!: ");
                    Console.Write(countdown);
                    if(Environment.TickCount - lastFoodTime >= 1000)
                    {
                        lastFoodTime = Environment.TickCount;
                        countdown -= 1;
                    }
                    if (countdown == 0)
                    {
                        Console.SetCursorPosition(Console.WindowWidth - 30, 0);
                        Console.Write(new string(' ',30));
                        events = false;
                    }

                }
                Console.ForegroundColor = ConsoleColor.Black; //hide text if clicked others button
                Console.CursorVisible = false;

                Thread.Sleep((int)sleepTime);

            }
        }
    }
}
