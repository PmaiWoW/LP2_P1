using System;
using System.Collections.Generic;
using System.Linq;

namespace LP2_P1
{
    public static class SearchMenu
    {
        public static IEnumerable<TitleBasics> originalNamedTitles =
            FileLoader.LoadTitleBasics();

        public static IEnumerable<TitleBasics> searchResults;

        private static string wantedTitle;
        private static ICollection<TitleType> types = new List<TitleType>();
        private static ICollection<TitleGenre?> genres = 
            new List<TitleGenre?>();
        private static bool? isAdult;
        private static int? start;
        private static int? end;

        public static void MenuLoop()
        {
            ConsoleKey key;

            do
            {
                Console.Clear();
                Console.WriteLine("\n  1. Search Title\n" +
                                  "  2. Search People\n" +
                                  "  Q. Quit\n");

                key = Console.ReadKey().Key;

                switch (key)
                {
                    case ConsoleKey.D1:
                        wantedTitle = default;
                        TitleSearch();
                        break;

                    case ConsoleKey.D2:
                        UserInterface.NotImplemented();
                        break;

                    case ConsoleKey.Q:
                        Quit();
                        break;

                    default:
                        break;
                }
            } while (key != ConsoleKey.Q);
        }

        private static void Titles(string wantedTitle, TitleType[] type,
            bool? adult, int? startDate, int? endDate,
            TitleGenre?[] genres)
        {
            searchResults = originalNamedTitles;

            if (wantedTitle != null)
                searchResults = searchResults.Where
                    (c => c.PrimaryTitle.ToLower().Contains
                    (wantedTitle.Trim().ToLower()));
            if (startDate.HasValue)
                searchResults = searchResults.Where
                    (c => c.StartYear >= startDate);
            if (endDate.HasValue)
                searchResults = searchResults.Where
                    (c => c.EndYear <= endDate);
            if (type.Length > 0)
                searchResults = searchResults.Where
                    (c => type.Any(a => a == c.Type));
            if (adult.HasValue)
                searchResults = searchResults.Where
                    (c => c.IsAdult == adult);
            for (int i = 0; i < genres.Length - 1; i++)
                searchResults =
                    from title in searchResults
                    where title.Genres.Contains(genres[i])
                    select title;

            TitleSearch searcher = new TitleSearch();

            searcher.SearchTitle(searchResults);
        }

        private static void TitleSearch()
        {
            Console.Clear();

            ConsoleKey key;

            UserInterface.PrintSearchBar();
            UserInterface.PrintTypeSelection(types, genres, isAdult);

            Console.CursorTop = 2;

            do
            {
                Console.CursorLeft = 1;
                Console.Write(">");

                key = Console.ReadKey().Key;
                
                if (key == ConsoleKey.UpArrow && Console.CursorTop > 2)
                {
                    UserInterface.ClearSpace();
                    Console.CursorTop -= 1;
                }
                else if (key == ConsoleKey.DownArrow && Console.CursorTop < 51)
                {
                    UserInterface.ClearSpace();
                    Console.CursorTop += 1;
                }
                else if (key == ConsoleKey.Enter)
                {
                    if (Console.CursorTop == 2)
                    {
                        UserInterface.ResizeWindow();
                        UserInterface.PrintSearchBar();
                        UserInterface.ColorSetup(4);
                        wantedTitle = default;
                        wantedTitle = Console.ReadLine();
                        Console.ResetColor();
                    }
                    if (Console.CursorTop >= 5 && Console.CursorTop <= 14)
                    {
                        UserInterface.ResizeWindow();
                        int index = Console.CursorTop - 5;
                        if (types.Contains((TitleType)index))
                            types.Remove((TitleType)index);
                        else
                            types.Add((TitleType)index);
                        UserInterface.PrintTypeSelection(types, genres,
                            isAdult);
                        Console.CursorTop = index + 5;
                    }
                    if (Console.CursorTop == 16)
                    {
                        UserInterface.ResizeWindow();
                        if (isAdult == true) isAdult = false;
                        else if (isAdult == null) isAdult = true;
                        else if (isAdult == false) isAdult = null;
                        UserInterface.PrintTypeSelection(types, genres,
                            isAdult);
                        Console.CursorTop = 16;
                    }
                    if (Console.CursorTop == 18)
                    {
                        UserInterface.ResizeWindow();
                        UserInterface.ColorSetup(3);
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
                    if (Console.CursorTop >= 20 && Console.CursorTop <= 47)
                    {
                        UserInterface.ResizeWindow();
                        int indexes = Console.CursorTop - 20;
                        if (genres.Contains((TitleGenre)indexes))
                            genres.Remove((TitleGenre)indexes);
                        else
                            genres.Add((TitleGenre)indexes);
                        UserInterface.PrintTypeSelection(types, genres, 
                            isAdult);
                        Console.CursorTop = indexes + 20;
                    }
                    if (Console.CursorTop >= 49 && Console.CursorTop <= 51)
                    {
                        UserInterface.ResizeWindow();
                        Titles(wantedTitle, types.ToArray(), isAdult, start,
                            end, genres.ToArray());
                        key = ConsoleKey.Q;
                    }
                }
            } while (key != ConsoleKey.Q);
        }

        public static void Quit()
        {
            UserInterface.QuitMessage();
            Console.ReadKey(true);
            Environment.Exit(0);
        }
    }
}