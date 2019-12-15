using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace LP2_P1
{
    public static class UserInterface
    {
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
























        public static void ColorSetup(int cursorLeft = 0,
            ConsoleColor backgroundColor = ConsoleColor.White,
            ConsoleColor foregroundColor = ConsoleColor.Black)
        {
            Console.CursorLeft = cursorLeft;
            Console.BackgroundColor = backgroundColor;
            Console.ForegroundColor = foregroundColor;
        }

        public static void ClearSpace()
        {
            Console.CursorLeft -= 2;
            Console.Write(" ");
        }

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

        public static void ResizeWindow()
        {
            Console.SetWindowSize(Program.WindowWidth, Program.WindowHeight);
            Console.SetBufferSize(Program.WindowWidth, Program.WindowHeight);
        }
    }
}
