using System;
using System.Collections.Generic;
using System.Text;

namespace LP2_P1
{
    static class TitleDetails
    {
        public static void Menu(TitleBasics title)
        {
            ConsoleKey key = ConsoleKey.D0;

            while (key != ConsoleKey.B)
            {
                UserInterface.ShowTitleDetails(title);
                key = Console.ReadKey().Key;
            }
        }
    }
}
