﻿using System;
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
                Console.Clear();
                Console.WriteLine(title);

                Console.CursorLeft = 1;
                Console.WriteLine("\nPress 'B' to to back to previous menu");

                key = Console.ReadKey().Key;
            }
        }
    }
}
