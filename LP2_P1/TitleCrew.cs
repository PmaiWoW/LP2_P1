namespace LP2_P1
{
    public struct TitleCrew
    {
        public string Tconst { get; }
        public string[] Directors { get; }
        public string[] Writers { get; }

        public TitleCrew(string tconst, string[] directors, string[] writers)
        {
            Tconst = tconst;
            Directors = directors;
            Writers = writers;
        }
    }
}