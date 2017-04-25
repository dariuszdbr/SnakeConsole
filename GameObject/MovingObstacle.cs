using SnakeConsole.Direction;
using SnakeConsole.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeConsole
{
    public class MovingObstacle : GameObject, IMovable ,ICollisionDetectable
    {
        
        private MoveDirection movingObstacleDirection;
        
        public MovingObstacle(Position pos)
            : base(pos)
        {
            this.Direction = movingObstacleDirection;
        }

        public MoveDirection Direction 
        {
            get { return movingObstacleDirection; }
            set { movingObstacleDirection = value; } 
        }
    
        public void ChangeDirection(MoveDirection newDirection)
        {
                this.Direction = newDirection;
        }
        
        public void ChangeDirection() // generate random direction
        {
            var generate = new Random();
            var random = generate.Next(0, 4);
            this.Direction = (MoveDirection)random;
        }


        public void Move()
        {
            this.ClearPoint();
            this.Position.Add(Constants.chooseNewPosition[(int)movingObstacleDirection]);
        }

        public void ClearPoint() //from the board
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.SetCursorPosition(this.Position.X, this.Position.Y);
            Console.Write(" ");
        }

        public void Draw()
        {
            Console.ForegroundColor = ConsoleColor.Red;  
            char obstacle = (char)176;
            Console.SetCursorPosition(this.Position.X, this.Position.Y);
            Console.Write(obstacle);
            Console.BackgroundColor = ConsoleColor.Black;
        }
      
    }
}
