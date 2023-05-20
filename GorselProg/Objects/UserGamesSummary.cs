using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GorselProg.Objects
{
    public class UserGamesSummary
    {
        public int TotalGamesPlayed { get; set; }
        public int WonGames { get; set; }
        public int CorrectAnswers { get; set; }

        public int Category1Correct { get; set; }
        public int Category2Correct { get; set; }
        public int Category3Correct { get; set; }
        public int Category4Correct { get; set; }
        public int Category5Correct { get; set; }
    }
}
