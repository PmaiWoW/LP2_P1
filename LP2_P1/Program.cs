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

            TitleSearch a = new TitleSearch();

            Console.WriteLine("Write the title of what you're looking for.\n");
            Console.BackgroundColor = ConsoleColor.White;
            Console.Write("                                                  ");
            Console.CursorLeft = 0;

            Console.ForegroundColor = ConsoleColor.Black;
            string wantedTitle = Console.ReadLine();
            Console.ResetColor();

            a.SearchTitle(wantedTitle);
        }
    }
}