using SnakeConsole.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SnakeConsole
{
    public class Position
    {
        private int _x;
        private int _y;

        public int X { get { return _x; } set { _x = value; } }
        public int Y { get { return _y; } set { _y = value; } }

        public Position()
        {
            X = Y = 0;
        }
  
        public Position(int x, int y)
        {
            this.X = x;
           this.Y = y;
        }

        public Position Add(Position obj)
        {
             this.X += obj.X;
             this.Y += obj.Y;
            return new Position(X,Y);
        }

        public Position Add(Position pos , int x)
        {
            return new Position(pos.X * x , pos.Y * x);
        }

        public Position Copy()
        {
            return new Position(X, Y);
        }

        /// <summary>
        /// Check wheter the Positions are equal
        /// </summary>
        /// <param name="obj">Object type Position</param>
        /// <returns> true or false </returns>
        public override bool Equals(object obj) // Przesłonięcie funkcji z klasy bazowej "Object" sprawdzające czy podane współrzędne są sobie równe;
        {
            if (!(obj is Position))
            {
                return false;
            }

            Position other = (Position)obj; // rzutowanie jawne object obj na typ Positon obj.

            return this.X == other.X && this.Y == other.Y; // zwraca wartość true gdy są równe i false gdy nie
        }

         public bool Equals(int x, int y) 
        {
            Position other = new Position(x, y); 
            return this.X == other.X && this.Y == other.Y;
        }


         
    }
}
