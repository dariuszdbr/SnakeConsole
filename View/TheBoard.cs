using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SnakeConsole
{
   public static class TheBoard
   {

       public static void DrawBoard()
       {
           Console.BackgroundColor = ConsoleColor.DarkCyan;
           string space = String.Empty;
           Console.SetCursorPosition(0, 0);
           Console.Write(space.PadLeft(Constants.width)); //górna krawędź
           Console.SetCursorPosition(0, Constants.height);
           Console.Write(space.PadLeft(Constants.width)); //dolna krawędź

           for (int i = 1; i <= Constants.height; i++)
           {
               Console.SetCursorPosition(0, i);     // lewa krawędź
               Console.Write(" ");
               Console.SetCursorPosition(Constants.width - 1, i); // prawa krawędź
               Console.Write(" ");
           }
           WritePoints();
           WriteLengthOfSnake();
           Console.SetWindowPosition(0, 0);
       } 


        public static void WritePoints()
        {
            Console.BackgroundColor = ConsoleColor.DarkCyan;
            Console.SetCursorPosition(1, Constants.height);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("Score = {0}", GameEngine.score);
            Console.BackgroundColor = ConsoleColor.Black;
        }

        public static void WriteLengthOfSnake(int x = 5)
        {
            Console.BackgroundColor = ConsoleColor.DarkCyan;
            Console.SetCursorPosition(15, Constants.height);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("Snake length =     "); // Cleaning
            Console.SetCursorPosition(15, Constants.height);
            Console.Write("Snake length = {0}", x);
            Console.BackgroundColor = ConsoleColor.Black;
        }

    
    }
}
