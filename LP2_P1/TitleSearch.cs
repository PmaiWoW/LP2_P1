using System;
using System.Collections.Generic;
using System.Linq;

namespace LP2_P1
{
    internal class TitleSearch
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
                .Where(c => c.OriginalTitle.ToLower()
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
                        SortByTitle();
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

        private void SortByTitle()
        {
            listState = State.Descending;
            SearchSelected(namedTitles.OrderBy(c => c.TitleType));
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
                Console.Write($"   {titlesToDisplay.ElementAt(i).OriginalTitle}");
                Console.CursorLeft = 100;
                Console.WriteLine($"| {titlesToDisplay.ElementAt(i).TitleType}");
            }
        }

        private enum State { Ascending, Descending, Unordered };
    }
}