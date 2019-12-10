using System;
using System.Collections.Generic;
using System.Text;

namespace LP2_P1
{
    static class TitleDetails
    {
        public static void DisplayInfo(TitleBasics title)
        {
            int nonNullGenres = 0;
            Console.Clear();
            Console.WriteLine(title);
            Console.CursorTop = 0;
            Menu(nonNullGenres, title);
        }

        private static void Menu(int nonNullGenres, TitleBasics title)
        {
            ConsoleKey key = ConsoleKey.D0;

            while (key != ConsoleKey.B)
            {
                Console.CursorLeft = 1;
                Console.Write(">");

                key = Console.ReadKey().Key;

                switch (key)
                {
                    case ConsoleKey.UpArrow:
                        if (Console.CursorTop > 0)
                        {
                            Console.CursorLeft -= 2;
                            Console.Write(" ");
                            Console.CursorTop -= 1;
                        }
                        break;
                    case ConsoleKey.DownArrow:
                        if (Console.CursorTop < 5 + nonNullGenres)
                        {
                            Console.CursorLeft -= 2;
                            Console.Write(" ");
                            Console.CursorTop += 1;
                        }
                        break;
                    case ConsoleKey.B:
                        Console.Clear();
                        Console.WriteLine("Going back to the previous menu." +
                            "\nPress any key to continue.");
                        Console.ReadKey();
                        break;
                }
            }
        }
    }
}
