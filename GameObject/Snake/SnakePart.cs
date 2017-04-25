using SnakeConsole.Direction;

namespace SnakeConsole
{
   public class SnakePart : GameObject
    {
        public SnakePart(Position pos)
            :base(pos) { }

        public MoveDirection Direction {get;  set; }   
    }
}
