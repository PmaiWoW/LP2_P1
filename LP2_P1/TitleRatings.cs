namespace LP2_P1
{
    public struct TitleRatings
    {
        // Instance Variables with the information necessary to a title rating
        public string Tconst { get; }
        public float? AverageRating { get; }
        public int? NumVotes { get; }

        // 'TitleRatings' Constructor Method
        public TitleRatings(string tConst, float averageRating, int numVotes)
        {
            Tconst = tConst;
            AverageRating = averageRating;
            NumVotes = numVotes;
        }
    }
}