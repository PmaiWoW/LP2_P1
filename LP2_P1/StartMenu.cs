﻿using System;
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
                        UserInterface.PrintSearchBar();
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
                        UserInterface.PrintTypeSelection(types, genres,
                            isAdult);
                        Console.CursorTop = index + 5;
                    }
                    if (Console.CursorTop == 15)
                    {
                        if (isAdult == true) isAdult = false;
                        else if (isAdult == null) isAdult = true;
                        else if (isAdult == false) isAdult = null;
                        UserInterface.PrintTypeSelection(types, genres,
                            isAdult);
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
                        UserInterface.PrintTypeSelection(types, genres, 
                            isAdult);
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
            UserInterface.QuitMessage();
            Console.ReadKey(true);
            Environment.Exit(0);
        }
    }
}