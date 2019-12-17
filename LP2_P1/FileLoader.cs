using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;

namespace LP2_P1
{
    /// <summary>
    /// FileLoader class.
    /// Has the names of folder and files, as well as paths, used as strings.
    /// Contains all methods that handle the loading/parsing of data files.
    /// </summary>
    public static class FileLoader
    {
        // Declare and initialize constant strings with folder and file names
        // Name of folder which contains data files
        private const string appName = "MyIMDBSearcher";
        // Name of file with data of TitleBasics
        private const string fileTitleBasics = "title.basics.tsv.gz";
        // Name of file with data of TitleRatings
        private const string fileTitleRatings = "title.ratings.tsv.gz";
        // Full path to folder with data files
        private static string folderWithFiles = 
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.
                LocalApplicationData), appName);

        // Parse full title.basics.tsv.gz data file into an IEnumerable of 
        // TitleBasics, which is later used to search the database
        public static IEnumerable<TitleBasics> LoadTitleBasics()
        {
            // Full path to data files
            string fileTitleBasicsFull = Path.Combine(folderWithFiles,
                fileTitleBasics);

            // Declare and/or initialize local variables
            // Line to be parsed
            string line;
            // Integer used to check which line is being parsed
            int currentObject = 0;
            // Integer used to check which line was read last
            int previous = 0;

            // try block that checks if TitleBasics file exists as it's
            // supposed to be in a method that throws an exception if
            // the file is not present, which is caught right after by the
            // catch block, which displays the exception message and shuts
            // down the program, if the exception is, in fact, thrown
            try
            {
                CheckForFile(fileTitleBasicsFull);
            } catch (Exception missingFile)
            {
                Console.WriteLine($"The following error ocurred:\n" +
                    $"{missingFile.Message}");
                Environment.Exit(0);
            }

            // Opens the data file with read permissions
            using (FileStream fs = new FileStream(fileTitleBasicsFull,
                FileMode.Open, FileAccess.Read))
            {
                // Decompresses the data file
                using (GZipStream gzs = new GZipStream(fs,
                    CompressionMode.Decompress))
                {
                    // Reads decompressed data file
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

                        // Beggining of parsing loop (only runs while line's
                        // contents are not null
                        while ((line = sr.ReadLine()) != null)
                        {
                            // Declare and instantiate various local variables
                            finalString = "";
                            // Split full line into various elements by tabs
                            elements = line.Split("\t");

                            int progress = (currentObject++ / (6350607 / 100));

                            if (progress != previous)
                                FillLoadingBar();

                            // Check if first element is an ID, to see if 
                            // line's contents are a title and, therefore,
                            // to be parsed
                            if (elements[0] != "tconst")
                            {
                                // TryParse 2nd element, if it returns true
                                // assign out variable's value to typeNul,
                                // otherwise it is null
                                typeNul = Enum.TryParse(elements[1].ToUpper(),
                                    out TitleType type) ? type :
                                    typeNul = null;
                                // Check if adult indentifier in line is 1 or
                                // not and assign value to isAdult variable
                                isAdult = (elements[4] == "1");
                                // TryParse 6th element, if it returns true
                                // assign out variable's value to startYearNul,
                                // otherwise it is null
                                startYearNul = int.TryParse(elements[5],
                                    out int startYear) ? startYear :
                                    startYearNul = null;
                                // TryParse 7th element, if it returns true
                                // assign out variable's value to endYearNul,
                                // otherwise it is null
                                endYearNul = int.TryParse(elements[6],
                                    out int endYear) ? endYear :
                                    endYearNul = null;
                                // TryParse 8th element, if it returns true
                                // assign out variable's value to 
                                // runtimeMinsNul, otherwise it is null
                                runtimeMinsNul = int.TryParse(elements[7],
                                    out int runtimeMins) ?
                                    runtimeMins : runtimeMinsNul = null;
                                // Split 9th element into a string array by 
                                // commas, which, in a for cycle, is then split
                                // again by hyfens if it contains one, joining
                                // the string again into a final string that is
                                // then assigned to current iteration's index 
                                // in the array
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
                                // Resize array
                                Array.Resize(ref genresFinal, genres.Length);
                                // for cycle, by which's iterations the content
                                // of the genres string array corresponding 
                                // index are TryParsed and assigned to the
                                // genresFinal TitleGenres array's
                                // corresponding index
                                for (int i = 0; i < genres.Length; i++)
                                    if (Enum.TryParse(genres[i].ToUpper(),
                                        out TitleGenre genre))
                                        genresFinal[i] = genre;
                                // yield return of new TitleBasics instance,
                                // with the necessary values given as 
                                // parameters
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

        /// <summary>
        /// Parses data in the title.ratings.tsv.gz file, said data being
        /// searched later as the database. Parsing 
        /// </summary>
        /// <returns>IEnumerable of TitleRatings which acts as the database to
        /// to be searched
        /// </returns>
        public static IEnumerable<TitleRatings> LoadTitleRatings()
        {
            // Declare and inicialize string with full path to TitleRatings file
            string fileTitleRatingsFull = Path.Combine(folderWithFiles,
                fileTitleRatings);
            // Define local variables
            string line;

            // try block that checks if TitleBasics file exists as it's
            // supposed to be in a method that throws an exception if 
            // the file is not present, which is caught right after by the
            // catch block, which displays the exception message and shuts 
            // down the program, if the exception is, in fact, thrown
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
                    // Reads decompressed data file
                    using (StreamReader sr = new StreamReader(gzs))
                    {
                        // Declare nullable variables
                        float? averageRatingNul;
                        int? numVotesNul;

                        // Declare non-nullable variables
                        string[] elements;

                        // Beggining of parsing loop (only runs while line's
                        // contents are not null
                        while ((line = sr.ReadLine()) != null)
                        {
                            // Split full line into various elements by tabs
                            elements = line.Split("\t");

                            if (elements[0] != "tconst")
                            {
                                // TryParse 2nd element, if it returns true
                                // assign out variable's value to 
                                // averageRatingNul, otherwise it is null
                                if (float.TryParse(elements[1],
                                    out float averageRating))
                                    averageRatingNul = averageRating;
                                else averageRatingNul = null;
                                // TryParse 3rd element, if it returns true
                                // assign out variable's value to 
                                // numVotesNul, otherwise it is null
                                if (int.TryParse(elements[2], out int 
                                    numVotes))
                                    numVotesNul = numVotes;
                                else numVotesNul = null;
                                // yield return of new TitleRatings instance,
                                // with the necessary values given as 
                                // parameters
                                yield return new TitleRatings(elements[0],
                                    averageRating, numVotes);
                            }
                        }
                    }
                }
            }
        }

        // 
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