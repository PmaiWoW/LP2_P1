namespace LP2_P1
{
    public struct TitleBasics
    {
        public string TConst { get; }
        public TitleType? Type { get; }
        public string PrimaryTitle { get; }
        public string OriginalTitle { get; }
        public bool IsAdult { get; }
        public int? StartYear { get; }
        public int? EndYear { get; }
        public int? RuntimeMinutes { get; }
        public TitleGenre?[] Genres { get; }

        public TitleBasics(string tConst, TitleType? type,
            string primaryTitle, string originalTitle, bool isAdult,
            TitleGenre[] genres, int? startYear = null, int? endYear = null,
            int? runtimeMinutes = null)
        {
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