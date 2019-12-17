using System;

namespace LP2_P1
{
    /// <summary>
    /// Responsible for running the program
    /// </summary>
    public class Program
    {
        // Holds the desired height and width of the console
        public const int WindowWidth = 200;
        public const int WindowHeight = 60;

        // Declare 'SearchMenu' searchMenu;
        private static SearchMenu searchMenu;

        /// <summary>
        /// Runs program and its methods
        /// </summary>
        /// <param name="args"></param>
        private static void Main(string[] args)
        {
            // Instantiate searchMenu as new SearchMenu();
            searchMenu = new SearchMenu();

            // Sets the console hight and width to the amount specified
            Console.SetWindowSize(WindowWidth, WindowHeight);

            // Positions the console
            Console.SetWindowPosition(0, 0);

            // Gives a title to the console window
            Console.Title = "MyIMDBSearcher";

            // Sets cursor as invisible
            Console.CursorVisible = false;

            // Starts the Main menu loop on SearchMenu class
            searchMenu.MenuLoop();
        }
    }
}