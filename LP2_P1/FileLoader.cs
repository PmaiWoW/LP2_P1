﻿using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;

namespace LP2_P1
{
    public static class FileLoader
    {
        private const string appName = "MyIMDBSearcher";
        private const string fileTitleBasics = "title.basics.tsv.gz";
        private const string fileTitleRatings = "title.ratings.tsv.gz";

        public static IEnumerable<TitleBasics> LoadTitleBasics()
        {
            // Full path to folder with data files
            string folderWithFiles = Path.Combine(Environment.GetFolderPath(
                Environment.SpecialFolder.LocalApplicationData), appName);
            // Full path to data files
            string fileTitleBasicsFull = Path.Combine(folderWithFiles,
                fileTitleBasics);

            // Define local variables
            string line;

            int currentObject = 0;
            int previous = 0;

            using (FileStream fs = new FileStream(fileTitleBasicsFull,
                FileMode.Open))
            {
                using (GZipStream gzs = new GZipStream(fs,
                    CompressionMode.Decompress))
                {
                    using (StreamReader sr = new StreamReader(gzs))
                    {
                        // Declare Nullable variables
                        int? endYearNul;
                        int? runtimeMinsNul;
                        TitleType? typeNul;

                        // Declare non-nullable variables
                        string[] genres;
                        TitleGenre[] genresTypes = new TitleGenre[3];
                        bool isAdult;

                        string[] elements;

                        Console.WriteLine("");
                        Console.WriteLine("Loading...");
                        CreateLoadingBar();

                        while ((line = sr.ReadLine()) != null)
                        {
                            elements = line.Split("\t");

                            int progress = (currentObject++ / (6350607 / 100));

                            if (progress != previous)
                                FillLoadingBar();

                            if (elements[0] != "tconst")
                            {
                                if (Enum.TryParse(elements[1],
                                    out TitleType type)) typeNul = type;
                                else typeNul = null;

                                if (elements[4] == "1") isAdult = true;
                                else isAdult = false;

                                int.TryParse(elements[5], out int startYear);

                                if (int.TryParse(elements[6], out int endYear))
                                    endYearNul = endYear;
                                else endYearNul = null;

                                if (int.TryParse(elements[7], 
                                    out int runtimeMins))
                                    runtimeMinsNul = runtimeMins;
                                else runtimeMinsNul = null;

                                genres = elements[8].Split(",");
                                for(int i = 0; i < genres.Length; i++)
                                {
                                    if (Enum.TryParse(genres[i], out TitleGenre
                                        genre))
                                    {
                                        genresTypes[i] = genre;
                                    }
                                }

                                yield return new TitleBasics(elements[0], 
                                    typeNul, elements[2], elements[3], 
                                    isAdult, startYear, genresTypes, 
                                    endYearNul,  runtimeMinsNul);
                            }
                            previous = progress;
                        }
                        Console.CursorVisible = true;
                    }
                }
            }
        }

        public static IEnumerable<TitleRatings> LoadTitleRatings()
        {
            // Full path to folder with data files
            string folderWithFiles = Path.Combine(Environment.GetFolderPath(
                Environment.SpecialFolder.LocalApplicationData), appName);
            // Full path to data file
            string fileTitleRatingsFull = Path.Combine(folderWithFiles,
                fileTitleRatings);

            // Define local variables
            string line;

            // Opens the data file with read permissions
            using (FileStream fs = new FileStream(fileTitleRatingsFull,
                FileMode.Open, FileAccess.Read))
            {
                // Decompresses the data file
                using (GZipStream gzs = new GZipStream(fs,
                                CompressionMode.Decompress))
                {
                    using (StreamReader sr = new StreamReader(gzs))
                    {
                        int numVotes;
                        float averageRating;

                        string[] elements;

                        while ((line = sr.ReadLine()) != null)
                        {
                            if (line[0] == 't' && line[1] == 't')
                            {
                                elements = line.Split("\t");

                                float.TryParse(elements[1], out averageRating);
                                int.TryParse(elements[2], out numVotes);

                                yield return new TitleRatings(elements[0],
                                    averageRating, numVotes);
                            }
                        }
                    }
                }
            }
        }

        private static void CreateLoadingBar()
        {
            Console.BackgroundColor = ConsoleColor.DarkRed;
            for (int i = 0; i < 100; i++)
                Console.Write(" ");
            Console.CursorLeft = 0;
            Console.ResetColor();
        }

        private static void FillLoadingBar()
        {
            Console.CursorVisible = false;
            Console.BackgroundColor = ConsoleColor.White;
            Console.Write(" ");
            Console.ResetColor();
        }
    }
}