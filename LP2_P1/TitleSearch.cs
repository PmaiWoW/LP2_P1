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
                        UserInterface.ShowDetailsMenu(namedTitles.ElementAt(keyInt - 1 +
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
                            Order(ref namedTitles);
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

        // Order searched items by Type
        private void UpdatePage()
        {
            UserInterface.ResizeWindow();

            UserInterface.ShowResultsMenu(namedTitles.SkipLast(namedTitles.
                Count() - skipNumber - displayNum).Skip(skipNumber).
                Select(c => c).ToList(), orderParameterString, listState);
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
            orderParameterString = default;
            listState = OrderState.Unordered;
            UpdatePage();
        }

        public void Order(ref IEnumerable<(TitleBasics titles, TitleRatings 
            ratings)> namedTitles)
        {
            UserInterface.ShowOrderMenu(namedTitles.SkipLast(namedTitles.
                Count()- skipNumber - displayNum).Skip(skipNumber).
                Select(c => c).ToHashSet(), orderParameterString, listState);


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
                // case the 'D1' key is pressed
                case ConsoleKey.D1:
                    // Orders the list by the title
                    namedTitles = namedTitles
                        .OrderBy(c => c.titles.PrimaryTitle)
                        .Select(c => c).ToHashSet();
                    orderParameterString = "Title";
                    break;
                // case the 'D2' key is pressed
                case ConsoleKey.D2:
                    // Orders the list by the type
                    namedTitles = namedTitles
                        .OrderBy(c => c.titles.Type)
                        .Select(c => c).ToHashSet();
                    orderParameterString = "Type";
                    break;
                // case the 'D3' key is pressed
                case ConsoleKey.D3:
                    // Orders the list by if it's adult or not
                    namedTitles = namedTitles
                        .OrderBy(c => c.titles.IsAdult)
                        .Select(c => c).ToHashSet();
                    orderParameterString = "Age Restriction";
                    break;
                // case the 'D4' key is pressed
                case ConsoleKey.D4:
                    // Orders the list by the year of start
                    namedTitles = namedTitles
                        .OrderBy(c => c.titles.StartYear)
                        .Select(c => c).ToHashSet();
                    orderParameterString = "Start Year";
                    break;
                // case the 'D5' key is pressed
                case ConsoleKey.D5:
                    // Orders the list by the year of end
                    namedTitles = namedTitles
                        .OrderBy(c => c.titles.EndYear)
                        .Select(c => c).ToHashSet();
                    orderParameterString = "End Year";
                    break;
                // case the 'D6' key is pressed
                case ConsoleKey.D6:
                    // Orders the list by the runtime
                    namedTitles = namedTitles
                        .OrderBy(c => c.titles.RuntimeMinutes)
                        .Select(c => c).ToHashSet();
                    orderParameterString = "Runtime";
                    break;
                // case the 'D7' key is pressed
                case ConsoleKey.D7:
                    // Orders the list by the first genre
                    namedTitles = namedTitles
                        .OrderBy(c => c.titles.Genres[0])
                        .Select(c => c).ToHashSet();
                    orderParameterString = "Genres";
                    break;

                // case the 'D8' key is pressed
                case ConsoleKey.D8:
                    namedTitles = namedTitles
                        // Orders the list by the ratings

                        .OrderBy(c => c.ratings.AverageRating)
                        .Select(c => c).ToHashSet();
                    orderParameterString = "Ratings";
                    break;

                // case the 'B' key is pressed
                case ConsoleKey.B:
                    break;
            }
            // Makes sure the Garbage collector runs after ordering
            GC.Collect();
        }
    }
}