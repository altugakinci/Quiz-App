using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GorselProg
{
    public static class Helper
    {
        public static string ConcatenateStrings(params string[] strings)
        {
            return string.Join("\n", strings);
        }

        public static string[] SplitString(string input)
        {
            return input.Split('\n');
        }

        public static int FindFirstTrueIndex(bool[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i])
                {
                    return i;
                }
            }

            return -1; // Eğer true eleman bulunamazsa -1 döndürülür
        }
    }
}
