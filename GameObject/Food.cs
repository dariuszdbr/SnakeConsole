using SnakeConsole.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeConsole
{
    public class Food : GameObject, ICollisionDetectable
    {
        public Food(Position pos)
            : base(pos) { }

        public virtual void Draw()
        {
            char food = (char)177;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.SetCursorPosition(this.Position.X, this.Position.Y);
            Console.Write(food);
        }


         public virtual Direction.MoveDirection Direction
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }
    }
}
