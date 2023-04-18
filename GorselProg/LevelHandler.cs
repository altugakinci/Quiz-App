using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GorselProg
{
    class LevelHandler
    {

        private static int level = 1;
        private static int xp = 0;

        //Level:XP
        //Örneğin level 7 olmak için önceki gereken XP:100+100+150+200+...+300
        //Her levelden sonra xp sıfırlanır.
        private static Dictionary<int, int> levels = new Dictionary<int, int>
        {
            {1,100},
            {2,100},
            {3,150}, 
            {4,200},
            {5,250},
            {6,300},
            {7,350},
            {8,400},
            {9,450},
            {10,500},
            {11,550},
            {12,600},
            {13,650},
        };

        public static int Level { get => level; set => level = value; }
        public static int Xp { get => xp; set => xp = value; }

        public static int calcLevel()
        {
            int total = 0;
            
            for(int i = 0; i < Level; i++)
            {
                total += levels[i];
            }

            return total;
        }
    }
}
