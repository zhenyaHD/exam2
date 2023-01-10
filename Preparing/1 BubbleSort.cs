using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preparing
{
    public static class BubbleSort
    {
        public static void Sort(int[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                bool breakFlag = true;
                for (int j = 0; j < array.Length - (i + 1); j++)
                {
                    if (array[j] > array[j + 1])
                    {
                        int temp = array[j];
                        array[j] = array[j + 1];
                        array[j + 1] = temp;
                        breakFlag = false;
                    }
                }
                if (breakFlag) break;
            }
        }
    }
}
