using SnakeConsole.Direction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeConsole
{
    public interface IMovable
    {
       
        void Move();

        void ChangeDirection(MoveDirection newDirection);
    }
}
