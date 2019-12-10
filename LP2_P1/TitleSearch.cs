using System;
using System.Collections.Generic;
using System.Linq;

namespace LP2_P1
{
    public class TitleSearch
    {
        private IEnumerable<TitleBasics> namedTitles;
        private State listState = State.Unordered;
        private int skipNumber = 30;
        private int displayedAmount = 0;

        public void SearchTitle(string wantedTitle)
        {
            namedTitles = FileLoader.LoadTitleBasics()
                .Where(c => c.PrimaryTitle.ToLower()
                .Contains(wantedTitle.Trim().ToLower()))
                .Select(c => c).ToList();

            SearchMenu();
        }

        private void SearchMenu()
        {
            ConsoleKey key = ConsoleKey.L;

            if(!namedTitles.Any(c => c.TConst != null))
            {
                Console.Clear();
                Console.WriteLine("No titles found, press any key to return" +
                    "to the main menu...");
                Console.Read();
                return;
            }
            UpdatePage();

            while (key != ConsoleKey.B)
            {
                key = ConsoleKey.D0;
                Console.CursorLeft = 1;
                Console.Write(">");

                key = Console.ReadKey().Key;

                switch (key)
                {
                    case ConsoleKey.RightArrow:
                        if (namedTitles.Count() / skipNumber > 0)
                        {
                            skipNumber += 30;
                            UpdatePage();
                        }
                        break;

                    case ConsoleKey.LeftArrow:
                        if (skipNumber > 30)
                        {
                            skipNumber -= 30;
                            UpdatePage();
                        }
                        break;

                    case ConsoleKey.UpArrow:
                        if (Console.CursorTop > 1)
                        {
                            Console.CursorLeft -= 2;
                            Console.Write(" ");
                            Console.CursorTop -= 1;
                        }
                        break;

                    case ConsoleKey.DownArrow:
                        if (Console.CursorTop < displayedAmount)
                        {
                            Console.CursorLeft -= 2;
                            Console.Write(" ");
                            Console.CursorTop += 1;
                        }
                        break;

                    case ConsoleKey.O:
                        Sorter.Sort(ref namedTitles);
                        UpdatePage();
                        break;

                    case ConsoleKey.R:
                        ReverseOrder();
                        break;

                    case ConsoleKey.F:
                        Filter();
                        break;

                    case ConsoleKey.Enter:
                        TitleDetails.DisplayInfo(
                            namedTitles.ElementAt(Console.CursorTop - 1));
                        UpdatePage();
                        UpdatePage();
                        break;

                    case ConsoleKey.B:
                        Console.Clear();
                        Console.WriteLine("Going back to the previous menu." +
                            "\nPress any key to continue.");
                        Console.ReadKey();
                        break;

                    default:
                        Console.Clear();
                        Console.WriteLine("Invalid option. Press any key to " +
                            "return to selection.");
                        Console.ReadKey();
                        UpdatePage();
                        break;
                }
            }
            Console.Clear();
        }

        // Sorts searched items by Type
        private void UpdatePage()
        {
            PrintResults(namedTitles.SkipLast(namedTitles.Count() - skipNumber)
                .Skip(skipNumber - 30).Select(c => c)
                .ToList());
        }

        private void ReverseOrder()
        {
            if (listState != State.Unordered)
                listState = listState == State.Descending ?
                    State.Ascending : State.Descending;

            namedTitles = namedTitles.Reverse();
            UpdatePage();
        }

        private void Filter()
        {
            Console.Clear();
            ConsoleKey key = ConsoleKey.D0;
            Console.WriteLine("Select the intended filter:\n" +
                "\n'1' to filter by a type" +
                "\n'2' to filter by age restriction" +
                "\n'3' to filter by a release year" +
                "\n'4' to filter by a end year" +
                "\n'5' to filter by a genre");

            key = Console.ReadKey().Key;

            switch (key)
            {
                case ConsoleKey.D2:
                    IsAdultFilter();
                    break;
                
                case ConsoleKey.D3:
                    StartYearFilter();
                    break;
                
                case ConsoleKey.D4:
                    EndYearFilter();
                    break;
            }

        }

        private void IsAdultFilter()
        {
            bool? adultsOnly = null;

            ConsoleKey key;

            Console.Clear();
            Console.WriteLine("Select the age restriction:\n" +
                "\n'1' Adults Only" +
                "\n'2' For Everyone");

            key = Console.ReadKey().Key;

            switch (key)
            {
                case ConsoleKey.D1:
                    adultsOnly = true;
                    break;
                
                case ConsoleKey.D2:
                    adultsOnly = false;
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine("Invalid option. Press any key to " +
                        "return to selection.");
                    return;
            }
            namedTitles = namedTitles
                .Where(c => c.IsAdult == adultsOnly)
                .Select(c => c).ToList();

            UpdatePage();
        }
        
        private void StartYearFilter()
        {
            int startYear;
            string yearString;
            Console.Clear();
            Console.WriteLine("Type the release year:\n");

            yearString = Console.ReadLine();

            if (!int.TryParse(yearString, out startYear))
            {
                Console.Clear();
                Console.WriteLine("Invalid option. Press any key to " +
                    "return to selection.");
                return;
            }
            else int.TryParse(yearString, out startYear);

            namedTitles = namedTitles
                .Where(c => c.StartYear == startYear)
                .Select(c => c).ToList();

            UpdatePage();
        }
        
        private void EndYearFilter()
        {
            int endYear;
            string yearString;
            Console.Clear();
            Console.WriteLine("Type the end year:\n");

            yearString = Console.ReadLine();

            if (!int.TryParse(yearString, out endYear))
            {
                Console.Clear();
                Console.WriteLine("Invalid option. Press any key to " +
                    "return to selection.");
                return;
            }
            else int.TryParse(yearString, out endYear);


            namedTitles = namedTitles
                .Where(c => c.EndYear == endYear)
                .Select(c => c).ToList();

            UpdatePage();
        }

        private void PrintResults(IEnumerable<TitleBasics> titlesToDisplay)
        {
            Console.Clear();

            Console.BackgroundColor = ConsoleColor.Gray;
            Console.ForegroundColor = ConsoleColor.Black;

            for (int i = 0; i < Console.WindowWidth; i++)
                Console.Write(" ");

            Console.CursorLeft = 0;
            Console.Write("    Name");
            Console.CursorLeft = 100;
            Console.Write("Type");
            Console.CursorLeft = 150;
            Console.Write($"State : {listState}");

            Console.ResetColor();

            Console.WriteLine("");

            displayedAmount = titlesToDisplay.Count();

            for (int i = 0; i < titlesToDisplay.Count(); i++)
            {
                Console.CursorLeft = 0;
                Console.Write($"   {titlesToDisplay.ElementAt(i).PrimaryTitle}");
                Console.CursorLeft = 100;
                Console.WriteLine($"| {titlesToDisplay.ElementAt(i).Type}");
            }

            Console.WriteLine("\n 'O' to order " +
                "\n 'R' to reverse the order " +
                "\n 'B' to go back to previous menu" +
                "\n 'ENTER' to select title" +
                "\n '->' for previous page" +
                "\n '<-' for next page \n");

            Console.CursorTop = 1;
        }

        private enum State { Ascending, Descending, Unordered };
    }
}