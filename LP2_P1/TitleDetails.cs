using System;

namespace LP2_P1
{
    internal static class TitleDetails
    {
        public static void Menu(TitleBasics title, TitleRatings ratings)
        {
            ConsoleKey key = ConsoleKey.D0;

            while (key != ConsoleKey.B)
            {
                Console.Clear();
                Console.WriteLine(title);
                Console.WriteLine(ratings);

                Console.CursorLeft = 1;
                Console.WriteLine("\nPress 'B' to to back to previous menu");

                key = Console.ReadKey().Key;
            }
        }
    }
}