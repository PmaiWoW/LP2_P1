using System;
using System.Collections.Generic;
using System.Linq;

namespace LP2_P1
{
    public class TitleSearch
    {
        private List<TitleBasics> originalNamedTitles =
            new List<TitleBasics>(63506070);
        private IEnumerable<TitleBasics> namedTitles;
        private State listState = State.Unordered;
        private int skipNumber = 0;
        private int displayedAmount = 0;
        private const int displayNum = 30;

        public void SearchTitle(IEnumerable<TitleBasics> wantedTitle)
        {
            originalNamedTitles = wantedTitle.ToList();

            SearchMenu();
        }

        private void SearchMenu()
        {
            namedTitles = originalNamedTitles;

            ConsoleKey key = ConsoleKey.D0;
            UpdatePage();

            while (key != ConsoleKey.B)
            {
                Console.CursorLeft = 1;
                Console.Write(">");

                if (!namedTitles.Any())
                {
                    Console.Clear();
                    Console.WriteLine("No titles found, returning to main " +
                        "menu...");
                    Console.ReadKey(true);
                    return;
                }

                key = Console.ReadKey().Key;

                switch (key)
                {
                    case ConsoleKey.UpArrow:
                        if (Console.CursorTop > 1)
                        {
                            UserInterface.ClearSpace();
                            Console.CursorTop -= 1;
                        }
                        break;

                    case ConsoleKey.DownArrow:
                        if (Console.CursorTop < displayedAmount)
                        {
                            UserInterface.ClearSpace();
                            Console.CursorTop += 1;
                        }
                        break;
                    case ConsoleKey.RightArrow:
                        if (namedTitles.Count() / (skipNumber + displayNum) > 0 ||
                            skipNumber == 0)
                        {
                            skipNumber += displayNum;
                            UpdatePage();
                        }
                        break;

                    case ConsoleKey.LeftArrow:
                        if (skipNumber + displayNum > displayNum)
                        {
                            skipNumber -= displayNum;
                            UpdatePage();
                        }
                        break;

                    case ConsoleKey.Enter:
                        TitleDetails.Menu(
                            namedTitles.ElementAt((Console.CursorTop - 1) +
                            skipNumber));
                        UpdatePage();
                        break;

                    case ConsoleKey.O:
                        Sort(ref namedTitles);
                        UpdatePage();
                        break;

                    case ConsoleKey.R:
                        ReverseOrder();
                        break;

                    case ConsoleKey.T:
                        namedTitles = originalNamedTitles;
                        listState = State.Unordered;
                        break;

                    default:
                        UserInterface.PrintInvalidChoice();
                        UpdatePage();
                        break;
                }
            }
            Console.Clear();
        }

        // Sorts searched items by Type
        private void UpdatePage()
        {
            Console.Clear();

            PrintResults(namedTitles.SkipLast(namedTitles.Count() - 
                skipNumber - displayNum).Skip(skipNumber).Select(c => c)
                .ToList());
        }

        private void ReverseOrder()
        {
            //Reverses order, only if the list is already ordered
            if (listState != State.Unordered)
            {
                listState = listState == State.Descending ?
                    State.Ascending : State.Descending;

                namedTitles = namedTitles.Reverse();
            }

            UpdatePage();
        }

        private void PrintResults(IEnumerable<TitleBasics> titlesToDisplay)
        {
            UserInterface.ColorSetup(backgroundColor: ConsoleColor.Gray);

            for (int i = 0; i < Console.WindowWidth; i++)
                Console.Write(" ");

            Console.SetBufferSize(Program.WindowWidth, Program.WindowHeight);

            Console.CursorLeft = 0;
            Console.Write("    Name");
            Console.CursorLeft = 100;
            Console.Write("Type");
            Console.CursorLeft = 150;
            Console.Write($"State : {listState}");

            Console.ResetColor();

            Console.WriteLine("");

            string pTitle;
            int maxLenght = 90;

            displayedAmount = titlesToDisplay.Count();

            for (int i = 0; i < titlesToDisplay.Count(); i++)
            {
                pTitle = titlesToDisplay.ElementAt(i).PrimaryTitle;
                Console.CursorLeft = 0;
                Console.Write($"   " +
                 $"{pTitle.Substring(0, Math.Min(pTitle.Length, maxLenght))}");
                Console.CursorLeft = 100;
                Console.WriteLine($"| {titlesToDisplay.ElementAt(i).Type}");
            }

            Console.WriteLine("\n '->' for next page" +
                "\n '<-' for previous page \n" +
                "\n 'ENTER' to select title" +
                "\n 'O' to order " +
                "\n 'R' to reverse the order " +
                "\n 'T' to reset the order " +
                "\n 'B' to go back to previous menu");

            Console.CursorTop = 1;
        }

        public void Sort(ref IEnumerable<TitleBasics> namedTitles)
        {
            ConsoleKey key;

            Console.Clear();
            Console.SetCursorPosition(0, 0);

            // Display Order Options
            Console.WriteLine("\n '1' to order by type" +
                "\n '2' to order by title" +
                "\n '3' to order by adult rating" +
                "\n '4' to order by year of release" +
                "\n '5' to order by year of end" +
                "\n '6' to order by genre" +
                "\n '7' to order by rating" +
                "\n 'B' to go back \n");

            // Read user's input
            key = Console.ReadKey().Key;

            // Resets titles every time the user orders the list
            if (key != ConsoleKey.B)
            {
                namedTitles = originalNamedTitles;
                listState = State.Ascending;
            }
            // Switch case between the possible options selected
            switch (key)
            {
                case ConsoleKey.D1:
                    namedTitles = namedTitles.OrderBy(c => c.Type);
                    break;

                case ConsoleKey.D2:
                    namedTitles = namedTitles.OrderBy(c => c.PrimaryTitle);
                    break;

                case ConsoleKey.D3:
                    namedTitles = namedTitles.OrderBy(c => c.IsAdult);
                    break;

                case ConsoleKey.D4:
                    namedTitles = namedTitles.OrderBy(c => c.StartYear);
                    break;

                case ConsoleKey.D5:
                    namedTitles = namedTitles.OrderBy(c => c.EndYear);
                    break;

                case ConsoleKey.D6:
                    namedTitles = namedTitles.OrderBy(c => c.Genres[0]);
                    break;

                case ConsoleKey.B:
                    break;

                default:
                    UserInterface.PrintInvalidChoice();
                    break;
            }
        }

        private enum State { Ascending, Descending, Unordered };
    }
}