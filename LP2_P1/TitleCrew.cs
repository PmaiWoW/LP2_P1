namespace LP2_P1
{
    public struct TitleCrew
    {
        public string TConst { get; }
        public string[] Directors { get; }
        public string[] Writers { get; }

        public TitleCrew(string tConst, string[] directors, string[] writers)
        {
            TConst = tConst;
            Directors = directors;
            Writers = writers;
        }
    }
}