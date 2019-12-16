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

            Console.BackgroundColor = ConsoleColor.White;
            Console.CursorLeft = 3;
            Console.Write($"         ");
            Console.ResetColor();
            Console.Write($"{description2}\n\n");

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
            IEnumerable<TitleBasics> titlesToDisplay,
            string sortParameterString, TitleSearch.SortState listState)
        {
            UserInterface.ColorSetup(backgroundColor: ConsoleColor.Gray);

            for (int i = 0; i < Console.WindowWidth; i++)
                Console.Write(" ");

            Console.SetBufferSize(Program.WindowWidth, Program.WindowHeight);

            Console.Write("    Name");
            Console.CursorLeft = 100;
            Console.Write(sortParameterString);
            Console.CursorLeft = 150;
            if (listState != TitleSearch.SortState.Unordered)
                Console.Write($"State : {listState}");

            Console.ResetColor();

            Console.WriteLine("");

            string pTitle;
            int maxLenght = 90;

            for (int i = 0; i < titlesToDisplay.Count(); i++)
            {
                pTitle = $"{i + 1}: {titlesToDisplay.ElementAt(i).PrimaryTitle}";
                Console.CursorLeft = 0;
                Console.Write($"   " +
                 $"{pTitle.Substring(0, Math.Min(pTitle.Length, maxLenght))}");

                Console.CursorLeft = 100;

                switch (sortParameterString)
                {
                    case "Type":
                        Console.WriteLine($"|" +
                            $"{titlesToDisplay.ElementAt(i).Type}");
                        break;
                    case "PrimaryTitle":
                        Console.WriteLine($"|" +
                            $"{titlesToDisplay.ElementAt(i).PrimaryTitle}");
                        break;
                    case "IsAdult":
                        Console.WriteLine($"|" +
                            $"{titlesToDisplay.ElementAt(i).IsAdult}");
                        break;
                    case "StartYear":
                        Console.WriteLine($"|" +
                            $"{titlesToDisplay.ElementAt(i).StartYear}");
                        break;
                    case "EndYear":
                        Console.WriteLine($"|" +
                            $"{titlesToDisplay.ElementAt(i).EndYear}");
                        break;
                    case "Genres":
                        Console.WriteLine($"|" +
                            $"{titlesToDisplay.ElementAt(i).Genres[0]}, " +
                            $"{titlesToDisplay.ElementAt(i).Genres[1]}, " +
                            $"{titlesToDisplay.ElementAt(i).Genres[2]}");
                        break;

                    default:
                        break;
                }
            }

            Console.WriteLine("\n '->' for next page" +
                "\n '<-' for previous page" +
                "\n '1-9' to select title" +
                "\n 'O' to order" +
                "\n 'R' to reverse the order" +
                "\n 'T' to reset the order" +
                "\n 'B' to go back to previous menu");

            Console.CursorTop = 1;
        }
        // OrderMenu UI
        // --------------------------------------------------------------------

        public static void OrderMenu()
        {
            Console.Clear();

            // Display Order Options
            Console.WriteLine("\n '1' to order by type" +
                "\n '2' to order by title" +
                "\n '3' to order by adult rating" +
                "\n '4' to order by year of release" +
                "\n '5' to order by year of end" +
                "\n '6' to order by genre" +
                "\n '7' to order by rating" +
                "\n 'B' to go back \n");
        }

        // Messages
        // --------------------------------------------------------------------

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
