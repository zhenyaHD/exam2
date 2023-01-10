using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preparing
{
    public static class InsertSort
    {
        public static void Sort(int[] array)
        {
            for (int i = 1; i < array.Length; i++)
            {
                int cur = array[i];
                int j = i - 1;

                while (j >= 0 && array[j] > cur)
                {
                    array[j + 1] = array[j];
                    array[j] = cur;
                    j--;
                }
            }
        }
    }
}
