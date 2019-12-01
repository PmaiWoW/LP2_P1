namespace LP2_P1
{
    public struct TitleBasics
    {
        public string TConst { get; }
        public string TitleType { get; }
        public string PrimaryTitle { get; }
        public string OriginalTitle { get; }
        public bool IsAdult { get; }
        public int StartYear { get; }
        public int? EndYear { get; }
        public int RuntimeMinutes { get; }
        public string[] Genres { get; }

        public TitleBasics(string tConst, string titleType,
            string primaryTitle, string originalTitle, bool isAdult, 
            int startYear, int runtimeMinutes, string[] genres,
            int? endYear = null)
        {
            TConst = tConst;
            TitleType = titleType;
            PrimaryTitle = primaryTitle;
            OriginalTitle = originalTitle;
            IsAdult = isAdult;
            StartYear = startYear;
            EndYear = endYear;
            RuntimeMinutes = runtimeMinutes;
            Genres = genres;
        }
    }
}
