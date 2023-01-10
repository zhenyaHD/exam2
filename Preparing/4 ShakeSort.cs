using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preparing
{
    public static class ShakeSort
    {
        public static void Sort(int[] array)
        {
            for (int i = 0; i < array.Length / 2; i++)
            {
                bool breakFlag = true;

                for (int j = i; j < array.Length - 1 - i; j++)
                {
                    if (array[j] > array[j + 1])
                    {
                        int temp = array[j];
                        array[j] = array[j + 1];
                        array[j + 1] = temp;
                        breakFlag = false;
                    }
                }

                for (int j = array.Length - 2 - i; j > i; j--)
                {
                    if (array[j - 1] > array[j])
                    {
                        int temp = array[j - 1];
                        array[j - 1] = array[j];
                        array[j] = temp;
                        breakFlag = false;
                    }
                }

                if (breakFlag) break;
            }
        }
    }
}
