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

            // 
            using (FileStream fs = new FileStream(fileTitleBasicsFull,
                FileMode.Open))
            {
                using (StreamReader sr = new StreamReader(fs))
                {
                    int startYear;
                    int endYear;
                    int runtimeMins;
                    int? endYearNul;
                    int? runtimeMinsNul;
                    string[] genres;
                    bool isAdult;

                    string[] elements;

                    while ((line = sr.ReadLine()) != null)
                    {
                        if (line[0] == 't' && line[1] == 't')
                        {
                            elements = line.Split("\t");

                            if (elements[4] == "1") isAdult = true;
                            else isAdult = false;
                            int.TryParse(elements[5], out startYear);
                            if (int.TryParse(elements[6], out endYear))
                                endYearNul = endYear;
                            else endYearNul = null;
                            if (int.TryParse(elements[7], out runtimeMins))
                                runtimeMinsNul = runtimeMins;
                            else runtimeMinsNul = null;
                            genres = elements[8].Split(",");

                            yield return new TitleBasics(elements[0], elements[1],
                                elements[2], elements[3], isAdult, startYear,
                                genres, endYearNul, runtimeMinsNul);
                        }
                    }
                }
            }
        }

        
        public static IEnumerable<TitleRatings> LoadTitleRatings()
        {
        // Full path to folder with data files
            string folderWithFiles = Path.Combine(Environment.GetFolderPath(
                Environment.SpecialFolder.LocalApplicationData), appName);
            // Full path to data files
            string fileTitleRatingsFull = Path.Combine(folderWithFiles,
                fileTitleRatings);

            // Define local variables
            string line;

            // 
            using (FileStream fs = new FileStream(fileTitleRatingsFull,
                FileMode.Open, FileAccess.Read))
            {
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


                                yield return new TitleRatings(elements[0], averageRating,
                                    numVotes);
                            }
                        }
                    }
                }
            }            
        }
    }
}
