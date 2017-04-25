using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeConsole
{
   public static class Menu
    {
       /// <summary>
        /// C o n s o l e  S n a k e  G a m e, 1. Enter - Start a new game, 2. Esc - Exit game
       /// </summary>
        public static void StartMenu()
        {
            Console.OutputEncoding = System.Text.Encoding.GetEncoding(1252);
            Console.Title = "Snake Game by D. D.";
            Console.SetBufferSize(Constants.width, Constants.height + 2); // deleting scrollbar, depends on the user resolution, if there are some trouble with that just comment
            Console.SetWindowSize(Constants.width, Constants.height + 2);
            

            string title = ">>> C o n s o l e  S n a k e  G a m e <<<";
            string title2 = "1. Enter - Start a new game";
            string title3 = "2. Esc - Exit game";
            
            while(true)
            {
                ConsoleColor Background = ConsoleColor.Black;
                //Console.Clear();
                Console.CursorVisible = false;
                Console.BackgroundColor = ConsoleColor.White;
                Console.Clear();
                // ">>> C o n s o l e  S n a k e  G a m e <<<";
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.SetCursorPosition((Console.WindowWidth - title.Length) / 2, Console.WindowHeight/2 - 3);
                Console.Write(title);
                // "1. Enter - Start a new game";
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.SetCursorPosition((Console.WindowWidth - title2.Length) / 2, Console.WindowHeight / 2 + 3);
                Console.Write(title2);
                // "2. Esc - Exit game";
                Console.SetCursorPosition((Console.WindowWidth - title3.Length) / 2, Console.WindowHeight / 2 + 6);
                Console.Write(title3);

                Console.BackgroundColor = Background;

                ConsoleKeyInfo choose = Console.ReadKey();
                switch(choose.Key)
                {
                    case ConsoleKey.Enter:
                    case ConsoleKey.D1:
                        Console.Clear();
                        GameEngine Game = new GameEngine();
                        break;
                    case ConsoleKey.Escape:
                    case ConsoleKey.D2:
                        Console.Clear();
                        return;
                    default:
                        break;
                }


            }
        }
    }
}
