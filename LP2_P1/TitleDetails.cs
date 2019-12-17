using System;

namespace LP2_P1
{
    public static class TitleDetails
    {
        public static void Menu(TitleBasics title, TitleRatings ratings)
        {
            // Crates a variable to hold the user input
            ConsoleKey key;

            // Stays on loop until the user presses 'B'
            do
            {
                // Displays to the user the details of the selected title
                Console.Clear();
                Console.WriteLine(title);
                Console.WriteLine(ratings);

                Console.CursorLeft = 1;
                Console.WriteLine("\nPress 'B' to to back to previous menu");

                // Checks for input from the user
                key = Console.ReadKey().Key;
            } while (key != ConsoleKey.B);
        }
    }
}