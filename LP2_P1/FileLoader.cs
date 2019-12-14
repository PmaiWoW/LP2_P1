using System;
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

                        Console.WriteLine();
                        Console.WriteLine("Loading...");
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
                        string[] elements;

                        while ((line = sr.ReadLine()) != null)
                        {
                            elements = line.Split("\t");

                            if (elements[0] != "tconst")
                            {
                                float.TryParse(elements[1], 
                                    out float averageRating);
                                int.TryParse(elements[2], out int numVotes);

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