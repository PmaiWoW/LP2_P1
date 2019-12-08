namespace LP2_P1
{
    public struct TitleBasics
    {
        public string TConst { get; }
        public string Type { get; }
        public string PrimaryTitle { get; }
        public string OriginalTitle { get; }
        public bool IsAdult { get; }
        public int StartYear { get; }
        public int? EndYear { get; }
        public int? RuntimeMinutes { get; }
        public string[] Genres { get; }

        public TitleBasics(string tConst, string type,
            string primaryTitle, string originalTitle, bool isAdult, 
            int startYear, string[] genres, int? endYear = null,
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
            Genres = new string[3];
            for (int i = 0; i < genres.Length; i++) Genres[i] = genres[i];
        }

        public override string ToString()
        {
            for (int i = 0; i < 3; i++) 
                Genres[i] = Genres[i] ?? @"\N";
            string endYearPrint = 
                EndYear.HasValue ? EndYear.ToString() : @"\N";
            string isAdult = IsAdult ? "Adult Only" : "For Everyone";

            string s = $"Title Name: {PrimaryTitle}, Type: {Type}, " +
               $"Is Adult Only? {isAdult}, " +
               $"Release Year: {StartYear}, " +
               $"Ending Year: {endYearPrint} " +
               $"Runtime (Mins): {RuntimeMinutes}, " +
               $"Genres: {Genres[0]}, {Genres[1]}, {Genres[2]}";
            return s;
        }
    }
}