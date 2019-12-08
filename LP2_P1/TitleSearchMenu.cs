using System;
using System.Collections.Generic;
using System.Text;

namespace LP2_P1
{
    // DEAD CODE, MAY BE REUSABLE
    static class TitleSearchMenu
    {

        public static string[] Menu()
        {
            string[] returnAnswer;

            Console.WriteLine("Search IMDb\n" +
                              "B. Go Back\n");


            returnAnswer = Console.ReadLine().Split(" ");

            return returnAnswer;
        }

        public static void GoBack()
        {

        }
    }
}
