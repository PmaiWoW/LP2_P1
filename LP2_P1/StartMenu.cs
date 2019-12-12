using System;
using System.Collections.Generic;
using System.Linq;

namespace LP2_P1
{
    public static class StartMenu
    {
        private static string wantedTitle;
        private static List<TitleType> types = new List<TitleType>(); 
        private static bool? isAdult;
        private static int? start;
        private static int? end;

        public static void MenuLoop()
        {
            ConsoleKey key;

            do
            {
                Console.Clear();
                Console.WriteLine("1. Search Title\n" +
                                  "2. Search People\n" +
                                  "Q. Quit\n");

                key = Console.ReadKey().Key;

                switch (key)
                {
                    case ConsoleKey.D1:
                        SubMenu();
                        break;

                    case ConsoleKey.D2:
                        Console.Clear();
                        Console.WriteLine("This search functionality has not" +
                            " been implemented yet.\nPress any key to return" +
                            " to selection.");
                        Console.ReadKey();
                        break;

                    case ConsoleKey.Q:
                        Quit();
                        break;

                    default:
                        Console.Clear();
                        Console.WriteLine("Invalid option. Press any key to " +
                            "return to selection.");
                        Console.ReadKey();
                        break;
                }
            } while (key != ConsoleKey.Q);
        }

        private static void Titles(string wantedTitle, TitleType[] type,
            bool? adult, int? startDate, int? endDate,
            TitleGenre?[] genres = null, int runtime1 = 0, int runtime2 = 3000)
        {
            IEnumerable<TitleBasics> originalNamedTitles = FileLoader.LoadTitleBasics()
                .Where(c => c.PrimaryTitle.ToLower()
                .Contains(wantedTitle.Trim().ToLower()))
                .Where(c => c.RuntimeMinutes >= runtime1 && c.RuntimeMinutes <= runtime2)
                .Select(c => c);

            if (startDate != null)
                originalNamedTitles = originalNamedTitles.Where
                    (c => c.StartYear >= startDate);
            if (endDate != null)
                originalNamedTitles = originalNamedTitles.Where
                    (c => c.EndYear <= endDate);
            if (type != null)
                originalNamedTitles = originalNamedTitles.Where
                    (c => type.Any(a => a == c.Type));
            if (adult != null)
                originalNamedTitles = originalNamedTitles.Where
                    (c => c.IsAdult == adult);
            if (genres != null)
                originalNamedTitles = originalNamedTitles.Where
                    (c => c.Genres == genres);

            TitleSearch searcher = new TitleSearch();
            searcher.SearchTitle(originalNamedTitles);
        }

        private static void SubMenu()
        {
            Console.Clear();

            ConsoleKey key = ConsoleKey.D0;

            PrintSearchBar();
            PrintTypeSelection();

            Console.CursorTop = 2;

            while (key != ConsoleKey.Q)
            {
                Console.CursorLeft = 1;
                Console.Write(">");

                key = Console.ReadKey().Key;

                if (key == ConsoleKey.UpArrow && Console.CursorTop > 2)
                {
                    Program.ClearSpace();
                    Console.CursorTop -= 1;
                }
                else if (key == ConsoleKey.DownArrow && Console.CursorTop < 20)
                {
                    Program.ClearSpace();
                    Console.CursorTop += 1;
                }
                else if (key == ConsoleKey.Enter)
                {
                    switch (Console.CursorTop)
                    {
                        case 2:
                            PrintSearchBar();
                            Console.CursorLeft = 4;
                            Console.BackgroundColor = ConsoleColor.White;
                            Console.ForegroundColor = ConsoleColor.Black;
                            wantedTitle = Console.ReadLine();
                            Console.ResetColor();
                            break;

                        case 5:case 6:case 7:case 8:case 9:
                        case 10: case 11:case 12:case 13:case 14:
                            int index = Console.CursorTop - 5;
                            if (types.Contains((TitleType)index))
                                types.Remove((TitleType)index);
                            else
                                types.Add((TitleType)index);
                            PrintTypeSelection();
                            Console.CursorTop = index + 5;
                            break;

                        case 15:
                            if (isAdult == true) isAdult = false;
                            else if (isAdult == null) isAdult = true;
                            else if (isAdult == false) isAdult = null;
                            PrintTypeSelection();
                            break;

                        case 17:
                            string[] date = Console.ReadLine().Split(' ');

                            if (date.Length >= 1 && date[0].Length == 4 && int.Parse(date[0]) != 0)
                                start = int.Parse(date[0]);
                            else
                                start = null;

                            if (date.Length == 2 && date[1].Length == 4 && int.Parse(date[1]) != 0)
                                end = int.Parse(date[1]);
                            else
                                end = null;
                            break;

                        case 20:
                            Titles(wantedTitle, types.ToArray(), isAdult, start, end);
                            break;
                    }
                }
            }
        }

        public static void Quit()
        {
            Console.Clear();
            Console.WriteLine("Thank you for using this searcher, we hope " +
                "to see you again!\nPress any key to exit.");
            Console.ReadKey();
            Environment.Exit(0);
        }

        private static void PrintTypeSelection()
        {
            Console.SetCursorPosition(0, 5);
            for (int i = 0; i < 9; i++)
            {
                char a = types.Contains((TitleType)i) ? 'X' : ' ';
                Console.WriteLine($"   [{a}]{(TitleType)i}");
            }

            Console.WriteLine("      (- no filter | X adult | ' ' not adult)");
            if (isAdult.HasValue == false)
                Console.Write("   [-]Adult Videos");
            else if (isAdult.Value == true)
                Console.Write("   [X]Adult Videos");
            else if (isAdult.Value == false)
                Console.Write("   [ ]Adult Videos");
        }

        private static void PrintSearchBar()
        {
            Console.SetCursorPosition(3, 0);
            Console.WriteLine("Write the name of what you're looking for.\n");
            Console.CursorLeft = 3;
            Console.BackgroundColor = ConsoleColor.White;
            Console.Write("                                                 ");
            Console.SetCursorPosition(2, 2);
            Console.ResetColor();
        }
    }
}