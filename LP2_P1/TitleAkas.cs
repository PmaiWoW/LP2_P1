namespace LP2_P1
{
    public struct TitleAkas
    {
        public string TitleID { get; }
        public int Ordering { get; }
        public string Title { get; }
        public string Region { get; }
        public string Language { get; }
        public string[] Types { get; }
        public string[] Attributes { get; }
        public bool IsOriginalTitle { get; }

        public TitleAkas(string titleID, int ordering, string title,
            string region, string language, string[] types,
            string[] attributes, bool isOriginalTitle)
        {
            TitleID = titleID;
            Ordering = ordering;
            Title = title;
            Region = region;
            Language = language;
            Types = types;
            Attributes = attributes;
            IsOriginalTitle = isOriginalTitle;
        }
    }
}