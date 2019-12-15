using System;
using System.Collections.Generic;
using System.Linq;

namespace LP2_P1
{
    public static class StartMenu
    {
        private static string wantedTitle;
        private static ICollection<TitleType> types = new List<TitleType>();
        private static ICollection<TitleGenre?> genres = 
            new List<TitleGenre?>();
        private static bool? isAdult;
        private static int? start;
        private static int? end;
        private static int? runtimeLow;
        private static int? runtimeHigh;

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
                        Console.ReadKey(true);
                        break;

                    case ConsoleKey.Q:
                        Quit();
                        break;

                    default:
                        Console.Clear();
                        Console.WriteLine("Invalid option. Press any key to " +
                            "return to selection.");
                        Console.ReadKey(true);
                        break;
                }
            } while (key != ConsoleKey.Q);
        }

        private static void Titles(string wantedTitle, TitleType[] type,
            bool? adult, int? startDate, int? endDate,
            TitleGenre?[] genres, int? runtime1, int? runtime2)
        {
            IEnumerable<TitleBasics> originalNamedTitles =
                FileLoader.LoadTitleBasics();
            
            if (wantedTitle != null)
                originalNamedTitles = originalNamedTitles.Where
                    (c => c.PrimaryTitle.ToLower().Contains
                    (wantedTitle.Trim().ToLower()));
            if (runtime1.HasValue)
                originalNamedTitles = originalNamedTitles.Where
                    (c => c.RuntimeMinutes >= runtime1);
            if (runtime2.HasValue)
                originalNamedTitles = originalNamedTitles.Where
                    (c => c.RuntimeMinutes <= runtime2);
            if (startDate.HasValue)
                originalNamedTitles = originalNamedTitles.Where
                    (c => c.StartYear >= startDate);
            if (endDate.HasValue)
                originalNamedTitles = originalNamedTitles.Where
                    (c => c.EndYear <= endDate);
            if (type.Length > 0)
                originalNamedTitles = originalNamedTitles.Where
                    (c => type.Any(a => a == c.Type));
            if (adult.HasValue)
                originalNamedTitles = originalNamedTitles.Where
                    (c => c.IsAdult == adult);
            for (int i = 0; i < genres.Length - 1; i++)
                originalNamedTitles =
                    from title in originalNamedTitles
                    where title.Genres.Contains(genres[i])
                    select title;

            TitleSearch searcher = new TitleSearch();
            searcher.SearchTitle(originalNamedTitles);
        }

        private static void SubMenu()
        {
            wantedTitle = default;

            Console.Clear();

            ConsoleKey key;

            PrintSearchBar();
            PrintTypeSelection();

            Console.CursorTop = 2;

            do
            {
                Console.CursorLeft = 1;
                Console.Write(">");

                key = Console.ReadKey().Key;

                if (key == ConsoleKey.UpArrow && Console.CursorTop > 2)
                {
                    Program.ClearSpace();
                    Console.CursorTop -= 1;
                }
                else if (key == ConsoleKey.DownArrow && Console.CursorTop < 52)
                {
                    Program.ClearSpace();
                    Console.CursorTop += 1;
                }
                else if (key == ConsoleKey.Enter)
                {
                    if (Console.CursorTop == 2)
                    {
                        PrintSearchBar();
                        Console.CursorLeft = 4;
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.ForegroundColor = ConsoleColor.Black;
                        wantedTitle = Console.ReadLine();
                        Console.ResetColor();
                    }
                    if (Console.CursorTop >= 5 && Console.CursorTop <= 14)
                    {
                        int index = Console.CursorTop - 5;
                        if (types.Contains((TitleType)index))
                            types.Remove((TitleType)index);
                        else
                            types.Add((TitleType)index);
                        PrintTypeSelection();
                        Console.CursorTop = index + 5;
                    }
                    if (Console.CursorTop == 15)
                    {
                        if (isAdult == true) isAdult = false;
                        else if (isAdult == null) isAdult = true;
                        else if (isAdult == false) isAdult = null;
                        PrintTypeSelection();
                        Console.CursorTop = 15;
                    }
                    if (Console.CursorTop == 17)
                    {
                        Console.CursorLeft = 3;
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.BackgroundColor = ConsoleColor.White;
                        string[] date = Console.ReadLine().Split(' ');

                        if (date.Length >= 1 && date[0].Length == 4 &&
                            int.Parse(date[0]) != 0)
                                start = int.Parse(date[0]);
                        else
                            start = null;

                        if (date.Length == 2 && date[1].Length == 4 &&
                            int.Parse(date[1]) != 0)
                                end = int.Parse(date[1]);
                        else
                            end = null;

                        Console.ResetColor();
                    }
                    if (Console.CursorTop == 19)
                    {
                        Console.CursorLeft = 3;
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.BackgroundColor = ConsoleColor.White;
                        string[] runtime = Console.ReadLine().Split(' ');

                        if (runtime.Length >= 1 && runtime[0].Length == 4 &&
                            int.Parse(runtime[0]) != 0)
                                runtimeLow = int.Parse(runtime[0]);
                        else
                            runtimeLow = null;

                        if (runtime.Length == 2 && runtime[1].Length == 4 &&
                            int.Parse(runtime[1]) != 0)
                                runtimeHigh = int.Parse(runtime[1]);
                        else
                            runtimeHigh = null;

                        Console.ResetColor();
                    }
                    if (Console.CursorTop >= 21 && Console.CursorTop <= 47)
                    {
                        int indexes = Console.CursorTop - 21;
                        if (genres.Contains((TitleGenre)indexes))
                            genres.Remove((TitleGenre)indexes);
                        else
                            genres.Add((TitleGenre)indexes);
                        PrintTypeSelection();
                        Console.CursorTop = indexes + 21;
                    }
                    if (Console.CursorTop == 52)
                    {
                        Titles(wantedTitle, types.ToArray(), isAdult, start,
                            end, genres.ToArray(), runtimeLow, runtimeHigh);
                        key = ConsoleKey.Q;
                    }
                }
            } while (key != ConsoleKey.Q);
        }

        public static void Quit()
        {
            Console.Clear();
            Console.WriteLine("Thank you for using this searcher, we hope " +
                "to see you again!\nPress any key to exit.");
            Console.ReadKey(true);
            Environment.Exit(0);
        }

        private static void PrintTypeSelection()
        {
            string description1 = "  (- no filter | X adult | ' ' not adult)" +
                "\n\n";

            string description2 = "   Insert 8 digits separated by space, " +
                "press enter to leave them empty, any unexpected characters" +
                " will reset to 'unfilled'";

            string description3 = "   Insert the minimum and maximum " +
                "runtime we should look for any unexpected characters will " +
                "reset to 'unfilled'\n\n";

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
                Console.Write(i == 1? description3: description2 + " \n \n");
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