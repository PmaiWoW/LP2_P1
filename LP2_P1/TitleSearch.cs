using System;
using System.Collections.Generic;
using System.Linq;

namespace LP2_P1
{
    class TitleSearch
    {
        // ############### Everything here is still subject to change, it is still not to my liking #################
        // (Menu barely works, especially if the list is empty)

        private IEnumerable<TitleBasics> namedTitles;

        public void SearchTitle()
        {
            Console.WriteLine("Write the title of what you're looking for.\n");
            Console.BackgroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("                                             ");
            Console.CursorTop -= 1;

            Console.ForegroundColor = ConsoleColor.Black;
            string wantedTitle = Console.ReadLine();
            Console.ResetColor();

            namedTitles = FileLoader.LoadTitleBasics()
                .Where(c => c.OriginalTitle.ToLower()
                .Contains(wantedTitle.Trim().ToLower()))
                .Select(c => c).ToList();

            SearchSelected(namedTitles);
        }

        private void SearchSelected(IEnumerable<TitleBasics> titles)
        {
            int skipNumber = -30;
            string input = "n";

            IEnumerable<TitleBasics> n2;

            while (input.Trim().ToLower() == "n")
            {
                skipNumber += 30;

                n2 = titles.SkipLast((titles.Count() > 30) ? titles.Count() - (skipNumber + 30) : 0).Skip(skipNumber).Select(c => c).ToList();

                if (n2.Count() > 0)
                    PrintResults(n2);
                else
                    break;

                Console.WriteLine("\n press 'O' to order \n press 'B' to go back \n press 'N' for the next page \n");
                input = Console.ReadLine();

                switch (input.Trim().ToLower())
                {
                    case "o":
                        OrderTitles();
                        break;

                    case "n":
                        break;

                    case "b":
                        SearchTitle();
                        break;
                }
            }
        }

        private void PrintResults(IEnumerable<TitleBasics> titlesToDisplay)
        {
            Console.Clear();
            for (int i = 0; i < titlesToDisplay.Count(); i++)
            {
                Console.CursorLeft = 0;
                Console.Write($"{titlesToDisplay.ElementAt(i).OriginalTitle}");
                Console.CursorLeft = 100;
                Console.WriteLine($"|  {titlesToDisplay.ElementAt(i).TitleType}");
            }
        }

        private void OrderTitles()
        {
            SearchSelected(namedTitles.OrderBy(c => c.TitleType).ToList());
        }
    }
}