namespace LP2_P1
{
    public struct TitleRatings
    {
        public string Tconst { get; }
        public float? AverageRating { get; }
        public int? NumVotes { get; }

        public TitleRatings(string tConst, float averageRating, int numVotes)
        {
            Tconst = tConst;
            AverageRating = averageRating;
            NumVotes = numVotes;
        }
        public override string ToString()
        {
            if(!AverageRating.HasValue && !NumVotes.HasValue)
            {
                return "This title has no ratings";
            }
            return $"\n   Average Ratings: {AverageRating} in {NumVotes} " +
                $"votes";
        }
    }
}