using System;

namespace LP2_P1
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WindowHeight = 60;
            Console.WindowWidth = 200;
            Console.Title = "MyIMDBSearcher";

            Console.SetWindowPosition(0, 0);

            StartMenu.MenuLoop();
        }
    }
}