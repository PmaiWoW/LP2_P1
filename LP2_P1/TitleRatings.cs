namespace LP2_P1
{
    /// <summary>
    /// Struct containing the information necessary to parse the ratings file
    /// </summary>
    public struct TitleRatings
    {
        // The ID of the rating
        public string Tconst { get; }
        // The average rating 
        public float? AverageRating { get; }
        // Number of votes it has
        public int? NumVotes { get; }

        /// <summary>
        /// Constructor of TitleRatings
        /// </summary>
        /// <param name="tConst"> The ID given </param>
        /// <param name="averageRating"> The average rating it given </param>
        /// <param name="numVotes"> The number of votes it given </param>
        public TitleRatings(string tConst, float averageRating, int numVotes)
        {
            // Assigns this structs properties to the ones passed as argument
            Tconst = tConst;
            AverageRating = averageRating;
            NumVotes = numVotes;
        }
    }
}