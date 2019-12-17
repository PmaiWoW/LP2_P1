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
                UserInterface.ShowTitleDetails(title);
                key = Console.ReadKey().Key;
            } while (key != ConsoleKey.B);
        }
    }
}