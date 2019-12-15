using System;
using System.Collections.Generic;
using System.Text;

namespace LP2_P1
{
    public static class UserInterface
    {
        public static void PrintTypeSelection(ICollection<TitleType> types,
            ICollection<TitleGenre?> genres, bool? isAdult)
        {
            string description1 = "  (- no filter | X adult | ' ' not adult)" +
                "\n\n";

            string description2 = "   Start Year - Insert 8 digits separated " +
                "by space, press enter to leave them empty, any unexpected " +
                "characters will reset to 'unfilled'";

            string description3 = "   End Year - Insert the minimum and " +
                "maximum runtime we should look for any unexpected " +
                "characters will reset to 'unfilled'\n\n";

            Console.SetCursorPosition(0, 5);
            for (int i = 0; i < 10; i++)
            {
                char a = types.Contains((TitleType)i) ? 'X' : ' ';
                Console.WriteLine($"   [{a}]{(TitleType)i}");
            }

            if (isAdult.HasValue == false)
                Console.Write("\n   [-]Adult Videos" + description1);
            else if (isAdult.Value == true)
                Console.Write("\n   [X]Adult Videos" + description1);
            else if (isAdult.Value == false)
                Console.Write("\n   [ ]Adult Videos" + description1);

            for (int i = 0; i < 2; i++)
            {
                Console.BackgroundColor = ConsoleColor.White;
                Console.CursorLeft = 3;
                Console.Write($"                      " +
                    $"                             ");
                Console.ResetColor();
                Console.Write(i == 1 ? description3 : description2 + " \n \n");
            }

            for (int i = 0; i < 28; i++)
            {
                char a = genres.Contains((TitleGenre)i) ? 'X' : ' ';
                Console.WriteLine($"   [{a}]{(TitleGenre)i}");
            }

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\n     ----------------");
            Console.WriteLine("    |     Search     |");
            Console.WriteLine("     ----------------");
            Console.ResetColor();
        }

        public static void PrintSearchBar()
        {
            Console.SetCursorPosition(3, 0);
            Console.WriteLine("Write the name of what you're looking for.\n");
            WriteOptions(3);
            Console.Write("                                                 ");
            Console.SetCursorPosition(2, 2);
            Console.ResetColor();
        }

        public static void WriteOptions(int cursorLeft)
        {
            Console.CursorLeft = cursorLeft;
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
        }

        public static void QuitMessage()
        {
            Console.Clear();
            Console.WriteLine("Thank you for using this searcher, we hope " +
                "to see you again!\nPress any key to exit.");
        }
    }
}
