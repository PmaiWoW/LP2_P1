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
        private const int displayNum = 9;
        string sortParameterString = default;

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
                if (!namedTitles.Any())
                {
                    Console.Clear();
                    Console.WriteLine("No titles found, returning to main " +
                        "menu...");
                    Console.ReadKey(true);
                    return;
                }

                key = Console.ReadKey().Key;

                if (key.ToString().Length == 2 && key.ToString()[0]=='D'
                    && char.IsDigit(key.ToString()[1]))
                {
                    if (int.TryParse(key.ToString()[1].ToString(),
                        out int keyInt) && keyInt != 0)
                    {
                        TitleDetails.Menu(namedTitles.ElementAt(keyInt - 1 +
                            skipNumber));
                        UpdatePage();
                    }
                }
                else
                {
                    switch (key)
                    {
                        case ConsoleKey.RightArrow:
                            if (namedTitles.Count()/(skipNumber + displayNum)>0
                                || skipNumber == 0)
                                    skipNumber += displayNum;
                            UpdatePage();
                            break;

                        case ConsoleKey.LeftArrow:
                            if (skipNumber + displayNum > displayNum)
                                skipNumber -= displayNum;
                            UpdatePage();
                            break;

                        case ConsoleKey.O:
                            Sort(ref namedTitles);
                            break;

                        case ConsoleKey.R:
                            ReverseOrder();
                            break;

                        case ConsoleKey.T:
                            ResetTitles();
                            break;

                        default:
                            UpdatePage();
                            break;
                    }
                }

            }
            Console.Clear();
        }

        // Sorts searched items by Type
        private void UpdatePage()
        {
            Console.Clear();
            UserInterface.ResizeWindow();

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

        private void ResetTitles()
        {
            namedTitles = originalNamedTitles;
            sortParameterString = default;
            listState = State.Unordered;
            UpdatePage();
        }

        private void PrintResults(IEnumerable<TitleBasics> titlesToDisplay)
        {
            UserInterface.ColorSetup(backgroundColor: ConsoleColor.Gray);

            for (int i = 0; i < Console.WindowWidth; i++)
                Console.Write(" ");

            Console.SetBufferSize(Program.WindowWidth, Program.WindowHeight);

            Console.Write("    Name");
            Console.CursorLeft = 100;
            Console.Write(sortParameterString);
            Console.CursorLeft = 150;
            if (listState != State.Unordered)
                Console.Write($"State : {listState}");

            Console.ResetColor();

            Console.WriteLine("");

            string pTitle;
            int maxLenght = 90;

            for (int i = 0; i < titlesToDisplay.Count(); i++)
            {
                pTitle = $"{i+1}: {titlesToDisplay.ElementAt(i).PrimaryTitle}";
                Console.CursorLeft = 0;
                Console.Write($"   " +
                 $"{pTitle.Substring(0, Math.Min(pTitle.Length, maxLenght))}");
                Console.CursorLeft = 100;
                Console.WriteLine($"| {titlesToDisplay.ElementAt(i).Type}");
            }

            Console.WriteLine("\n '->' for next page" +
                "\n '<-' for previous page" +
                "\n '1'-'9' to select title" +
                "\n 'O' to order" +
                "\n 'R' to reverse the order" +
                "\n 'T' to reset the order" +
                "\n 'B' to go back to previous menu");

            Console.CursorTop = 1;
        }

        public void Sort(ref IEnumerable<TitleBasics> namedTitles)
        {
            ConsoleKey key;

            UserInterface.OrderMenu();

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
                    sortParameterString = "Type"; 
                    namedTitles = namedTitles.OrderBy(c => c.Type);
                    break;

                case ConsoleKey.D2:
                    sortParameterString = "PrimaryTitle";
                    namedTitles = namedTitles.OrderBy(c => c.PrimaryTitle);
                    break;

                case ConsoleKey.D3:
                    sortParameterString = "IsAdult";
                    namedTitles = namedTitles.OrderBy(c => c.IsAdult);
                    break;

                case ConsoleKey.D4:
                    sortParameterString = "StartYear";
                    namedTitles = namedTitles.OrderBy(c => c.StartYear);
                    break;

                case ConsoleKey.D5:
                    sortParameterString = "EndYear";
                    namedTitles = namedTitles.OrderBy(c => c.EndYear);
                    break;

                case ConsoleKey.D6:
                    sortParameterString = "Genres";
                    namedTitles = namedTitles.OrderBy(c => c.Genres[0]);
                    break;

                case ConsoleKey.B:
                    break;

                default:
                    break;
            }
            UpdatePage();
        }

        private enum State { Ascending, Descending, Unordered };
    }
}