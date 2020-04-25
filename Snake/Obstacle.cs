using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    class Obstacle
    {
        private List<Position> obstacles;
        public Obstacle()
        {
            Random randomNumbersGenerator = new Random();
            obstacles = new List<Position>()
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
        }

        public Position Generate(int x, int y)
        {
            Position ob = new Position(x,y);

            return ob;
        }

        public List<Position> GetPositions()
        {
            return obstacles;
        }

        public void Add(Position p)
        {
            obstacles.Add(p);
        }

    }
}
