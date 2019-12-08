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

            Console.WriteLine($"   {title.OriginalTitle}");
            Console.WriteLine($"   {title.PrimaryTitle}");
            Console.WriteLine($"   {title.IsAdult}");
            Console.WriteLine($"   {title.RuntimeMinutes}");
            Console.WriteLine($"   {title.StartYear} / {title.EndYear}");
            Console.WriteLine($"   >To be changed<");
            for (int a = 0; a < title.Genres.Length; a++)
                if (title.Genres[a] != null)
                {
                    nonNullGenres++;
                    Console.WriteLine($"   {title.Genres[a]}");
                }

            Console.CursorTop = 0;
            Menu(nonNullGenres, title);
        }

        private static void Menu(int nonNullGenres, TitleBasics title)
        {
            ConsoleKey key = ConsoleKey.L;

            while (key != ConsoleKey.Escape)
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
                }
            }
        }
    }
}
