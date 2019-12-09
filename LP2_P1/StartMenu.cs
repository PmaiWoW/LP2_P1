using System;

namespace LP2_P1
{
    public static class StartMenu
    {
        public static void MenuLoop()
        {
            TitleSearch searcher = new TitleSearch();
            ConsoleKey answer = ConsoleKey.L;
            string wantedTitle;

            while (answer != ConsoleKey.Q)
            {
                Console.Clear();
                Console.WriteLine("1. Search Title\n" +
                                  "2. Search People\n" +
                                  "Q. Quit\n");

                answer = Console.ReadKey().Key;

                switch (answer)
                {
                    case ConsoleKey.D1:
                        PrintSearchBar();
                        wantedTitle = Console.ReadLine();
                        searcher.SearchTitle(wantedTitle);
                        Console.ResetColor();
                        break;

                    case ConsoleKey.D2:
                        Console.Clear();
                        Console.WriteLine("This search functionality has not" +
                            "been implemented yet.\nPress any key to return" +
                            "to selection.");
                        Console.ReadKey();
                        break;

                    case ConsoleKey.Q:
                        Quit();
                        break;

                    default:
                        Console.Clear();
                        Console.WriteLine("Invalid option. Press any key to " +
                            "return to selection.");
                        Console.ReadKey();
                        break;
                }
            }
        }

        public static void Quit()
        {
            Console.Clear();
            Console.WriteLine("Thank you for using this searcher, we hope " +
                "to see you again!\nPress any key to exit.");
            Console.ReadKey();
            Environment.Exit(0);
        }

        private static void PrintSearchBar()
        {
            Console.Clear();
            Console.WriteLine("Write the name of what you're looking for.\n");
            Console.BackgroundColor = ConsoleColor.White;
            Console.Write("                                                 ");
            Console.CursorLeft = 0;

            Console.ForegroundColor = ConsoleColor.Black;
        }
    }
}
