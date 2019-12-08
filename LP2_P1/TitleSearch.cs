using System;
using System.Collections.Generic;
using System.Linq;

namespace LP2_P1
{
    public class TitleSearch
    {
        // ############### Everything here is still subject to change, it is still not to my liking #################
        // (Menu barely works, especially if the list is empty)

        // If there's no results it closes automatically
        private IEnumerable<TitleBasics> namedTitles;

        private State listState = State.Unordered;
        private int skipNumber = 30;

        public void SearchTitle(string wantedTitle)
        {
            namedTitles = FileLoader.LoadTitleBasics()
                .Where(c => c.PrimaryTitle.ToLower()
                .Contains(wantedTitle.Trim().ToLower()))
                .Select(c => c).ToList();

            SearchSelected(namedTitles);
        }

        private void SearchSelected(IEnumerable<TitleBasics> titles)
        {
            string input = "l";

            IEnumerable<TitleBasics> titleBasics;

            ConsoleKey key = ConsoleKey.L;

            while (key != ConsoleKey.N)
            {
                titleBasics = titles.SkipLast(titles.Count() - skipNumber)
                .Skip(skipNumber - 30).Select(c => c)
                .ToList();

                PrintResults(titleBasics);

                Console.WriteLine("\n 'O' to order " +
                    "\n 'R' to reverse the order " +
                    "\n 'N' to leave " +
                    "\n 'T' to reset to unordered " +
                    "\n 'W' for previous page" +
                    "\n 'S' for next page \n");

                key = Console.ReadKey().Key;

                switch (key)
                {
                    case ConsoleKey.O:
                        Sort(out key);
                        break;

                    case ConsoleKey.R:
                        ReverseOrder(titles);
                        break;

                    case ConsoleKey.W:
                        if (skipNumber > 30)
                            skipNumber -= 30;
                        break;

                    case ConsoleKey.S:
                        if (titles.Count() / skipNumber > 0)
                            skipNumber += 30;
                        break;

                    case ConsoleKey.T:
                        listState = State.Unordered;
                        SearchSelected(namedTitles);
                        break;

                    case ConsoleKey.UpArrow:
                        Console.WriteLine("Up pressed");
                        break;

                    case ConsoleKey.DownArrow:
                        Console.WriteLine("Down Pressed");
                        break;
                }
            }
        }

        private void Sort(out ConsoleKey key)
        {
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
        }

        // Sorts searched items by Type
        private void SortByType()
        {
            listState = State.Descending;
            SearchSelected(namedTitles.OrderBy(c => c.Type));
        }

        // Sorts searched items by PrimaryTitle
        private void SortByTitle()
        {
            listState = State.Descending;
            SearchSelected(namedTitles.OrderBy(c => c.PrimaryTitle));
        }

        // Sorts searched items by IsAdult
        private void SortByIsAdult()
        {
            listState = State.Descending;
            SearchSelected(namedTitles.OrderBy(c => c.IsAdult));
        }

        // Sorts searched items by StartYear
        private void SortByRelease()
        {
            listState = State.Descending;
            SearchSelected(namedTitles.OrderBy(c => c.StartYear));
        }

        // Sorts searched items by EndYear
        private void SortByEnd()
        {
            listState = State.Descending;
            SearchSelected(namedTitles.OrderBy(c => c.EndYear));
        }

        // Sorts searched items by Genres
        private void SortByGenre()
        {
            listState = State.Descending;
            SearchSelected(namedTitles.OrderBy(c => c.Genres[0]));
        }

        private void ReverseOrder(IEnumerable<TitleBasics> titles)
        {
            if (listState != State.Unordered)
                listState = listState == State.Descending ?
                    State.Ascending : State.Descending;

            SearchSelected(titles.Reverse());
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

            for (int i = 0; i < titlesToDisplay.Count(); i++)
            {
                Console.CursorLeft = 0;
                Console.Write($"   {titlesToDisplay.ElementAt(i).PrimaryTitle}");
                Console.CursorLeft = 100;
                Console.WriteLine($"| {titlesToDisplay.ElementAt(i).Type}");
            }
        }

        private enum State { Ascending, Descending, Unordered };
    }
}