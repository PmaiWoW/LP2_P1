namespace LP2_P1
{
    /// <summary>
    /// Struct containing the information necessary to parse the ratings file
    /// </summary>
    public struct TitleBasics
    {
        // The ID of the rating
        public string TConst { get; }
        // The type of the video
        public TitleType? Type { get; }
        // It's main title
        public string PrimaryTitle { get; }
        // The original title given
        public string OriginalTitle { get; }
        // If it's for adults or not
        public bool IsAdult { get; }
        // The year the video was released or started
        public int? StartYear { get; }
        // The year the series ended
        public int? EndYear { get; }
        // The lenght of the movie in minutes
        public int? RuntimeMinutes { get; }
        // The 3 Genres it fits into
        public TitleGenre?[] Genres { get; }

        /// <summary>
        /// Constructor of TitleBasics
        /// </summary>
        /// <param name="tConst"> The ID given </param>
        /// <param name="type"> The type given </param>
        /// <param name="primaryTitle"> The primary title given </param>
        /// <param name="originalTitle"> The original title given </param>
        /// <param name="isAdult"> The adult rating given </param>
        /// <param name="genres"> All the genres given </param>
        /// <param name="startYear"> The year it started </param>
        /// <param name="endYear"> The year it ended </param>
        /// <param name="runtimeMinutes"> The leght of the movie given </param>
        public TitleBasics(string tConst, TitleType? type,
            string primaryTitle, string originalTitle, bool isAdult,
            TitleGenre[] genres, int? startYear = null, int? endYear = null,
            int? runtimeMinutes = null)
        {
            // Assigns this structs properties to the ones passed as argument
            TConst = tConst;
            Type = type;
            PrimaryTitle = primaryTitle;
            OriginalTitle = originalTitle;
            IsAdult = isAdult;
            StartYear = startYear;
            EndYear = endYear;
            RuntimeMinutes = runtimeMinutes;
            Genres = new TitleGenre?[3];
            for (int i = 0; i < genres?.Length; i++) Genres[i] = genres[i];
        }
    }
}