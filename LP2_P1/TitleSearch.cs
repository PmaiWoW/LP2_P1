using System;
using System.Collections.Generic;
using System.Linq;

namespace LP2_P1
{
    /// <summary>
    /// The main look of the program where the the titles are displayed and
    /// the user can interect with them
    /// </summary>
    public class TitleSearch
    {
        // Creates two IEnumerables responsible for containing the information
        private IEnumerable<(TitleBasics titles, TitleRatings ratings)> 
            originalNamedTitles;
        private IEnumerable<(TitleBasics titles, TitleRatings ratings)> 
            namedTitles;

        // Creates variables
        private OrderState listState = OrderState.Unordered;
    
        // Integer for the number of titles it should skip
        private int skipNumber = 0;
        // Integer for the number of titles it shoudl display
        private const int displayNum = 9;
        // Which parameter should the collection be srted by.
        string sortParameterString = default;

        /// <summary>
        /// Assigns this originalTitles the IEnumerable it received and goes
        /// to the main loop
        /// </summary>
        /// <param name="wantedTitle"> IEnumerable with the titles with the 
        /// parameters the user chose </param>
        public void SearchTitle(IEnumerable<(TitleBasics, TitleRatings)> 
            wantedTitle)
        {
            // Gives originalNamedTitles the contents of the IEnumerable
            // Passed as argument
            originalNamedTitles = wantedTitle.ToHashSet();

            // Goes to SearchMenu method
            SearchMenu();
        }
        /// <summary>
        /// The main loop of the menu
        /// </summary>
        private void SearchMenu()
        {
            // Equals namedTitles to originalNamedTitles
            namedTitles = originalNamedTitles;

            // Checks if there's titles on the list
            if (!namedTitles.Any())
            {
                // Displays a message to the user
                UserInterface.NoResults();
                // Returns to the searchmenu
                return;
            }

            // Creates a variable for storing user input
            ConsoleKey key = ConsoleKey.D0;
            // Updates the information displayed to the user
            UpdatePage();

            // A while loop until the user presses 'b'
            while (key != ConsoleKey.B)
            {
                // Updates the page info
                UpdatePage();
                // Assigns the value of key the key the user pressed
                key = Console.ReadKey().Key;

                if (key.ToString().Length == 2 && key.ToString()[0]=='D'
                    && char.IsDigit(key.ToString()[1]))
                {
                    if (int.TryParse(key.ToString()[1].ToString(),
                        out int keyInt) && keyInt != 0)
                    {   
                        UserInterface.Menu(namedTitles.ElementAt(keyInt - 1 +
                            skipNumber).titles, namedTitles.ElementAt(keyInt 
                            - 1 + skipNumber).ratings);
                    }
                    UpdatePage();
                }
                else
                {
                    switch (key)
                    {
                        case ConsoleKey.RightArrow:
                            if (namedTitles.Count() / (skipNumber + 
                                displayNum) > 0 || skipNumber == 0)
                            {
                                skipNumber += displayNum;
                            }
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

            UserInterface.ShowResultsMenu(namedTitles.SkipLast(namedTitles.
                Count() - skipNumber - displayNum).Skip(skipNumber).
                Select(c => c).ToList(), sortParameterString, listState);
        }

        private void ReverseOrder()
        {
            //Reverses order, only if the list is already ordered
            if (listState != OrderState.Unordered)
            {
                listState = listState == OrderState.Descending ?
                    OrderState.Ascending : OrderState.Descending;

                namedTitles = namedTitles.Reverse();
            }
            // Updates the information displayed to the user
            UpdatePage();
        }

        private void ResetTitles()
        {
            skipNumber = 0;
            namedTitles = originalNamedTitles;
            sortParameterString = default;
            listState = OrderState.Unordered;
            UpdatePage();
        }

        public void Sort(ref IEnumerable<(TitleBasics titles, TitleRatings 
            ratings)> namedTitles)
        {
            UserInterface.ShowOrderMenu(namedTitles.SkipLast(namedTitles.
                Count()- skipNumber - displayNum).Skip(skipNumber).
                Select(c => c).ToHashSet(), sortParameterString, listState);


            // Read user's input
            ConsoleKey key;
            key = Console.ReadKey().Key;

            // Resets titles and jumps to first page
            // every time the user orders the list
            if (key != ConsoleKey.B)
            {
                namedTitles = originalNamedTitles;
                listState = OrderState.Ascending;
                skipNumber = 0;
            }
            // Switch case between the possible options selected
            switch (key)
            {
                // case nº1
                case ConsoleKey.D1:
                    // Orders the list by the title
                    namedTitles = namedTitles
                        .OrderBy(c => c.titles.PrimaryTitle)
                        .Select(c => c).ToHashSet();
                    sortParameterString = "Title";
                    break;

                // case nº2
                case ConsoleKey.D2:
                    // Orders the list by the type
                    namedTitles = namedTitles
                        .OrderBy(c => c.titles.Type)
                        .Select(c => c).ToHashSet();
                    sortParameterString = "Type";
                    break;

                // case nº3
                case ConsoleKey.D3:
                    // Orders the list by if it's adult or not
                    namedTitles = namedTitles
                        .OrderBy(c => c.titles.IsAdult)
                        .Select(c => c).ToHashSet();
                    sortParameterString = "Age Restriction";
                    break;

                // case nº4
                case ConsoleKey.D4:
                    // Orders the list by the year of start
                    namedTitles = namedTitles
                        .OrderBy(c => c.titles.StartYear)
                        .Select(c => c).ToHashSet();
                    sortParameterString = "Start Year";
                    break;
                // case nº5
                case ConsoleKey.D5:
                    // Orders the list by the year of end
                    namedTitles = namedTitles
                        .OrderBy(c => c.titles.EndYear)
                        .Select(c => c).ToHashSet();
                    sortParameterString = "End Year";
                    break;
                
                case ConsoleKey.D6:
                    // Orders the list by the year of end
                    namedTitles = namedTitles
                        .OrderBy(c => c.titles.RuntimeMinutes)
                        .Select(c => c).ToHashSet();
                    sortParameterString = "Runtime";
                    break;
                // case nº6
                case ConsoleKey.D7:
                    namedTitles = namedTitles
                        .OrderBy(c => c.titles.Genres[0])
                        .Select(c => c).ToHashSet();
                    sortParameterString = "Genres";
                    break;

                // case nº7
                case ConsoleKey.D8:
                    namedTitles = namedTitles
                        .OrderBy(c => c.ratings.AverageRating)
                        .Select(c => c).ToHashSet();
                    sortParameterString = "Ratings";
                    break;

                // case the 'B' key is pressed
                case ConsoleKey.B:
                    break;

                // If the input is unexpected
                default:
                    break;
            }
            // Makes sure the Garbage collector runs after ordering
            GC.Collect();
        }
    }
}