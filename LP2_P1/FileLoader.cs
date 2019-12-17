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
        // Full path to folder with data files
        private static string folderWithFiles = 
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.
                LocalApplicationData), appName);

        public static IEnumerable<TitleBasics> LoadTitleBasics()
        {
            // Full path to data files
            string fileTitleBasicsFull = Path.Combine(folderWithFiles,
                fileTitleBasics);
            try
            {
                CheckForFile(fileTitleBasicsFull);
            } catch (Exception missingFile)
            {
                Console.WriteLine($"The following error ocurred:\n" +
                    $"{missingFile.Message}");
                Environment.Exit(0);
            }

            // Define local variables
            string line;
            int currentObject = 0;
            int previous = 0;

            using (FileStream fs = new FileStream(fileTitleBasicsFull,
                FileMode.Open, FileAccess.Read))
            {
                using (GZipStream gzs = new GZipStream(fs,
                    CompressionMode.Decompress))
                {
                    using (StreamReader sr = new StreamReader(gzs))
                    {
                        // Declare Nullable variables
                        int? startYearNul;
                        int? endYearNul;
                        int? runtimeMinsNul;
                        TitleType? typeNul;

                        // Declare non-nullable variables
                        string[] genres;
                        TitleGenre[] genresFinal = new TitleGenre[3];
                        bool isAdult;
                        string finalString;

                        string[] elements;

                        CreateLoadingBar();

                        while ((line = sr.ReadLine()) != null)
                        {
                            finalString = "";
                            elements = line.Split("\t");

                            int progress = (currentObject++ / (6350607 / 100));

                            if (progress != previous)
                                FillLoadingBar();

                            if (elements[0] != "tconst")
                            {
                                typeNul = Enum.TryParse(elements[1].ToUpper(),
                                    out TitleType type) ? type :
                                    typeNul = null;

                                isAdult = (elements[4] == "1");

                                startYearNul = int.TryParse(elements[5],
                                    out int startYear) ? startYear :
                                    startYearNul = null;

                                endYearNul = int.TryParse(elements[6],
                                    out int endYear) ? endYear :
                                    endYearNul = null;


                                runtimeMinsNul = int.TryParse(elements[7],
                                    out int runtimeMins) ?
                                    runtimeMins : runtimeMinsNul = null;


                                genres = elements[8].Split(",");
                                for(int i = 0; i < genres.Length; i++)
                                {
                                    if (genres[i].Contains("-"))
                                    {
                                        string[] hyfenGenres =
                                            genres[i].Split("-");
                                        foreach (string hyfenS in hyfenGenres)
                                            finalString += hyfenS;
                                        genres[i] = finalString;
                                    }
                                }

                                Array.Resize<TitleGenre>(ref genresFinal,
                                    genres.Length);

                                for (int i = 0; i < genres.Length; i++)
                                    if (Enum.TryParse(genres[i].ToUpper(),
                                        out TitleGenre genre))
                                        genresFinal[i] = genre;

                                yield return new TitleBasics(elements[0],
                                    typeNul, elements[2], elements[3],
                                    isAdult, genresFinal, startYearNul,
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
            string fileTitleRatingsFull = Path.Combine(folderWithFiles,
                fileTitleRatings);
            // Define local variables
            string line;

            try
            {
                CheckForFile(fileTitleRatingsFull);
            }
            catch (Exception missingFile)
            {
                Console.WriteLine($"The following error ocurred:\n" +
                    $"{missingFile.Message}");
                Environment.Exit(0);
            }

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
                        string[] elements;

                        while ((line = sr.ReadLine()) != null)
                        {
                            elements = line.Split("\t");

                            if (elements[0] != "tconst")
                            {
                                float? averageRatingNul;
                                int? numVotesNul;

                                if (float.TryParse(elements[1],
                                    out float averageRating))
                                    averageRatingNul = averageRating;
                                else averageRatingNul = null;
                                if (int.TryParse(elements[2], out int 
                                    numVotes))
                                    numVotesNul = numVotes;
                                else numVotesNul = null;

                                yield return new TitleRatings(elements[0],
                                    averageRating, numVotes);
                            }
                        }
                    }
                }
            }
        }

        private static void CheckForFile(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new Exception("Required file not found, program will" +
                    " shutdown.\nPlease acquire the correct files with the" +
                    $" following names :\n{fileTitleBasics}\n" +
                    $"{fileTitleRatings}\nand place them in the correct " +
                    $"path:\n{folderWithFiles}");
            }
        }

    private static void CreateLoadingBar()
        {
            Console.WriteLine("\n\n\nLoading...");
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