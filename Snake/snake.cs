using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    class Snake
    {
        private Direction direction;
        private Queue<Position> snakeElements;
        public Snake()
        {
            direction = Direction.right;
            snakeElements = new Queue<Position>();
            for (int i = 0; i <=3; i++)
            {
                snakeElements.Enqueue(new Position(2, i));
            }
        }

        public Position[] Directions()
        {
            Position[] directions = new Position[]
            {
                new Position(0, 1), // right
                new Position(0, -1), // left
                new Position(1, 0), // down
                new Position(-1, 0), // up
            };

            return directions;
        }

        public Position Move_Snake(Position snakeHead)
        {
            
            Position[] directions = Directions();
            Position nextDirection = directions[(int)direction];
            Position snakeNewHead = new Position(snakeHead.row + nextDirection.row,
                    snakeHead.col + nextDirection.col);

            if(snakeNewHead.col < 0)
            {
                snakeNewHead.col = Console.WindowWidth - 1;
            }

            if (snakeNewHead.row < 2)
            {
                snakeNewHead.row = Console.WindowHeight - 1;
            }

            if (snakeNewHead.row >= Console.WindowHeight)
            {
                snakeNewHead.row = 2;
            }

            if (snakeNewHead.col >= Console.WindowWidth)
            {
                snakeNewHead.col = 0;
            }

            return snakeNewHead;

        }

        public void updateSnake(Position p)
        {
            snakeElements.Enqueue(p);
        }

        public string SnakeHead()
        {
            if(direction == Direction.right)
            {
                return ">";
            }else if(direction == Direction.left)
            {
                return "<";
            }else if(direction == Direction.up)
            {
                return "^";
            }
            else
            {
                return "v";
            }
        }

        public void SnakeBody(int color)
        {
            foreach (Position position in snakeElements)
            {
                Console.SetCursorPosition(position.col, position.row);
                Console.Write("*");
            }
        }

        public Position Moving()
        {
            Position last = snakeElements.Dequeue();
            return last;
        }

        public Queue<Position> GetSnakeElements()
        {
            return snakeElements;
        }

        public void Clear()
        {
            snakeElements.Clear();
        }

        public void Reset()
        {
            direction = Direction.right;
            snakeElements = new Queue<Position>();
            for (int i = 0; i <= 3; i++)
            {
                snakeElements.Enqueue(new Position(0, i));
            }
        }
        public Direction GetDirection()
        {
            return direction;
        }

        public void SetDirection(Direction x)
        {
            direction = x;
        }









    }
}
