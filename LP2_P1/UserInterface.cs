using System;
using System.Collections.Generic;
using System.Linq;

namespace LP2_P1
{
    /// <summary>
    /// Responsible for the UI elements of the program
    /// </summary>
    public static class UserInterface
    {
        /// <summary>
        ///Displays a white bar and sets cursor position inside
        /// the bar in order to see the user input
        /// </summary>
        public static void PrintSearchBar()
        {
            // Sets cursor position to (3, 0)
            Console.SetCursorPosition(3, 0);
            // Asks for input
            Console.WriteLine("Write the name of what you're looking for.\n");
            // Sets cursor left to 3, background color
            // to white and text color to black
            ColorSetup(3);

            // Writes blank space to serve as searchbar
            Console.Write("                                                 ");

            // Sets cursor position to (2, 2)
            Console.SetCursorPosition(2, 2);

            // Resets colors
            Console.ResetColor();
        }


        /// <summary>
        /// Displays the type selection menu for the user to see and interact
        /// </summary>
        /// <param name="types"></param>
        /// <param name="genres"></param>
        /// <param name="isAdult"></param>
        public static void PrintTypeSelection(ICollection<TitleType> types,
            ICollection<TitleGenre?> genres, bool? isAdult)
        {
            // Sets what each string will display when they're called
            string description1 = "  Adult Videos (- no filter | X adult |" +
                "' ' not adult)\n\n";

            string description2 = "   Start Year & End Year - Insert 2 " +
                "years, separated by a space"; 
            string description3 = "   Runtime low & Runtime high - Insert 2" +
                "numbers, separated by a space";
            string description4 = "   Lowest rating & Highest rating - " +
                "Insert 2 numbers, separated by space";

            // Sets cursor position to (0, 5) 
            Console.SetCursorPosition(0, 5);

            // Loops through how many types there is
            for (int i = 0; i < 10; i++)
            {
                // Displays the checkox and name of each type
                char a = types.Contains((TitleType)i) ? 'X' : ' ';
                Console.WriteLine($"   [{a}]{(TitleType)i}");
            }

            // Creates new char variable to use as comparer
            char isAdultChar = '-';

            // Defines which char is displayed based on 'isAdult' value
            if (isAdult.HasValue == false) isAdultChar = '-';
            else if (isAdult == true) isAdultChar = 'X';
            else if (isAdult == false) isAdultChar = ' ';

            // Displays 'isAdult' value
            Console.Write($"\n   [{isAdultChar}] {description1}");

            // Displays search bars for startYear/endYear,
            // runtime interval and ratings interval
            for (int i = 0; i < 3; i++)
            {
                // Sets background color as white
                Console.BackgroundColor = ConsoleColor.White;

                // Sets cursor left as 3
                Console.CursorLeft = 3;

                // Writes blank space
                Console.Write($"         ");

                // Resets colors
                Console.ResetColor();

                // uses description 2
                if (i == 0) Console.Write(description2 + "\n\n");

                // uses description 2
                if (i == 1) Console.Write(description3 + "\n\n");

                // uses description 2
                if (i == 2) Console.Write(description4 + "\n\n");
            }

            // Loops through how many genres there is
            for (int i = 0; i < 28; i++)
            {
                //Displays the checkox and name of every possible genre
                char a = genres.Contains((TitleGenre)i) ? 'X' : ' ';
                Console.WriteLine($"   [{a}]{(TitleGenre)i}");
            }

            // Sets text color to 'ConsoleColor.Cyan' 
            Console.ForegroundColor = ConsoleColor.Cyan;

            // Displays search box
            Console.WriteLine("\n     ----------------" +
                "\n    |     Search     |" +
                "\n     ----------------");

            // Resets colors
            Console.ResetColor();
        }

        /// <summary>
        /// Displays 9 results of the titlesList and correspondent value of
        /// the ordered parameter (if it's being ordered)
        /// </summary>
        /// <param name="titlesToDisplay">titles to be displayed</param>
        /// <param name="orderParameterString">string that tells us which
        /// parameter is being ordered</param>
        /// <param name="listState">state of the order</param>
        public static void PrintResults(
            IEnumerable<(TitleBasics a, TitleRatings p)> titlesToDisplay,
            string orderParameterString, OrderState listState)
        {
            // Clears console
            Console.Clear();
            // Sets color for first line
            ColorSetup(backgroundColor: ConsoleColor.Gray);

            // Assigns value to 'orderParameterString' to prevent errors
            if (orderParameterString == null) orderParameterString = "";

            // Initializes null strings that track how many space there are
            // until the end of that specific line, for printing purposes
            string fillAllLine = default;
            string fillPartialLine = default;

            // Supporting variable to get 'fillPartialLine' value
            int extraFill = listState.ToString().Length +
                orderParameterString.Length;


            // Loops through how many spaces are left to the end of the 
            // console and fills that space with blank string with that size
            for (int i = 7; i < Console.WindowWidth; i++) fillAllLine += " ";

            //Loops through how many spaces are left to the end of the console
            for (int i = 64 + extraFill; i < Console.WindowWidth; i++)
                //fills that space with blank string with that size
                fillPartialLine += " ";

            // Displays first row, that indicates where the name is,
            // and displays what variable is being ordered, followed by 
            // 
            Console.WriteLine($"Name".PadLeft(6) +
                ((listState != OrderState.Unordered && orderParameterString !=
                "Title") ? ($"Sorting by: ".PadLeft(54) + orderParameterString) 
                + $" ({listState})" + fillPartialLine : $"{fillAllLine}"));

            // Resets console color
            Console.ResetColor();

            // Supporting variables for optimized printing
            string pTitle;
            string pTitleDisplay;
            string orderParameterDisplay = default;
            int maxLenght = 50;

            // Loops through each title to display,
            // and prints required information
            for (int i = 0; i < titlesToDisplay.Count(); i++)
            {
                // Set 'pTitle' string as the option number 
                // plus the primary title
                pTitle = $"{i + 1}: " +
                    $"{titlesToDisplay.ElementAt(i).a.PrimaryTitle}";

                // Restrain string size so it's not bigger than the
                // predefined space
                pTitleDisplay = $"  " +
                 $"{pTitle.Substring(0, Math.Min(pTitle.Length, maxLenght))}" +
                 (titlesToDisplay.ElementAt(i).a.PrimaryTitle.Length > 
                 maxLenght ? $"... " : "");

                // Checks what is the current order parameter and displays
                // that specific property for every title being displayed
                switch (orderParameterString)
                {
                    // Checks if it's ordering by type
                    case "Type":
                        orderParameterDisplay = "|" +
                            titlesToDisplay.ElementAt(i).a.Type.ToString();
                        break;
                    // Checks if it's ordering by adult rating
                    case "Age Restriction":
                        orderParameterDisplay = "|" +
                            (titlesToDisplay.ElementAt(i).a.IsAdult ?
                            "Adults Only" : "For Everyone"); break;
                    // Checks if it's ordering by start year
                    case "Start Year":
                        orderParameterDisplay = "|" +
                           (titlesToDisplay.ElementAt(i).a.StartYear.HasValue ?
                            titlesToDisplay.ElementAt(i).a.StartYear.ToString()
                            : @"\N"); break;
                    // Checks if it's ordering by end year
                    case "End Year":
                        orderParameterDisplay = "|" +
                            (titlesToDisplay.ElementAt(i).a.EndYear.HasValue ?
                            titlesToDisplay.ElementAt(i).a.EndYear.ToString()
                            : @"\N"); break;
                    // Checks if it's ordering by runtime
                    case "Runtime":
                      orderParameterDisplay = "|" +
                      (titlesToDisplay.ElementAt(i).a.RuntimeMinutes.HasValue ?
                      titlesToDisplay.ElementAt(i).a.RuntimeMinutes.ToString().
                      PadRight(5) + "minutes" : @"\N"); break;
                    // Checks if it's ordering by genres
                    case "Genres":
                        orderParameterDisplay = "|" +
                          ((titlesToDisplay.ElementAt(i).a.Genres[0].HasValue ?
                           titlesToDisplay.ElementAt(i).a.Genres[0].ToString()
                           : @"\N") + ", ").PadRight(14) +
                          ((titlesToDisplay.ElementAt(i).a.Genres[1].HasValue ?
                           titlesToDisplay.ElementAt(i).a.Genres[1].ToString()
                           : @"\N") + ", ").PadRight(14) +
                           (titlesToDisplay.ElementAt(i).a.Genres[2].HasValue ?
                           titlesToDisplay.ElementAt(i).a.Genres[2].ToString()
                           : @"\N"); break;
                    // Checks if it's ordering by ratings
                    case "Ratings":
                        orderParameterDisplay = "|" +
                            (titlesToDisplay.ElementAt(i).p.AverageRating.
                            HasValue ?
                            titlesToDisplay.ElementAt(i).p.AverageRating.
                            ToString() : "No ratings"); break;
                    default:
                        break;
                }
                // Joins the two strings and display the entire line 
                Console.WriteLine(pTitleDisplay.PadRight(59) +
                    orderParameterDisplay);
            }
        }

        /// <summary>
        /// Displays the Title Search menu 
        /// </summary>
        /// <param name="titlesToDisplay">titles to be displayed</param>
        /// <param name="orderParameterString">string that tells us which
        /// parameter is being ordered</param>
        /// <param name="listState">state of the order</param>
        public static void ShowResultsMenu(
            IEnumerable<(TitleBasics titles, TitleRatings ratings)> 
            titlesToDisplay, string orderParameterString, OrderState listState)
        {
            // Clears console
            Console.Clear();
            // Calls method that prints every line of the searchResults
            PrintResults(titlesToDisplay, orderParameterString, listState);

            // Displays every possible input option
            Console.WriteLine("\n  '1-9' to select title" +
                "\n  '->' for next page" +
                "\n  '<-' for previous page" +
                "\n  'O' to order" +
                "\n  'R' to reverse the order" +
                "\n  'T' to reset the order" +
                "\n  'B' to go back to previous menu");
        }

        /// <summary>
        /// Displays the Order menu 
        /// </summary>
        /// <param name="titlesToDisplay">titles to be displayed</param>
        /// <param name="orderParameterString">string that tells us which
        /// parameter is being ordered</param>
        /// <param name="listState">state of the order</param>
        public static void ShowOrderMenu(
            IEnumerable<(TitleBasics titles, TitleRatings ratings)> 
            titlesToDisplay, string orderParameterString, OrderState listState)
        {
            // Clears console
            Console.Clear();

            // Calls method that prints every line of the searchResults
            PrintResults(titlesToDisplay, orderParameterString, listState);

            // Displays every possible input option
            Console.WriteLine("\n  '1' to order by title" +
                "\n  '2' to order by type" +
                "\n  '3' to order by adult rating" +
                "\n  '4' to order by year of release" +
                "\n  '5' to order by year of end" +
                "\n  '6' to order by runtime" +
                "\n  '7' to order by genre" +
                "\n  '8' to order by rating" +
                "\n  'B' to go back \n");
        }

        /// <summary>
        /// Displays every title parameter information for the selected title
        /// </summary>
        /// <param name="title">'TitleBasics' item to be analyzed</param>
        /// <param name="ratings">'Titleratings' item to be analyzed</param>
        public static void ShowDetailsMenu(TitleBasics title,
            TitleRatings ratings)
        {
            // Crates a variable to hold the user input
            ConsoleKey key;

            // Begins Loop
            do
            {
                // Calls method that displays 'TitleBasics' information
                ShowTitleDetails(title);
                // Calls method that displays 'TitleRatings' information
                ShowTitleRatings(ratings);
                // Reads user input and store the value in the 'key' variable
                key = Console.ReadKey().Key;

              // Stays on loop until the user presses 'B'
            } while (key != ConsoleKey.B);
        }

        /// <summary>
        /// Converts all TitleBasics properties to one string,
        /// that is displayed in 'ShowDetailsMenu'
        /// </summary>
        /// <param name="title">'TitleBasics' item to be analyzed</param>
        public static void ShowTitleDetails(TitleBasics title)
        {
            // Clears console
            Console.Clear();

            // Sets value of 'typePrint based on the titles type
            string typePrint =
                title.Type.HasValue ? title.Type.ToString() : @"\N";

            // Sets value of 'startYearPrint' based on the titles start year
            string startYearPrint =
                title.StartYear.HasValue ? title.StartYear.ToString() : @"\N";

            // Sets value of 'endYearPrint' based on the titles end year
            string endYearPrint =
                title.EndYear.HasValue ? title.EndYear.ToString() : @"\N";

            // Sets value of 'runtimePrint' based on the titles runtime minutes
            string runtimePrint = title.RuntimeMinutes.HasValue ?
                title.RuntimeMinutes.ToString() : @"\N";

            // Sets value of 'isAdult' based on the titles type
            string isAdult = title.IsAdult ? "Adults Only" : "For Everyone";

            // Sets value of 'genrresPrint' based on the titles type
            string[] genresPrint = new string[3];
            for (int i = 0; i < 3; i++)
                genresPrint[i] = title.Genres[i].HasValue ?
                    title.Genres[i].ToString() : @"\N";

            // Displays every title variables value
            Console.WriteLine($"\n  Title Name:      {title.PrimaryTitle}" +
                $"\n  Original Title:  {title.OriginalTitle}" +
                $"\n  Type:            {typePrint}" +
                $"\n  Age Restriction: {isAdult}" +
                $"\n  Release Year:    {startYearPrint}" +
                $"\n  Ending Year:     {endYearPrint}" +
                $"\n  Runtime (Mins):  {runtimePrint} " +
                $"\n  Genres:          {genresPrint[0]}" +
                $"\n                   {genresPrint[1]}" +
                $"\n                   {genresPrint[2]}");
        }

        /// <summary>
        /// Converts all TitleRatings properties to one string,
        /// that is displayed in 'ShowDetailsMenu'
        /// </summary>
        /// <param name="ratings">'TitleRatings' item to be analyzed</param>
        public static void ShowTitleRatings(TitleRatings ratings)
        {
            // Checks if the title in question has ratings
            if (!ratings.AverageRating.HasValue && !ratings.NumVotes.HasValue)
            {
                // Displays specific message if it has no ratings
                Console.WriteLine("This title has no ratings" + 
                    $"\n  Press 'B' to to back to previous menu");
            }
            // If title in question has ratings
            else
            {
                // Displays average rating and how many votes were recorded
                Console.WriteLine($"\n   Average Ratings: " +
                    $"{ratings.AverageRating} in {ratings.NumVotes} votes");
            }
        }

        /// <summary>
        /// Displays message for when the program finds no results for a search
        /// </summary>
        public static void NoResults()
        {
            // Clears console
            Console.Clear();

            //Displays no title found message
            Console.WriteLine("No titles found, returning to main " +
                "menu...");

            // Checks is any key has been pressed
            Console.ReadKey(true);
        }

        /// <summary>
        /// Displays message for when the program accesses a
        /// functionality that is yet to be implemented
        /// </summary>
        public static void NotImplemented()
        {
            // Clears console
            Console.Clear();

            //Displays functionality not implemented message
            Console.WriteLine("This search functionality has not" +
                " been implemented yet.\nPress any key to return" +
                " to selection.");

            // Checks is any key has been pressed
            Console.ReadKey(true);
        }

        /// <summary>
        /// Displays message for when the user quits the program
        /// </summary>
        public static void QuitMessage()
        {
            // Clears console
            Console.Clear();

            // Display goodbye message
            Console.WriteLine("Thank you for using this searcher, we hope " +
                "to see you again!\nPress any key to exit.");
        }

        /// <summary>
        /// Clears the space where the cursor moves in the 'SearchMenu' class
        /// </summary>
        public static void ClearSpace()
        {
            // Sets cursor's left value to 1
            Console.CursorLeft = 1;

            // Prints blank space to erase what was there before
            Console.Write(" ");
        }


        /// <summary>
        /// Sets new position for the cursor and/or
        /// set a specific color for background and/or foreground
        /// </summary>
        /// <param name="cursorLeft">Defines on what column
        /// the cursor will be</param>
        /// <param name="backgroundColor">Defines background color</param>
        /// <param name="foregroundColor">Defines text color</param>
        public static void ColorSetup(int cursorLeft = 0,
            ConsoleColor backgroundColor = ConsoleColor.White,
            ConsoleColor foregroundColor = ConsoleColor.Black)
        {
            // Sets new cursor left value given
            Console.CursorLeft = cursorLeft;

            // Sets background color to value given
            Console.BackgroundColor = backgroundColor;

            // Sets text color to value given
            Console.ForegroundColor = foregroundColor;
        }

        /// <summary>
        /// Resets the window size and buffer size to the preset values
        /// </summary>
        public static void ResizeWindow()
        {
            Console.SetWindowSize(Program.WindowWidth, Program.WindowHeight);
            Console.SetBufferSize(Program.WindowWidth, Program.WindowHeight);
        }
    }
}
