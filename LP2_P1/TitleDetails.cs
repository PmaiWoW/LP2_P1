using System;
using System.Collections.Generic;
using System.Linq;

namespace LP2_P1
{
    internal static class TitleDetails
    {
        static TitleSearch searcher = new TitleSearch();

        public static void DisplayMenu(TitleBasics title, TitleRatings ratings,
            ref IEnumerable<(TitleBasics, TitleRatings)> namedTitles)
        {
            ConsoleKey key = ConsoleKey.D0;

            while (key != ConsoleKey.B)
            {
                Console.CursorLeft = 1;

                Console.Clear();
                Console.WriteLine($"{title}\n{ratings}");

                if (title.Type == TitleType.TVSERIES || title.Type ==
                    TitleType.TVMINISERIES)
                {
                    Console.WriteLine("'1' to go to episodes list");
                    if (key == ConsoleKey.D1)
                    {
                        IEnumerable<TitleEpisode> episodesEnumerable =
                            FileLoader.LoadTitleEpisode();
                        episodesEnumerable =
                            from titles in namedTitles
                            join episode in episodesEnumerable on
                            titles.Item1.TConst equals episode.ParentTconst
                            where episode.ParentTconst == titles.Item1.TConst
                            select episode;

                        DisplayEpisodes(title, ratings, ref episodesEnumerable,
                            ref namedTitles);
                    }
                }

                if (title.Type == TitleType.TVEPISODE)
                {
                    Console.WriteLine("'1' to go to inspect parent title");
                    if (key == ConsoleKey.D1)
                    {
                        IEnumerable<TitleEpisode> episodesEnumerable =
                            FileLoader.LoadTitleEpisode();
                        episodesEnumerable =
                            from titles in namedTitles
                            join episode in episodesEnumerable on
                            titles.Item1.TConst equals episode.ParentTconst
                            where episode.ParentTconst == titles.Item1.TConst
                            select episode;

                        // (from episode in episodesEnumerable
                        //  where title.TConst == episode.ParentTconst
                        //  select title).First();

                        DisplaySeries(title.TConst, ref namedTitles);
                    }
                }

                // if(title.Type == TitleType.TVSERIES || title.Type == 
                //     TitleType.TVMINISERIES)
                // {
                //     Console.WriteLine("'1' to go to episodes info");
                //     if (key == ConsoleKey.D1)
                //     {
                //         namedTitles =
                //             from titles in namedTitles
                //             where titles.Item1.TConst ==
                //             titles.Item3.ParentTconst
                //             select titles;
                //         searcher.SearchTitle(namedTitles);
                //     }
                // }

                Console.WriteLine("\nPress 'B' to to back to previous menu");

                key = Console.ReadKey().Key;
            }
        }

        private static void DisplaySeries(string TConst,
            ref IEnumerable<(TitleBasics, TitleRatings)> namedTitles)
        {
            TitleBasics parentTitle =
                (from title in namedTitles
                 where title.Item1.TConst == TConst
                 select title.Item1).ToArray()[0];

            Console.WriteLine(parentTitle);
        }

        private static void DisplayEpisodes(TitleBasics title, TitleRatings
            ratings, ref IEnumerable<TitleEpisode> episodesEnumerable,
            ref IEnumerable<(TitleBasics, TitleRatings)> namedTitles)
        {
            TitleEpisode[] episodesArray = episodesEnumerable.ToArray();
            TitleEpisode[] seasonEpisodesArray;
            string input;

            episodesArray =
                (from episode in episodesEnumerable
                 where episode.ParentTconst == title.TConst
                 orderby episode.EpisodeNumber
                 orderby episode.SeasonNumber
                 select episode).ToArray();

            Console.WriteLine($"Show has {episodesArray.Last().SeasonNumber}" +
                $" seasons.\nInsert the number of the one you want to view.");
            input = Console.ReadLine();

            if (int.TryParse(input, out int seasonNum))
            {
                seasonEpisodesArray =
                    (from episode in episodesArray
                     where episode.SeasonNumber == seasonNum
                     select episode
                    ).ToArray();

                for (int i = 0; i < seasonEpisodesArray.Length; i++)
                {
                    Console.WriteLine(seasonEpisodesArray[i]);
                }

                Console.WriteLine($"There are {seasonEpisodesArray.Length} " +
                    $"episode in this season.\n'1' to choose an episode to " +
                    $"inspect\n'B' to go back");

                SelectEpisode(seasonEpisodesArray, ref namedTitles,
                    ref episodesEnumerable);
            }

        }

        private static void SelectEpisode(TitleEpisode[] seasonEpisodes,
            ref IEnumerable<(TitleBasics, TitleRatings)> namedTitles,
            ref IEnumerable<TitleEpisode> episodeEnum)
        {
            Console.WriteLine("Insert the number of the episode you want to " +
                "inspect");
            string input = Console.ReadLine();

            if (int.TryParse(input, out int epNum))
            {
                DisplayChild(seasonEpisodes[epNum].ParentTconst, ref
                    episodeEnum, epNum);
            }
        }

        private static void DisplayChild(string TConst, ref
            IEnumerable<TitleEpisode> episodeEnum, int epNum)
        {
            TitleEpisode childTitle =
                (from title in episodeEnum
                 where title.ParentTconst == TConst
                 select title).ToArray()[epNum];

            Console.WriteLine(childTitle);
        }

        private static void DisplayParent(string TConst, ref
            IEnumerable<(TitleBasics, TitleRatings)> namedTitles)
        {
            TitleBasics parentTitle =
                (from title in namedTitles
                 where title.Item1.TConst == TConst
                 select title.Item1).ToArray()[0];

            Console.WriteLine(parentTitle);
        }
    }
}