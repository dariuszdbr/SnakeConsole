using SnakeConsole.Direction;
using SnakeConsole.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeConsole
{
    public class MovingFood : Food, IMovable, ICollisionDetectable
    {
        private MoveDirection movingFoodDirection;

        public MovingFood(Position foodPosition)
            :base(foodPosition)
            {
            this.Direction = movingFoodDirection;
            }

        public void ClearPoint()
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.SetCursorPosition(this.Position.X, this.Position.Y);
            Console.Write(" ");
        }

        public override MoveDirection Direction
        {
            get { return movingFoodDirection; }
            set { ChangeDirection( value ); }
        }

        public void ChangeDirection(MoveDirection newDirection)
        {
            this.movingFoodDirection = newDirection;
        }

        public void ChangeDirection()
        {
            var generate = new Random();
            var random = generate.Next(0, 4);
            this.Direction = (MoveDirection)random;
        }

        public void Move()
        {
                this.ClearPoint();
                this.Position.Add(Constants.chooseNewPosition[(int)movingFoodDirection]);
        }

        public override void Draw()
        {
            char food = (char)177;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.SetCursorPosition(this.Position.X, this.Position.Y);
            Console.Write(food);
        }
    }
}
