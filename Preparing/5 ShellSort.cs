using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preparing
{
    public static class ShellSort
    {
        // n log n
        public static void Sort(int[] array)
        {
            int d = array.Length / 2;
            while (d >= 1)
            {
                for (int i = d; i < array.Length; i++)
                {
                    int j = i;
                    while (j >= d && array[j - d] > array[j])
                    {
                        int temp = array[j - d];
                        array[j - d] = array[j];
                        array[j] = temp;
                        j -= d;
                    }
                }
                d /= 2;
            }
        }
    }
}
