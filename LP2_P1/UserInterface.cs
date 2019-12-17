using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace LP2_P1
{
    public static class UserInterface
    {
        // SearchMenu UI
        // --------------------------------------------------------------------
        public static void PrintSearchBar()
        {
            Console.SetCursorPosition(3, 0);
            Console.WriteLine("Write the name of what you're looking for.\n");
            ColorSetup(3);
            Console.Write("                                                 ");
            Console.SetCursorPosition(2, 2);
            Console.ResetColor();
        }

        public static void PrintTypeSelection(ICollection<TitleType> types,
            ICollection<TitleGenre?> genres, bool? isAdult)
        {
            string description1 = "  Adult Videos (- no filter | X adult |" +
                "' ' not adult)\n\n";

            string description2 = "   Start Year & End Year - Insert 2 " +
                "years, separated by a space"; 
            string description3 = "   Runtime low & Runtime high - Insert 2" +
                "numbers, separated by a space";
            string description4 = "   Lowest rating & Highest rating - " +
                "Insert 2 numbers, separated by space";

            Console.SetCursorPosition(0, 5);
            for (int i = 0; i < 10; i++)
            {
                char a = types.Contains((TitleType)i) ? 'X' : ' ';
                Console.WriteLine($"   [{a}]{(TitleType)i}");
            }

            char isAdultChar = '-';

            if (isAdult.HasValue == false) isAdultChar = '-';
            else if (isAdult == true) isAdultChar = 'X';
            else if (isAdult == false) isAdultChar = ' ';

            Console.Write($"\n   [{isAdultChar}] {description1}");

            for (int i = 0; i < 3; i++)
            {
                Console.BackgroundColor = ConsoleColor.White;
                Console.CursorLeft = 3;
                Console.Write($"         ");
                Console.ResetColor();
                if (i == 0) Console.Write(description2 + "\n\n");
                if (i == 1) Console.Write(description3 + "\n\n");
                if (i == 2) Console.Write(description4 + "\n\n");
            }

            for (int i = 0; i < 28; i++)
            {
                char a = genres.Contains((TitleGenre)i) ? 'X' : ' ';
                Console.WriteLine($"   [{a}]{(TitleGenre)i}");
            }

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\n     ----------------" +
                "\n    |     Search     |" +
                "\n     ----------------");
            Console.ResetColor();
        }

        // Search Results UI
        // -------------------------------------------------------------------
        public static void PrintResults(
            IEnumerable<(TitleBasics a, TitleRatings p)> titlesToDisplay,
            string sortParameterString, OrderState listState)
        {
            Console.Clear();
            // Sets color for first line
            ColorSetup(backgroundColor: ConsoleColor.Gray);

            // Assigns value to 'sortParameterString' to prevent errors
            if (sortParameterString == null) sortParameterString = "";

            // Initializes null strings that track how many space there are
            // until the end of that specific line, for printing purposes
            string fillAllLine = default;
            string fillPartialLine = default;

            // Supporting variable to get 'fillPartialLine' value
            int extraFill = listState.ToString().Length +
                sortParameterString.Length;

            // Checks how many spaces are between the last print and the end
            // of the console width, and add those blank spaces to variables
            for (int i = 7; i < Program.WindowWidth; i++) fillAllLine += " ";
            for (int i = 64 + extraFill; i < Program.WindowWidth; i++)
                fillPartialLine += " ";

            // Prints first line, checking if the list has been sorted to
            // add aditional information to the first line
            Console.WriteLine($"Name".PadLeft(6) +
                ((listState != OrderState.Unordered &&
                sortParameterString != "Title") ? ($"Sorting by: ".PadLeft(54) +
                sortParameterString) + $" ({listState})" +
                fillPartialLine : $"{fillAllLine}"));

            Console.ResetColor();

            // Supporting variables for optimized printing
            string pTitle;
            string pTitleDisplay;
            string sortParameterDisplay = default;
            int maxLenght = 50;

            // Loops through each title to display,
            // and prints required information
            for (int i = 0; i < titlesToDisplay.Count(); i++)
            {

                pTitle = $"{i + 1}: " +
                    $"{titlesToDisplay.ElementAt(i).a.PrimaryTitle}";

                pTitleDisplay = $"  " +
                 $"{pTitle.Substring(0, Math.Min(pTitle.Length, maxLenght))}" +
                 (titlesToDisplay.ElementAt(i).a.PrimaryTitle.Length > maxLenght
                 ? $"... " : "");

                switch (sortParameterString)
                {
                    case "Type":
                        sortParameterDisplay = "|" +
                            titlesToDisplay.ElementAt(i).a.Type.ToString();
                        break;
                    case "Age Restriction":
                        sortParameterDisplay = "|" +
                            (titlesToDisplay.ElementAt(i).a.IsAdult ?
                            "Adults Only" : "For Everyone"); break;
                    case "Start Year":
                        sortParameterDisplay = "|" +
                           (titlesToDisplay.ElementAt(i).a.StartYear.HasValue ?
                            titlesToDisplay.ElementAt(i).a.StartYear.ToString()
                            : @"\N"); break;
                    case "End Year":
                        sortParameterDisplay = "|" +
                            (titlesToDisplay.ElementAt(i).a.EndYear.HasValue ?
                            titlesToDisplay.ElementAt(i).a.EndYear.ToString()
                            : @"\N"); break;
                    case "Runtime":
                      sortParameterDisplay = "|" +
                      (titlesToDisplay.ElementAt(i).a.RuntimeMinutes.HasValue ?
                      titlesToDisplay.ElementAt(i).a.RuntimeMinutes.ToString().
                      PadRight(5) + "minutes" : @"\N"); break;
                    case "Genres":
                        sortParameterDisplay = "|" +
                          ((titlesToDisplay.ElementAt(i).a.Genres[0].HasValue ?
                           titlesToDisplay.ElementAt(i).a.Genres[0].ToString()
                           : @"\N") + ", ").PadRight(14) +
                          ((titlesToDisplay.ElementAt(i).a.Genres[1].HasValue ?
                           titlesToDisplay.ElementAt(i).a.Genres[1].ToString()
                           : @"\N") + ", ").PadRight(14) +
                           (titlesToDisplay.ElementAt(i).a.Genres[2].HasValue ?
                           titlesToDisplay.ElementAt(i).a.Genres[2].ToString()
                           : @"\N"); break;
                    case "Ratings":
                        sortParameterDisplay = "|" +
                            (titlesToDisplay.ElementAt(i).p.AverageRating.HasValue ?
                            titlesToDisplay.ElementAt(i).p.AverageRating.ToString()
                            : "No ratings"); break;
                    default:
                        break;
                }
                Console.WriteLine(pTitleDisplay.PadRight(59) +
                    sortParameterDisplay);
            }


        }
        // Menu UI
        // --------------------------------------------------------------------

        public static void ShowResultsMenu(
            IEnumerable<(TitleBasics titles, TitleRatings ratings)> titlesToDisplay,
            string sortParameterString, OrderState listState)
        {
            PrintResults(titlesToDisplay, sortParameterString, listState);

            Console.WriteLine("\n  '1-9' to select title" +
                "\n  '->' for next page" +
                "\n  '<-' for previous page" +
                "\n  'O' to order" +
                "\n  'R' to reverse the order" +
                "\n  'T' to reset the order" +
                "\n  'B' to go back to previous menu");
        }

        public static void ShowOrderMenu(
            IEnumerable<(TitleBasics titles, TitleRatings ratings)> titlesToDisplay,
            string sortParameterString, OrderState listState)
        {
            Console.Clear();

            PrintResults(titlesToDisplay, sortParameterString, listState);

            // Display Order Options
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

        public static void ShowTitleDetails(TitleBasics title)
        {
            Console.Clear();
            Console.WriteLine(title);
            Console.WriteLine("\n  Press 'B' to to back to previous menu");
        }

        // Messages
        // --------------------------------------------------------------------

        public static void NoResults()
        {
            Console.Clear();
            Console.WriteLine("No titles found, returning to main " +
                "menu...");
            Console.ReadKey(true);
        }
        // Currently not being used
        public static void PrintInvalidChoice()
        {
            Console.Clear();
            Console.WriteLine("Invalid option. Press any key to " +
                    "return to previous menu.");
            Console.ReadKey(true);
        }

        public static void NotImplemented()
        {
            Console.Clear();
            Console.WriteLine("This search functionality has not" +
                " been implemented yet.\nPress any key to return" +
                " to selection.");
            Console.ReadKey(true);
        }

        public static void QuitMessage()
        {
            Console.Clear();
            Console.WriteLine("Thank you for using this searcher, we hope " +
                "to see you again!\nPress any key to exit.");
        }

        // DisplayOptions
        // --------------------------------------------------------------------


        public static void ClearSpace()
        {
            Console.CursorLeft = 1;
            Console.Write(" ");
        }
        public static void ColorSetup(int cursorLeft = 0,
            ConsoleColor backgroundColor = ConsoleColor.White,
            ConsoleColor foregroundColor = ConsoleColor.Black)
        {
            Console.CursorLeft = cursorLeft;
            Console.BackgroundColor = backgroundColor;
            Console.ForegroundColor = foregroundColor;
        }

        public static void ResizeWindow()
        {
            Console.SetWindowSize(Program.WindowWidth, Program.WindowHeight);
            Console.SetBufferSize(Program.WindowWidth, Program.WindowHeight);
        }
    }
}
