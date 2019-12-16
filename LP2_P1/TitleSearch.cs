using System;
using System.Collections.Generic;
using System.Linq;

namespace LP2_P1
{
    public class TitleSearch
    {
        // Creates two IEnumerables responsible for containing the information
        private IEnumerable<(TitleBasics titles, TitleRatings ratings)> originalNamedTitles;
        private IEnumerable<(TitleBasics titles, TitleRatings ratings)> namedTitles;

        // Creates variables
        private State listState = State.Unordered;
        private int skipNumber = 0;
        private int displayedAmount = 0;
        private const int displayNum = 30;

        public void SearchTitle(IEnumerable<(TitleBasics, TitleRatings)> wantedTitle)
        {
            // Gives originalNamedTitles the contents of the IEnumerable
            // Passed as argument
            originalNamedTitles = wantedTitle.ToHashSet();

            // Goes to SearchMenu method
            SearchMenu();
        }

        private void SearchMenu()
        {
            // Equals namedTitles to originalNamedTitles
            namedTitles = originalNamedTitles;

            // Creates a variable for storing user input
            ConsoleKey key;
            // Updates the information displayed to the user
            UpdatePage();

            // A while loop until the user presses 'b'
            do
            {
                // Sets the cursor to the position 1 of the screen
                Console.CursorLeft = 1;
                // Displays an arrow at the position of the cursor
                Console.Write(">");

                // Checks if the list contains any titles
                if (!namedTitles.Any())
                {
                    // Clears the console
                    Console.Clear();
                    // Displays a message to the user
                    Console.WriteLine("No titles found, returning to main " +
                        "menu...");
                    // Checks for any input from the user
                    Console.ReadKey(true);
                    // Returns to SearchMenu
                    return;
                }

                // Assigns the value of key the key the user pressed
                key = Console.ReadKey().Key;

                // Switch case depending on the key the user pressed
                switch (key)
                {
                    // if the user pressed the Up arrow
                    case ConsoleKey.UpArrow:
                        // Checks if the cursor is lesser than one on screen
                        if (Console.CursorTop > 1)
                        {
                            // Uses the UserInterface class to clear the arrow
                            UserInterface.ClearSpace();
                            // Moves the cursor one position up
                            Console.CursorTop -= 1;
                        }
                        break;

                    // if the user pressed the Down arrow
                    case ConsoleKey.DownArrow:
                        // Checks if the cursor is lower than the amount of
                        // titles currently displayed (mostly 30 but can be less)
                        if (Console.CursorTop < displayedAmount)
                        {
                            // Uses the UserInterface class to clear the arrow
                            UserInterface.ClearSpace();
                            // Moves the cursor one position down
                            Console.CursorTop += 1;
                        }
                        break;

                    // if the user pressed the right arrow
                    case ConsoleKey.RightArrow:
                        // Checks if the next page will have content if pressed
                        if (namedTitles.Count() / (skipNumber + displayNum) > 0 ||
                            skipNumber == 0)
                        {
                            // Adds 30 to the amount of titles the program
                            //should skip
                            skipNumber += displayNum;
                            // Updates the information displayed to the user
                            UpdatePage();
                        }
                        break;

                    // if the user pressed the left arrow
                    case ConsoleKey.LeftArrow:
                        // Checks if the previous page will have content
                        if (skipNumber + displayNum > displayNum)
                        {
                            // Subtracts 30 to the amount of titles the program
                            // should skip
                            skipNumber -= displayNum;
                            // Updates the information displayed to the user
                            UpdatePage();
                        }
                        break;

                    // if the user pressed 'O'
                    case ConsoleKey.O:
                        // Goes to the Sort method
                        Sort();
                        // Updates the information displayed to the user
                        UpdatePage();
                        break;

                    // if the user pressed 'R'
                    case ConsoleKey.R:
                        // Goes to the ReverseOrder method
                        ReverseOrder();
                        break;

                    // if the user pressed 'T'
                    case ConsoleKey.T:
                        // Resets namedTitles to the original state
                        namedTitles = originalNamedTitles;
                        // Sets the state of the list to unordered
                        listState = State.Unordered;
                        break;

                    // if the user pressed Enter
                    case ConsoleKey.Enter:
                        // Uses the static class TitleDetails to display to the
                        // user the contents of the selected titles
                        TitleDetails.Menu(
                            namedTitles.ElementAt(Console.CursorTop - 1 + skipNumber).titles,
                            namedTitles.ElementAt(Console.CursorTop - 1 + skipNumber).ratings);
                        // Updates the information displayed to the user
                        UpdatePage();
                        break;

                    // if the user pressed 'B'
                    case ConsoleKey.B:
                        //PrintBackToMenu();
                        break;

                    // if none of the options prints a message
                    default:
                        // Uses UserInterface to display the user that his 
                        // choice is invalid
                        UserInterface.PrintInvalidChoice();
                        // Updates the information displayed to the user
                        UpdatePage();
                        break;
                }
            } while (key != ConsoleKey.B);
            // Clears the console
            Console.Clear();
        }

        // Sorts searched items by Type
        private void UpdatePage()
        {
            // Clears the console
            Console.Clear();
            // Resets the windows and buffer size of the console
            UserInterface.ResizeWindow();

            // Splits namedTitles into the next 30 results and displays it
            PrintResults(namedTitles.Select(c => c.titles)
                .SkipLast(namedTitles.Count() - skipNumber - displayNum)
                .Skip(skipNumber).ToHashSet());
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
            // Updates the information displayed to the user
            UpdatePage();
        }

        private void PrintResults(IEnumerable<TitleBasics> titlesToDisplay)
        {
            // Sets the console color acording to the parameter given
            UserInterface.ColorSetup(backgroundColor: ConsoleColor.Gray);

            // Creates a gray bar at the top of the screen
            for (int i = 0; i < Console.WindowWidth; i++)
                Console.Write(" ");

            // Resets the windows and buffer size of the console
            Console.SetBufferSize(Program.WindowWidth, Program.WindowHeight);

            // Writes on the gray bar what the below parameters mean
            Console.CursorLeft = 0;
            Console.Write("    Name");
            Console.CursorLeft = 100;
            Console.Write("Type");
            Console.CursorLeft = 150;
            Console.Write($"State : {listState}");

            // Resets the console color
            Console.ResetColor();

            Console.WriteLine("");

            // Creates two variables
            string pTitle;
            int maxLenght = 90;

            // Equals the displayedAmount to the amount of results on screen
            displayedAmount = titlesToDisplay.Count();

            // Performs a loop for all the titles to be displayed
            for (int i = 0; i < titlesToDisplay.Count(); i++)
            {
                // Equals pTitle to the title of the current element
                pTitle = titlesToDisplay.ElementAt(i).PrimaryTitle;
                Console.CursorLeft = 0;
                // Displays the title on screen cutting when it reaches 90 
                // characters
                Console.Write($"   " +
                 $"{pTitle.Substring(0, Math.Min(pTitle.Length, maxLenght))}");
                Console.CursorLeft = 100;
                // Displays the type of the current element
                Console.WriteLine($"| {titlesToDisplay.ElementAt(i).Type}");
            }

            // Displays the options to the user
            Console.WriteLine("\n '->' for next page" +
                "\n '<-' for previous page \n" +
                "\n 'ENTER' to select title" +
                "\n 'O' to order " +
                "\n 'R' to reverse the order " +
                "\n 'T' to reset the order " +
                "\n 'B' to go back to previous menu");

            Console.CursorTop = 1;
        }

        public void Sort()
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
                // case nº1
                case ConsoleKey.D1:
                    // Orders the list by the type
                    namedTitles = namedTitles
                        .OrderBy(c => c.titles.Type)
                        .Select(c => c).ToHashSet();
                    break;

                // case nº2
                case ConsoleKey.D2:
                    // Orders the list by the title
                    namedTitles = namedTitles
                        .OrderBy(c => c.titles.PrimaryTitle)
                        .Select(c => c).ToHashSet();
                    break;

                // case nº3
                case ConsoleKey.D3:
                    // Orders the list by if it's adult or not
                    namedTitles = namedTitles
                        .OrderBy(c => c.titles.IsAdult)
                        .Select(c => c).ToHashSet();
                    break;

                // case nº4
                case ConsoleKey.D4:
                    // Orders the list by the year of start
                    namedTitles = namedTitles
                        .OrderBy(c => c.titles.StartYear)
                        .Select(c => c).ToHashSet();
                    break;
                // case nº5
                case ConsoleKey.D5:
                    // Orders the list by the year of end
                    namedTitles = namedTitles
                        .OrderBy(c => c.titles.EndYear)
                        .Select(c => c).ToHashSet();
                    break;
                // case nº6
                case ConsoleKey.D6:
                    namedTitles = namedTitles
                        .OrderBy(c => c.titles.Genres[0])
                        .Select(c => c).ToHashSet();
                    break;

                // case nº7
                case ConsoleKey.D7:
                    namedTitles = namedTitles
                        .OrderBy(c => c.ratings.AverageRating)
                        .Select(c => c).ToHashSet();
                    break;

                // case the 'B' key is pressed
                case ConsoleKey.B:
                    break;

                // If the input is unexpected
                default:
                    UserInterface.PrintInvalidChoice();
                    break;
            }
            // Makes sure the Garbage collector runs after ordering
            GC.Collect();
        }

        // A private enum with the possible states os the list
        private enum State { Ascending, Descending, Unordered };
    }
}