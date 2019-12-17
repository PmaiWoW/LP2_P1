using System;
using System.Collections.Generic;
using System.Linq;

namespace LP2_P1
{
    public class Searcher
    {
        public static IEnumerable<TitleBasics> originalNamedTitles =
            FileLoader.LoadTitleBasics();

        public static IEnumerable<TitleBasics> searchResults;


        private static string wantedTitleName;
        private static ICollection<TitleType> types = new List<TitleType>();
        private static ICollection<TitleGenre?> genres =
            new List<TitleGenre?>();
        private static bool? isAdult;
        private static int? startYear;
        private static int? endYear;
        public void SelectSearch()
        {
            ConsoleKey key = ConsoleKey.D0;

            do
            {
                UserInterface.SelectSearchUI();
                key = Console.ReadKey().Key;

                switch (key)
                {
                    case ConsoleKey.D1:
                        TitleSearch();
                        break;
                    case ConsoleKey.D2:
                        PeopleSearch();
                        break;
                    case ConsoleKey.Q:
                        Quit();
                        break;
                }

            } while (key != ConsoleKey.Q);

        }

        public void TitleSearch()
        {
            ConsoleKey key = ConsoleKey.D0;
            do
            {
                key = Console.ReadKey().Key;

                switch (key)
                {
                    case ConsoleKey.D1:
                        wantedTitleName = UserInterface.SelectNameUI();
                        break;
                    case ConsoleKey.D2:
                        break;
                    case ConsoleKey.D3:
                        isAdult = UserInterface.SelectAgeRestrictionUI();

                        break;
                    case ConsoleKey.D4:
                        startYear = UserInterface.SelectStartYearUI();
                        break;
                    case ConsoleKey.D5:
                        endYear = UserInterface.SelectEndYearUI();
                        break;
                    case ConsoleKey.D6:
                        break;
                    case ConsoleKey.Enter:
                        //Titles();
                        break;
                    case ConsoleKey.B:
                        break;
                    default:
                        break;
                }


            } while (key != ConsoleKey.B);

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





























        // PEOPLESEARCH METHODS
        //---------------------------------------------------------------------
        public void PeopleSearch()
        {
            UserInterface.NotImplemented();
        }


        public static void Quit()
        {
            UserInterface.QuitMessage();
            Console.ReadKey(true);
            Environment.Exit(0);
            return;
        }
    }
}
