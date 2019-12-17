using System;
using System.Collections.Generic;
using System.Linq;

namespace LP2_P1
{
    public class TitleSearch
    {
        private IEnumerable<TitleBasics> originalNamedtitles;
        private IEnumerable<TitleBasics> namedTitles;
        private SortState listState = SortState.Unordered;
        private int skipNumber = 0;
        private const int displayNum = 9;
        string sortParameterString = default;

        public void SearchTitle(IEnumerable<TitleBasics> wantedTitle)
        {
            originalNamedtitles = wantedTitle.ToHashSet();

            SearchMenu();
        }

        private void SearchMenu()
        {
            namedTitles = originalNamedtitles;

            if (!namedTitles.Any())
            {
                UserInterface.NoResults();
                return;
            }

            ConsoleKey key = ConsoleKey.D0;
            UpdatePage();

            while (key != ConsoleKey.B)
            {
                key = Console.ReadKey().Key;

                if (key.ToString().Length == 2 && key.ToString()[0]=='D'
                    && char.IsDigit(key.ToString()[1]))
                {
                    if (int.TryParse(key.ToString()[1].ToString(),
                        out int keyInt) && keyInt != 0)
                    {
                        TitleDetails.Menu(namedTitles.ElementAt(keyInt - 1 +
                            skipNumber));
                    }
                    UpdatePage();
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
            UserInterface.ResizeWindow();

            UserInterface.ResultsMenu(namedTitles.SkipLast(namedTitles.Count()
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
            namedTitles = originalNamedtitles;
            sortParameterString = default;
            listState = SortState.Unordered;
            UpdatePage();
        }

        public void Sort(ref IEnumerable<TitleBasics> originalNTRef)
        {
            UserInterface.OrderMenu(namedTitles.SkipLast(namedTitles.Count()
                - skipNumber - displayNum).Skip(skipNumber).Select(c => c)
                .ToList(), sortParameterString, listState);


            // Read user's input
            ConsoleKey key;
            key = Console.ReadKey().Key;

            // Resets titles and jumps to first page
            // every timethe user orders the list
            if (key != ConsoleKey.B)
            {
                originalNTRef = this.originalNamedtitles;
                listState = SortState.Ascending;
                skipNumber = 0;
            }
            // Switch case between the possible options selected
            switch (key)
            {
                case ConsoleKey.D1:
                    sortParameterString = "Title";
                    originalNTRef = originalNTRef.OrderBy(c => c.PrimaryTitle);
                    break;

                case ConsoleKey.D2:
                    sortParameterString = "Type"; 
                    originalNTRef = originalNTRef.OrderBy(c => c.Type);
                    break;

                case ConsoleKey.D3:
                    sortParameterString = "Age Restriction";
                    originalNTRef = originalNTRef.OrderBy(c => c.IsAdult);
                    break;

                case ConsoleKey.D4:
                    sortParameterString = "Start Year";
                    originalNTRef = originalNTRef.OrderBy(c => c.StartYear);
                    break;

                case ConsoleKey.D5:
                    sortParameterString = "End Year";
                    originalNTRef = originalNTRef.OrderBy(c => c.EndYear);
                    break;

                case ConsoleKey.D6:
                    sortParameterString = "Genres";
                    originalNTRef = originalNTRef.OrderBy(c => c.Genres[0]);
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