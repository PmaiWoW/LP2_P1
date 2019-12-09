using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace LP2_P1
{
    public static class Sorter
    {
        public static void Sort(ref IEnumerable<TitleBasics> namedTitles)
        {
            ConsoleKey key;

            Console.Clear();
            Console.CursorLeft = 0;
            Console.CursorTop = 0;


            // Display Order Options
            Console.WriteLine("\n '1' to order by type" +
                "\n '2' to order by title" +
                "\n '3' to order by adult rating" +
                "\n '4' to order by year of release" +
                "\n '5' to order by year of end" +
                "\n '6' to order by genre" +
                "\n 'B' to go back \n");

            // Read user's input
            key = Console.ReadKey().Key;

            // Switch case between the possible options selected
            switch (key)
            {
                case ConsoleKey.D1:
                    namedTitles = SortByType(namedTitles);
                    break;

                case ConsoleKey.D2:
                    namedTitles = SortByTitle(namedTitles);
                    break;

                case ConsoleKey.D3:
                    namedTitles = SortByIsAdult(namedTitles);
                    break;

                case ConsoleKey.D4:
                    namedTitles = SortByRelease(namedTitles);
                    break;

                case ConsoleKey.D5:
                    namedTitles = SortByEnd(namedTitles);
                    break;

                case ConsoleKey.D6:
                    namedTitles = SortByGenre(namedTitles);
                    break;

                case ConsoleKey.B:
                    Console.Clear();
                    Console.WriteLine("Going back to the previous menu." +
                        "\nPress any key to continue.");
                    Console.ReadKey();
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine("Invalid option. Press any key to " +
                            "return to previous menu.");
                    Console.ReadKey();
                    break;
            }

        }

        public static IEnumerable<TitleBasics> SortByType(IEnumerable<TitleBasics> namedTitles)
        {
            return namedTitles.OrderBy(c => c.Type);
        }

        // Sorts searched items by PrimaryTitle
        public static IEnumerable<TitleBasics> SortByTitle(IEnumerable<TitleBasics> namedTitles)
        {
            return namedTitles.OrderBy(c => c.PrimaryTitle);
        }

        // Sorts searched items by StartYear
        public static IEnumerable<TitleBasics> SortByRelease(IEnumerable<TitleBasics> namedTitles)
        {
            return namedTitles.OrderBy(c => c.StartYear);
        }

        // Sorts searched items by EndYear
        public static IEnumerable<TitleBasics> SortByEnd(IEnumerable<TitleBasics> namedTitles)
        {
            return namedTitles.OrderBy(c => c.EndYear);
        }

        // Sorts searched items by Genres
        public static IEnumerable<TitleBasics> SortByGenre(IEnumerable<TitleBasics> namedTitles)
        {
            return namedTitles.OrderBy(c => c.Genres[0]);
        }

        // Sorts searched items by IsAdult
        public static IEnumerable<TitleBasics> SortByIsAdult(IEnumerable<TitleBasics> namedTitles)
        {
            return namedTitles.OrderBy(c => c.IsAdult);
        }
    }
}
