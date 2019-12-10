using System;
using System.Collections.Generic;
using System.Linq;

namespace LP2_P1
{
    public class TitleSearch
    {
        private IEnumerable<TitleBasics> originalNamedTitles;
        private IEnumerable<TitleBasics> namedTitles;
        private State listState = State.Unordered;
        private int skipNumber = 30;
        private int displayedAmount = 0;

        public void SearchTitle(string wantedTitle)
        {
            originalNamedTitles = FileLoader.LoadTitleBasics()
                .Where(c => c.PrimaryTitle.ToLower()
                .Contains(wantedTitle.Trim().ToLower()))
                .Select(c => c).ToList();

            SearchMenu();
        }

        private void SearchMenu()
        {
            namedTitles = originalNamedTitles;

            ConsoleKey key = ConsoleKey.L;
            UpdatePage();

            while (key != ConsoleKey.B)
            {
                key = ConsoleKey.D0;
                Console.CursorLeft = 1;
                Console.Write(">");

                if (!namedTitles.Any())
                {
                    Console.Clear();
                    Console.WriteLine("No titles found, returning to main " +
                        "menu...");
                    Console.ReadKey();
                    return;
                }

                key = Console.ReadKey().Key;

                switch (key)
                {
                    case ConsoleKey.RightArrow:
                        if (namedTitles.Count() / skipNumber > 0)
                        {
                            skipNumber += 30;
                            UpdatePage();
                        }
                        break;

                    case ConsoleKey.LeftArrow:
                        if (skipNumber > 30)
                        {
                            skipNumber -= 30;
                            UpdatePage();
                        }
                        break;

                    case ConsoleKey.UpArrow:
                        if (Console.CursorTop > 1)
                        {
                            Console.CursorLeft -= 2;
                            Console.Write(" ");
                            Console.CursorTop -= 1;
                        }
                        break;

                    case ConsoleKey.DownArrow:
                        if (Console.CursorTop < displayedAmount)
                        {
                            Console.CursorLeft -= 2;
                            Console.Write(" ");
                            Console.CursorTop += 1;
                        }
                        break;

                    case ConsoleKey.O:
                        Sorter.Sort(ref namedTitles);
                        UpdatePage();
                        break;

                    case ConsoleKey.R:
                        ReverseOrder();
                        break;

                    case ConsoleKey.F:
                        Filter();
                        UpdatePage();
                        break;

                    case ConsoleKey.T:
                        Console.Clear();
                        namedTitles = originalNamedTitles;
                        Console.WriteLine("Search results have been reset to" +
                            " name only.\nPress any key to continue.");
                        Console.ReadKey();
                        UpdatePage();
                        break;

                    case ConsoleKey.Enter:
                        TitleDetails.DisplayInfo(
                            namedTitles.ElementAt(Console.CursorTop - 1));
                        UpdatePage();
                        UpdatePage();
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
                            "return to selection.");
                        Console.ReadKey();
                        UpdatePage();
                        break;
                }
            }
            Console.Clear();
        }

        // Sorts searched items by Type
        private void UpdatePage()
        {
            PrintResults(namedTitles.SkipLast(namedTitles.Count() - skipNumber)
                .Skip(skipNumber - 30).Select(c => c)
                .ToList());
        }

        private void ReverseOrder()
        {
            if (listState != State.Unordered)
                listState = listState == State.Descending ?
                    State.Ascending : State.Descending;

            namedTitles = namedTitles.Reverse();
            UpdatePage();
        }

        private void Filter()
        {
            ConsoleKey key = ConsoleKey.D0;

            Console.Clear();
            Console.WriteLine("Select what the pretended filter:\n" +
                "\n'1' to filter by a type" +
                "\n'2' to filter by adult rating" +
                "\n'3' to filter by a release year" +
                "\n'4' to filter by a end year" +
                "\n'5' to filter by a genre" +
                "\n'B' to go back to previous menu");

            key = Console.ReadKey().Key;
            string input;

            switch (key)
            {
                case ConsoleKey.D1:
                    Console.Clear();
                    Console.WriteLine("Available types are:" +
                        "\nShort - Movie - tvMovie - tvSeries - tvEpisode - " +
                        "tvShort - tvMiniSeries - tvSpecial - Video - " +
                        "Videogame" +
                        "\nInsert the desired type:");

                    input = Console.ReadLine();
                    if (Enum.TryParse(input.ToUpper(), out TitleType type))
                    {
                        namedTitles =
                            (from title in namedTitles
                             where title.Type == type
                             select title).ToList();
                    }
                    break;
                case ConsoleKey.D5:
                    Console.Clear();
                    Console.WriteLine("Documentary - Short - Animation - " +
                        "Comedy - Romance - Sport - Action - News - Drama" +
                        "\nFantasy - Horror - Biography - Music - War - " +
                        "Crime - Western - Family - Adventure - History - " +
                        "Mystery - 0SciFi - Thriller - Musical - FilmNoir - " +
                        "\nGameShow - TalkShow - RealityTV - Adult");

                    input = Console.ReadLine();
                    if (Enum.TryParse(input.Trim().ToUpper(), 
                        out TitleGenre genre))
                    {
                        namedTitles =
                            (from title in namedTitles
                            where title.Genres.Contains(genre)
                            select title).ToList();
                    }
                    break;

                case ConsoleKey.B:
                    Console.Clear();
                    Console.WriteLine("Going back to the previous menu." +
                        "\nPress any key to continue.");
                    break;
            }

        }

        private void PrintResults(IEnumerable<TitleBasics> titlesToDisplay)
        {
            Console.Clear();

            Console.BackgroundColor = ConsoleColor.Gray;
            Console.ForegroundColor = ConsoleColor.Black;

            for (int i = 0; i < Console.WindowWidth; i++)
                Console.Write(" ");

            Console.CursorLeft = 0;
            Console.Write("    Name");
            Console.CursorLeft = 100;
            Console.Write("Type");
            Console.CursorLeft = 150;
            Console.Write($"State : {listState}");

            Console.ResetColor();

            Console.WriteLine("");

            displayedAmount = titlesToDisplay.Count();

            for (int i = 0; i < titlesToDisplay.Count(); i++)
            {
                Console.CursorLeft = 0;
                Console.Write($"   {titlesToDisplay.ElementAt(i).PrimaryTitle}");
                Console.CursorLeft = 100;
                Console.WriteLine($"| {titlesToDisplay.ElementAt(i).Type}");
            }

            Console.WriteLine("\n 'O' to order " +
                "\n 'F' to filter " +
                "\n 'R' to reverse the order " +
                "\n 'T' to reset search list" +
                "\n 'B' to go back to previous menu" +
                "\n 'ENTER' to select title" +
                "\n '->' for previous page" +
                "\n '<-' for next page \n");

            Console.CursorTop = 1;
        }

        private enum State { Ascending, Descending, Unordered };
    }
}