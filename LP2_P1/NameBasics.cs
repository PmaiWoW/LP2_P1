namespace LP2_P1
{
    public struct NameBasics
    {
        public string NConst { get; }
        public string PrimaryName { get; }
        public int BirthYear { get; }
        public int? DeathYear { get; }
        public string[] PrimaryProfession { get; }
        public string[] KnownForTitles { get; }

        public NameBasics(string nConst, string primaryName, int birthYear,
            string[] primaryProfession, string[] knownForTitles,
            int? deathYear = null)
        {
            NConst = nConst;
            PrimaryName = primaryName;
            BirthYear = birthYear;
            DeathYear = deathYear;
            PrimaryProfession = primaryProfession;
            KnownForTitles = knownForTitles;
        }
    }
}