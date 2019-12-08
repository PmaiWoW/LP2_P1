namespace LP2_P1
{
    public struct TitleEpisode
    {
        public string TConst { get; }
        public string ParentTconst { get; }
        public int? SeasonNumber { get; }
        public int? EpisodeNumber { get; }

        public TitleEpisode(string tConst, string parentTConst,
            int? seasonNumber = null, int? episodeNumber = null)
        {
            TConst = tConst;
            ParentTconst = parentTConst;
            SeasonNumber = seasonNumber;
            EpisodeNumber = episodeNumber;
        }
    }
}