﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace LP2_P1
{
    public static class SearchMenu
    {
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
                Console.WriteLine("1. Search Title\n" +
                                  "2. Search People\n" +
                                  "Q. Quit\n");

                key = Console.ReadKey().Key;

                switch (key)
                {
                    case ConsoleKey.D1:
                        TitleSearch();
                        break;

                    case ConsoleKey.D2:
                        UserInterface.NotImplemented();
                        break;

                    case ConsoleKey.Q:
                        Quit();
                        break;

                    default:
                        UserInterface.PrintInvalidChoice();
                        break;
                }
            } while (key != ConsoleKey.Q);
        }

        private static void Titles(string wantedTitle, TitleType[] type,
            bool? adult, int? startDate, int? endDate,
            TitleGenre?[] genres)
        {
            IEnumerable<TitleRatings> titleRatingsList = 
                FileLoader.LoadTitleRatings();
            IEnumerable<TitleBasics> titleBasicsList =
                FileLoader.LoadTitleBasics();
            
            if (wantedTitle != null)
                titleBasicsList = titleBasicsList.Where
                    (c => c.PrimaryTitle.ToLower().Contains
                    (wantedTitle.Trim().ToLower()));
            if (startDate.HasValue)
                titleBasicsList = titleBasicsList.Where
                    (c => c.StartYear >= startDate);
            if (endDate.HasValue)
                titleBasicsList = titleBasicsList.Where
                    (c => c.EndYear <= endDate);
            if (type.Length > 0)
                titleBasicsList = titleBasicsList.Where
                    (c => type.Any(a => a == c.Type));
            if (adult.HasValue)
                titleBasicsList = titleBasicsList.Where
                    (c => c.IsAdult == adult);
            for (int i = 0; i < genres.Length - 1; i++)
                titleBasicsList =
                    from title in titleBasicsList
                    where title.Genres.Contains(genres[i])
                    select title;

            IEnumerable<(TitleBasics a, TitleRatings p)> mixedList =
                from titles in titleBasicsList
                join ratings in titleRatingsList on titles.TConst equals ratings.Tconst
                where ratings.AverageRating >= 8 && ratings.AverageRating <= 10
                select (titles, ratings);

            TitleSearch searcher = new TitleSearch();
            searcher.SearchTitle(mixedList);
        }

        private static void TitleSearch()
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

                        if (int.TryParse(date[0], out int parsedStart) && 
                            date[0].Length == 4 && parsedStart != 0)
                            start = parsedStart;
                        else
                            start = null;
                        if (int.TryParse(date[1], out int parsedEnd) && 
                            date[1].Length == 4 && parsedEnd != 0)
                            start = parsedEnd;
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