using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeConsole
{
    public interface IDestroyable
    {
        void Destroy();

        bool IsDestroyed { get; set; }
    }
}
