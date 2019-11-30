using System;
using System.Collections.Generic;
using System.Text;

namespace LP2_P1
{
    public struct TitleRatings
    {
        public string Tconst { get; }
        public float AverageRating { get; }
        public int NumVotes { get; }

        public TitleRatings(string tconst, float averageRating, int numVotes)
        {
            Tconst = tconst;
            AverageRating = averageRating;
            NumVotes = numVotes;
        }
    }
}
