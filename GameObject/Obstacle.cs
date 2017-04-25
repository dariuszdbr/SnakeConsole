using SnakeConsole.Direction;
using SnakeConsole.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeConsole
{

    public class Obstacle : GameObject
    {
        Random rand = new Random();
        public List<Position> obstacleBigPosition = new List<Position>();

        public Obstacle(Position pos)
            : base(pos) { }

        public Obstacle(Position pos, int size)
            : base(pos) 
        {
            this.Size = size;
            GenerateObstacle(pos);
          
        }

        public virtual void Draw()
        {       
            foreach (var point in obstacleBigPosition)
            {
                char obstacle = ' '; //(char)176;
                Console.BackgroundColor = ConsoleColor.DarkCyan;
                Console.SetCursorPosition(point.X, point.Y);
                Console.Write(obstacle);  
            }
            Console.BackgroundColor = ConsoleColor.Black;
        }

        public List<Position> GenerateObstacle(Position pos)
            {
                obstacleBigPosition.Clear();
                var currentPosition = pos.Copy();

                obstacleBigPosition.Add(currentPosition);

                for (int i = 0; i < Constants.chooseNewPosition.Count; i++ )
                    if (possible(pos, i))
                    {
                        var possiblePosition = pos.Copy().Add(Constants.chooseNewPosition[i]);
                        obstacleBigPosition.Add(possiblePosition);
                    }
                return obstacleBigPosition;
            }
        
        public bool possible(Position pos, int listIndex)
        {
            var possiblePosition = pos.Copy().Add(Constants.chooseNewPosition[listIndex]);

            if (possiblePosition.X > 2 && possiblePosition.X < Constants.width - 3  &&
                possiblePosition.Y > 2 && possiblePosition.Y < Constants.height - 3)
            {
                return true;
            }

            return false;
        }
    
    }
}
