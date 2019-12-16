using System;
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
            TitleGenre?[] genres, int? runtime1, int? runtime2,
            int? rating1, int? rating2)
        {
            TitleSearch searcher = new TitleSearch();

            IEnumerable<TitleBasics> titleBasicsEnumerable =
                FileLoader.LoadTitleBasics();
            IEnumerable<TitleRatings> titleRatingsEnumerable =
                FileLoader.LoadTitleRatings();
            IEnumerable<TitleEpisode> titleEpisodeEnumerable =
                FileLoader.LoadTitleEpisode();

            if (wantedTitle != null)
                titleBasicsEnumerable = titleBasicsEnumerable.Where
                    (c => c.PrimaryTitle.ToLower().Contains
                    (wantedTitle.Trim().ToLower()));
            if (runtime1.HasValue)
                titleBasicsEnumerable = titleBasicsEnumerable.Where
                    (c => c.RuntimeMinutes >= runtime1);
            if (runtime2.HasValue)
                titleBasicsEnumerable = titleBasicsEnumerable.Where
                    (c => c.RuntimeMinutes <= runtime2);
            if (startDate.HasValue)
                titleBasicsEnumerable = titleBasicsEnumerable.Where
                    (c => c.StartYear >= startDate);
            if (endDate.HasValue)
                titleBasicsEnumerable = titleBasicsEnumerable.Where
                    (c => c.EndYear <= endDate);
            if (type.Length > 0)
                titleBasicsEnumerable = titleBasicsEnumerable.Where
                    (c => type.Any(a => a == c.Type));
            if (adult.HasValue)
                titleBasicsEnumerable = titleBasicsEnumerable.Where
                    (c => c.IsAdult == adult);
            for (int i = 0; i < genres.Length - 1; i++)
                titleBasicsEnumerable =
                    from title in titleBasicsEnumerable
                    where title.Genres.Contains(genres[i])
                    select title;

            Console.WriteLine($"rating1:{rating1.HasValue}\nrating2{rating2.HasValue}");
            Console.ReadKey();

            IEnumerable<(TitleBasics titles, TitleRatings ratings)> mixedEnum =
                titleBasicsEnumerable.GroupJoin(titleRatingsEnumerable.
                Where(c => true), title2 => title2.TConst, rating2 => 
                rating2.Tconst, (t, r) => new 
                {
                    Title = t,
                    Rating = r.Where(y => y.Tconst.Contains(t.TConst))
                }).Select(x => (x.Title, x.Rating.FirstOrDefault())).ToArray();

            if (rating1.HasValue)
                mixedEnum = mixedEnum.Where(c =>
                c.ratings.AverageRating >= rating1);
            if (rating2.HasValue)
                mixedEnum = mixedEnum.Where(c =>
                c.ratings.AverageRating <= rating2);

            searcher.SearchTitle(mixedEnum);
        }

        private static void TitleSearch()
        {
            int? start = null;
            int? end = null;
            int? runtime1 = null;
            int? runtime2 = null;
            int? ratingLow = null;
            int? ratingHigh = null;
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
                else if (key == ConsoleKey.DownArrow && Console.CursorTop < 55)
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
                    if (Console.CursorTop == 20)
                    {
                        UserInterface.ResizeWindow();
                        UserInterface.ColorSetup(3);
                        string[] runtime = Console.ReadLine().Split(' ');

                        if (runtime.Length >= 1 && runtime[0].Length > 0 &&
                            int.Parse(runtime[0]) != 0)
                            runtime1 = int.Parse(runtime[0]);
                        else
                            runtime1 = null;

                        if (runtime.Length == 2 && runtime[0].Length > 0 &&
                            int.Parse(runtime[1]) != 0)
                            runtime2 = int.Parse(runtime[1]);
                        else
                            runtime2 = null;

                        Console.ResetColor();
                    }
                    if (Console.CursorTop == 22)
                    {
                        UserInterface.ResizeWindow();
                        UserInterface.ColorSetup(3);
                        string[] rating = Console.ReadLine().Split(' ');

                        if (rating.Length >= 1 && rating[0].Length > 0 &&
                            int.Parse(rating[0]) != 0)
                            ratingLow = int.Parse(rating[0]);
                        else
                            ratingLow = null;

                        if (rating.Length == 2 && rating[1].Length > 0 &&
                            int.Parse(rating[1]) != 0)
                            ratingHigh = int.Parse(rating[1]);
                        else
                            ratingHigh = null;

                        Console.ResetColor();
                    }
                    if (Console.CursorTop >= 24 && Console.CursorTop <= 51)
                    {
                        UserInterface.ResizeWindow();
                        int indexes = Console.CursorTop - 24;
                        if (genres.Contains((TitleGenre)indexes))
                            genres.Remove((TitleGenre)indexes);
                        else
                            genres.Add((TitleGenre)indexes);
                        UserInterface.PrintTypeSelection(types, genres,
                            isAdult);
                        Console.CursorTop = indexes + 24;
                    }
                    if (Console.CursorTop >= 53 && Console.CursorTop <= 55)
                    {
                        UserInterface.ResizeWindow();
                        Titles(wantedTitle, types.ToArray(), isAdult, start,
                            end, genres.ToArray(), runtime1, runtime2,
                            ratingLow, ratingHigh);
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