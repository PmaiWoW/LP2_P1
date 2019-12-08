namespace LP2_P1
{
    public struct TitlePrincipals
    {
        public string Tconst { get; }
        public int Ordering { get; }
        public string Nconst { get; }
        public string Category { get; }
        public string Job { get; }
        public string Characters { get; }

        public TitlePrincipals(string tConst, int ordering, string nConst,
            string category, string job, string characters)
        {
            Tconst = tConst;
            Ordering = ordering;
            Nconst = nConst;
            Category = category;
            Job = job;
            Characters = characters;
        }
    }
}