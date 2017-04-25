using SnakeConsole.Direction;

namespace SnakeConsole.Interfaces
{
    public interface ICollisionDetectable
    {
        Position Position { get; set; }
        MoveDirection Direction { get; set; }
    }
}
