using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace LP2_P1
{
    public static class Sorter
    {
        public static IEnumerable<TitleBasics> SortByType(IEnumerable<TitleBasics> namedTitles)
        {
            return namedTitles.OrderBy(c => c.Type);
        }

        // Sorts searched items by PrimaryTitle
        public static IEnumerable<TitleBasics> SortByTitle(IEnumerable<TitleBasics> namedTitles)
        {
            return namedTitles.OrderBy(c => c.PrimaryTitle);
        }

        // Sorts searched items by StartYear
        public static IEnumerable<TitleBasics> SortByRelease(IEnumerable<TitleBasics> namedTitles)
        {
            return namedTitles.OrderBy(c => c.StartYear);
        }

        // Sorts searched items by EndYear
        public static IEnumerable<TitleBasics> SortByEnd(IEnumerable<TitleBasics> namedTitles)
        {
            return namedTitles.OrderBy(c => c.EndYear);
        }

        // Sorts searched items by Genres
        public static IEnumerable<TitleBasics> SortByGenre(IEnumerable<TitleBasics> namedTitles)
        {
            return namedTitles.OrderBy(c => c.Genres[0]);
        }

        // Sorts searched items by IsAdult
        public static IEnumerable<TitleBasics> SortByIsAdult(IEnumerable<TitleBasics> namedTitles)
        {
            return namedTitles.OrderBy(c => c.IsAdult);
        }

        public static IEnumerable<TitleBasics> SortByRating(IEnumerable<TitleBasics> namedTitles)
        {
            IEnumerable<string> named = namedTitles.Select(c => c.TConst).ToList();

            IEnumerable<string> ratings = FileLoader.LoadTitleRatings()
                .Where(p => named.Contains(p.Tconst))
                .OrderBy(o => o.AverageRating).Select(c => c.Tconst).ToList();

            return namedTitles.Where(p => ratings
                .Contains(p.TConst)).OrderBy(b => ratings.ToList()
                .FindIndex(a => a == b.TConst)).Reverse().Concat(namedTitles
                .Where(p => !ratings.Contains(p.TConst))).ToList();
        }
    }
}
