using System;
using System.Collections.Generic;
using System.Linq;

namespace LP2_P1
{
    public class TitleSearch
    {
        private IEnumerable<TitleBasics> namedTitles;
        private State listState = State.Unordered;
        private int skipNumber = 30;
        private int displayedAmount = 0;

        public void SearchTitle(string wantedTitle)
        {
            namedTitles = FileLoader.LoadTitleBasics()
                .Where(c => c.PrimaryTitle.ToLower()
                .Contains(wantedTitle.Trim().ToLower()))
                .Select(c => c).ToList();

            SearchSelected();
        }

        private void SearchSelected()
        {
            ConsoleKey key = ConsoleKey.L;

            if(!namedTitles.Any(c => c.TConst != null))
            {
                Console.Clear();
                Console.WriteLine("No titles found, returning to search " +
                    "menu...");
                return;
            }
            UpdatePage();

            while (key != ConsoleKey.B)
            {
                Console.CursorLeft = 1;
                Console.Write(">");

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
                        Sort(out key);
                        break;

                    case ConsoleKey.R:
                        ReverseOrder();
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

        private void Sort(out ConsoleKey key)
        {
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
                    SortByType();
                    break;

                case ConsoleKey.D2:
                    SortByTitle();
                    break;

                case ConsoleKey.D3:
                    SortByIsAdult();
                    break;

                case ConsoleKey.D4:
                    SortByRelease();
                    break;

                case ConsoleKey.D5:
                    SortByEnd();
                    break;

                case ConsoleKey.D6:
                    SortByGenre();
                    break;
            }
            UpdatePage();
        }

        #region --- Utilities---

        // Sorts searched items by Type
        private void SortByType()
        {
            listState = State.Descending;
            namedTitles = namedTitles.OrderBy(c => c.Type);
            UpdatePage();
        }

        // Sorts searched items by PrimaryTitle
        private void SortByTitle()
        {
            listState = State.Descending;
            namedTitles = namedTitles.OrderBy(c => c.PrimaryTitle);
            UpdatePage();
        }

        // Sorts searched items by StartYear
        private void SortByRelease()
        {
            listState = State.Descending;
            namedTitles = namedTitles.OrderBy(c => c.StartYear);
            UpdatePage();
        }

        // Sorts searched items by EndYear
        private void SortByEnd()
        {
            listState = State.Descending;
            namedTitles = namedTitles.OrderBy(c => c.EndYear);
            UpdatePage();
        }

        // Sorts searched items by Genres
        private void SortByGenre()
        {
            listState = State.Descending;
            namedTitles = namedTitles.OrderBy(c => c.Genres[0]);
            UpdatePage();
        }

        // Sorts searched items by IsAdult
        private void SortByIsAdult()
        {
            listState = State.Descending;
            namedTitles = namedTitles.OrderBy(c => c.IsAdult);
            UpdatePage();
        }

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
                "\n 'R' to reverse the order " +
                "\n 'ESC' to leave " +
                "\n '->' for previous page" +
                "\n '<-' for next page \n");

            Console.CursorTop = 1;
        }

        #endregion --- Utilities---

        private enum State { Ascending, Descending, Unordered };
    }
}