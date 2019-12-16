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
        private SortState listState = SortState.Unordered;
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

            UserInterface.PrintResults(namedTitles.SkipLast(namedTitles.Count()
                - skipNumber - displayNum).Skip(skipNumber).Select(c => c)
                .ToList(), sortParameterString, listState);
        }

        private void ReverseOrder()
        {
            //Reverses order, only if the list is already ordered
            if (listState != SortState.Unordered)
            {
                listState = listState == SortState.Descending ?
                    SortState.Ascending : SortState.Descending;

                namedTitles = namedTitles.Reverse();
            }

            UpdatePage();
        }

        private void ResetTitles()
        {
            namedTitles = originalNamedTitles;
            sortParameterString = default;
            listState = SortState.Unordered;
            UpdatePage();
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
                listState = SortState.Ascending;
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
        public enum SortState { Ascending, Descending, Unordered };
    }
}