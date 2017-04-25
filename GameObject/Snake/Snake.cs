using SnakeConsole.Direction;
using SnakeConsole.Interfaces;
using System;
using System.Collections.Generic;

namespace SnakeConsole
{
    public class Snake : GameObject, IMovable, ICollisionDetectable, IDestroyable
    {
        #region Fields
 
        public MoveDirection snakeDirection;
        public LinkedList<SnakePart> Parts { get; set; }

         #endregion

        #region Contructor
        
        public Snake(Position pos,int size)
            : base(pos,size)
            {  
                this.IsDestroyed = false;    
                Parts = new LinkedList<SnakePart>();

            for(int i = 0; i < size; i++)
            {
                Parts.AddLast(new SnakePart(new Position(Constants.width /2 - i , Constants.height/2)));
            } 
            DrawSnake();
            snakeDirection = MoveDirection.right;
           }

        #endregion

        #region Methods

        public void DrawSnake()
        {
            foreach (var snakePart  in Parts)
            {
                DrawPoint(snakePart.Position);
            }
            DrawHead();
        }

        
        public void DrawHead()
        {
            Console.BackgroundColor = ConsoleColor.DarkRed;
            Console.SetCursorPosition(Parts.First.Value.Position.X, Parts.First.Value.Position.Y);
            Console.Write(" ");  
        }
 
        public void ClearPoint() //From the board
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.SetCursorPosition(Parts.Last.Value.Position.X, Parts.Last.Value.Position.Y);
            Console.Write(" ");
        }

        public void DrawPoint(Position pos) // In the same colour like snake
        {
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.SetCursorPosition(pos.X, pos.Y);
            Console.Write(" ");
        }

        public void Move()
        {
            ClearPoint(); // at the end of linkedList Parts
            Parts.RemoveLast(); // remove last part from linkedList
            DrawPoint(Parts.First.Value.Position); // In the same colour like snake instead of red Head
            var copyPosition = Parts.First.Value.Position.Copy();
            var newHead = new SnakePart(copyPosition.Add(Constants.chooseNewPosition[(int)snakeDirection]));
            Parts.AddFirst(newHead);
            DrawHead();  
        }
        
        public void Grow()
        {   var direction = Parts.Last.Value.Direction;
            var oppositePosition = Constants.chooseNewPosition[(int)direction];
            var tail = Parts.Last.Value.Position.Copy();
            SnakePart newTail = new SnakePart(tail.Add(tail.Add(oppositePosition, -1)));
            Parts.AddLast(newTail);
        }  
 
        public void ChangeDirection(MoveDirection newDirection)
        {
            this.snakeDirection = newDirection;
        }
        
        public MoveDirection Direction 
        {
            get { return snakeDirection; }
            set { ChangeDirection(value); }
        }

        public void Destroy()
        {
            this.IsDestroyed = true;
        }

        public bool IsDestroyed { get; set; }

        public bool isCollidedWith(Position ObjPos)
        {
            foreach (var snakePart in this.Parts)
            {
                if (snakePart.Position.X == ObjPos.X && snakePart.Position.Y == ObjPos.Y)
                {
                    return true;
                }
            }
            return false;
        }

        #endregion
    }
}