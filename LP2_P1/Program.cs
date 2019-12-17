using System;

namespace LP2_P1
{
    public class Program
    {    
        // Holds the desired height and width of the console
        public const int WindowWidth = 200;
        public const int WindowHeight = 60;

        private static void Main(string[] args)
        {
            // Sets the console hight and width to the amount specified
            Console.SetWindowSize(WindowWidth, WindowHeight);
            // Positions the console
            Console.SetWindowPosition(0, 0);

            // Gives a title to the console window
            Console.Title = "MyIMDBSearcher";
            Console.CursorVisible = false;

            // Starts the Main menu loop on SearchMenu class
            SearchMenu.MenuLoop();
        }
    }
}