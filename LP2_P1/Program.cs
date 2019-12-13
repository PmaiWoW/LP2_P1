using System;
using System.Linq;
using System.Collections.Generic;

namespace LP2_P1
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WindowHeight = 60;
            Console.WindowWidth = 200;
            Console.CursorVisible = false;
            Console.Title = "MyIMDBSearcher";
            
            Console.SetWindowPosition(0, 0);
            
            StartMenu.MenuLoop();
            
            string wantedTitle = Console.ReadLine();
            Console.ResetColor();
        }
        public static void ClearSpace()
        {
            Console.CursorLeft -= 2;
            Console.Write(" ");
        }
    }
}