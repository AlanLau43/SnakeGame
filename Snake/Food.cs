using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    class Food
    {
        private Position food;
        private Position extra;
        public Food()
        {
            Random randomNumbersGenerator = new Random();
            food = new Position(randomNumbersGenerator.Next(0, 30),
                    randomNumbersGenerator.Next(0, 120));

            extra = new Position(randomNumbersGenerator.Next(0, 30),
                    randomNumbersGenerator.Next(0, 120));

        }

        public Position Generate(int x, int y)
        {
            Position g = new Position(x,y);
            return g;
        }

        public Position GetFood()
        {
            return food;
        }

        public Position GetExFood()
        {
            return extra;
        }
    }
}
