using GorselProg.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GorselProg.Objects
{
    public class SummaryGame
    {
        public User FirstUser { get; set; }
        public User SecondUser { get; set; }
        public User ThirdUser { get; set; }
        public int Category1Correct { get; set; }
        public int Category2Correct { get; set; }
        public int Category3Correct { get; set; }
        public int Category4Correct { get; set; }
        public int Category5Correct { get; set; }

        public int SumXP { get; set; }
        public int Level { get; set; }

        public bool isLevelUp { get; set; }
    }
}
