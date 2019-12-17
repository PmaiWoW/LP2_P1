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
        // Creates IEnumerable responsible for
        // containing original, untampered information
        private IEnumerable<(TitleBasics titles, TitleRatings ratings)> 
            originalNamedTitles;

        // Creates IEnumerable responsible for containing 
        // the information that will be changed constantly
        private IEnumerable<(TitleBasics titles, TitleRatings ratings)> 
            namedTitles;

        // Creates OrderState variable that stores the state of the order
        private OrderState listState = OrderState.Unordered;
    
<<<<<<< HEAD
        // Integer for the number of titles it should skip
        private int skipNumber = 0;

        // Sets how many titles are shown at once
        private const int displayNum = 9;

        // Sets string that tells what property is being ordered
        string orderParameterString = default;

        /// <summary>
        /// Sets 'originalNamedTitles' as the default list for the searched titles
        /// </summary>
        /// <param name="wantedTitles">search results list</param>
=======
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
>>>>>>> 317792027de90c371d3ae05048968b774dd41de9
        public void SearchTitle(IEnumerable<(TitleBasics, TitleRatings)> 
            wantedTitles)
        {
            // Gives originalNamedTitles the contents of the IEnumerable
            // passed as argument
            originalNamedTitles = wantedTitles.ToHashSet();

            // Calls 'SearchMenu' method
            SearchMenu();
        }
        /// <summary>
<<<<<<< HEAD
        /// Reads user input on what to do in the menu
=======
        /// The main loop of the menu
>>>>>>> 317792027de90c371d3ae05048968b774dd41de9
        /// </summary>
        private void SearchMenu()
        {
            // Copies the value of 'originalNamedTitles' to 'namedTitles'
            namedTitles = originalNamedTitles;

<<<<<<< HEAD
            // Checks if 'namedTitles' has any values
            if (!namedTitles.Any())
            {
                // Displays no results message
                UserInterface.NoResults();

                // Returns to previous method
=======
            // Checks if there's titles on the list
            if (!namedTitles.Any())
            {
                // Displays a message to the user
                UserInterface.NoResults();
                // Returns to the searchmenu
>>>>>>> 317792027de90c371d3ae05048968b774dd41de9
                return;
            }

            // Creates a variable for storing user input
            ConsoleKey key = ConsoleKey.D0;

            // Updates the information displayed to the user
            UpdatePage();

            // A while loop until the user presses 'B'
            while (key != ConsoleKey.B)
            {
<<<<<<< HEAD
                // Updates the information displayed to the user
=======
                // Updates the page info
>>>>>>> 317792027de90c371d3ae05048968b774dd41de9
                UpdatePage();

                // Assigns to the value of key, the key the user pressed
                key = Console.ReadKey().Key;

                // Checks if user pressed any D# key
                if (key.ToString().Length == 2 && key.ToString()[0]=='D'
                    && char.IsDigit(key.ToString()[1]))
                {
                    // Tries to parse the second char of that
                    // key as int, gets 'keyInt' as value and
                    // checks if 'keyInt' is differnt than 0
                    if (int.TryParse(key.ToString()[1].ToString(),
                        out int keyInt) && keyInt != 0)
                    {   
                        // Shows title details of the title the user selected
                        UserInterface.ShowDetailsMenu(namedTitles.ElementAt(keyInt - 1 +
                            skipNumber).titles, namedTitles.ElementAt(keyInt 
                            - 1 + skipNumber).ratings);
                    }
                    // Updates the information displayed to the user
                    UpdatePage();
                }
                // If key is not a D#
                else
                {
                    // Switch of the 'key' value
                    switch (key)
                    {
                        // case the 'RightArrow' key is pressed
                        case ConsoleKey.RightArrow:

                            // Skips required values to move
                            //one page to the right
                            if (namedTitles.Count() / (skipNumber + 
                                displayNum) > 0 || skipNumber == 0)
                            {
                                skipNumber += displayNum;
                            }

                            // Updates the information displayed to the user
                            UpdatePage();
                            break;

                        // case the 'LeftArrow' key is pressed
                        case ConsoleKey.LeftArrow:

                            // Skips required values to move
                            //one page to the left
                            if (skipNumber + displayNum > displayNum)
                                skipNumber -= displayNum;

                            // Updates the information displayed to the user
                            UpdatePage();
                            break;

                        // case the 'O' key is pressed
                        case ConsoleKey.O:
                            // Calls 'Order', passing a
                            // reference to namedTitles
                            Order(ref namedTitles);
                            break;

                        // case the 'R' key is pressed
                        case ConsoleKey.R:
                            ReverseOrder();
                            break;

                        // case the 'T' key is pressed
                        case ConsoleKey.T:
                            // Calls Method 'ResetTitles' to
                            // reset current results
                            ResetTitles();
                            break;

                        default:
                            // Updates the information displayed to the user
                            UpdatePage();
                            break;
                    }
                }
                
            }
            // Clears console
            Console.Clear();
        }

        /// <summary>
        /// Updates the search and order values
        /// </summary>
        private void UpdatePage()
        {
            // Calls method to resize window ro preset value
            UserInterface.ResizeWindow();

            // Calls method that displays the search results
            UserInterface.ShowResultsMenu(namedTitles.SkipLast(namedTitles.
                Count() - skipNumber - displayNum).Skip(skipNumber).
                Select(c => c).ToList(), orderParameterString, listState);
        }

        /// <summary>
        /// Reverses the order of the current list
        /// </summary>
        private void ReverseOrder()
        {
            // Checks if the list is already ordered
            if (listState != OrderState.Unordered)
            {
                // Switches between the two ordered 'OrderStates'
                listState = listState == OrderState.Descending ?
                    OrderState.Ascending : OrderState.Descending;

                // Reverses the order of the current list
                namedTitles = namedTitles.Reverse();
            }
            // Updates the information displayed to the user
            UpdatePage();
        }

        /// <summary>
        /// Resets the list of namedTitles to it's default state
        /// </summary>
        private void ResetTitles()
        {
            // Sets 'skipnumber' as '0' to go to first results page
            skipNumber = 0;

            // Copies the value of 'originalNamedTitles' to 'namedTitles
            namedTitles = originalNamedTitles;

            // Resets 'orderParameterString'
            orderParameterString = default;

            // Resets OrderState to 'Unordered'
            listState = OrderState.Unordered;

            // Updates the information displayed to the user
            UpdatePage();
        }

        /// <summary>
        ///  Orders the current list, according to user input
        /// </summary>
        /// <param name="namedTitles">reference to the titles
        /// list with the current changes</param>
        public void Order(ref IEnumerable<(TitleBasics titles, TitleRatings 
            ratings)> namedTitles)
        {
            // Call method 'ShowOrdermenu' of the 'UserInterface' class,
            // that displays the current list results and the Order menu
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

                    // Sets 'orderParameterString' as "Title"
                    orderParameterString = "Title";
                    break;
                    
                // case the 'D2' key is pressed
                case ConsoleKey.D2:

                    // Orders the list by the type
                    namedTitles = namedTitles
                        .OrderBy(c => c.titles.Type)
                        .Select(c => c).ToHashSet();

                    // Sets 'orderParameterString' as "Type"
                    orderParameterString = "Type";
                    break;

                // case the 'D3' key is pressed
                case ConsoleKey.D3:

                    // Orders the list by if it's adult or not
                    namedTitles = namedTitles
                        .OrderBy(c => c.titles.IsAdult)
                        .Select(c => c).ToHashSet();

                    // Sets 'orderParameterString' as "Age Restriction"
                    orderParameterString = "Age Restriction";
                    break;

                // case the 'D4' key is pressed
                case ConsoleKey.D4:

                    // Orders the list by the year of start
                    namedTitles = namedTitles
                        .OrderBy(c => c.titles.StartYear)
                        .Select(c => c).ToHashSet();

                    // Sets 'orderParameterString' as "Start Year"
                    orderParameterString = "Start Year";
                    break;

                // case the 'D5' key is pressed
                case ConsoleKey.D5:

                    // Orders the list by the year of end
                    namedTitles = namedTitles
                        .OrderBy(c => c.titles.EndYear)
                        .Select(c => c).ToHashSet();

                    // Sets 'orderParameterString' as "End Year"
                    orderParameterString = "End Year";
                    break;

                // case the 'D6' key is pressed
                case ConsoleKey.D6:

                    // Orders the list by the runtime
                    namedTitles = namedTitles
                        .OrderBy(c => c.titles.RuntimeMinutes)
                        .Select(c => c).ToHashSet();

                    // Sets 'orderParameterString' as "Runtime"
                    orderParameterString = "Runtime";
                    break;

                // case the 'D7' key is pressed
                case ConsoleKey.D7:

                    // Orders the list by the first genre
                    namedTitles = namedTitles
                        .OrderBy(c => c.titles.Genres[0])
                        .Select(c => c).ToHashSet();

                    // Sets 'orderParameterString' as "Genres"
                    orderParameterString = "Genres";
                    break;

                // case the 'D8' key is pressed
                case ConsoleKey.D8:
                    namedTitles = namedTitles

                        // Orders the list by the ratings
                        .OrderBy(c => c.ratings.AverageRating)
                        .Select(c => c).ToHashSet();

                    // Sets 'orderParameterString' as "Ratings"
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