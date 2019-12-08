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
            int? startYear, TitleGenre[]? genres, int? endYear = null,
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

        public override string ToString()
        {
            string typePrint =
                Type.HasValue ? Type.ToString() : @"\N";
            string startYearPrint =
                StartYear.HasValue ? StartYear.ToString() : @"\N";
            string endYearPrint =
                EndYear.HasValue ? EndYear.ToString() : @"\N";
            string isAdult = IsAdult ? "Adult Only" : "For Everyone";
            string[] genresPrint = new string[3];
            for (int i = 0; i < 3; i++)
                genresPrint[i] = Genres[i].HasValue ? 
                    Genres[i].ToString() : @"\N";

            string s = $"Title Name: {PrimaryTitle}, Type: {typePrint}, " +
               $"Is Adult Only? {isAdult}, " +
               $"Release Year: {StartYear}, " +
               $"Ending Year: {endYearPrint} " +
               $"Runtime (Mins): {RuntimeMinutes}, " +
               $"Genres: {genresPrint[0]}, {genresPrint[1]}, {genresPrint[2]}";
            return s;
        }
    }
}