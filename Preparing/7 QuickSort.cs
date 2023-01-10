using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preparing
{
    public static class QuickSort
    {
        public static void Sort(int[] array)
        {
            SortArray(array, 0, array.Length - 1);
        }

        private static void SortArray(int[] array, int minIndex, int maxIndex)
        {
            if (minIndex >= maxIndex)
            {
                return;
            }

            int pivotIndex = Partition(array, minIndex, maxIndex);
            SortArray(array, minIndex, pivotIndex - 1);
            SortArray(array, pivotIndex + 1, maxIndex);
        }

        private static int Partition(int[] array, int minIndex, int maxIndex)
        {
            int pivot = minIndex - 1;
            for (int i = minIndex; i < maxIndex; i++)
            {
                if (array[i] < array[maxIndex])
                {
                    pivot++;
                    Swap(ref array[pivot], ref array[i]);
                }
            }

            pivot++;
            Swap(ref array[pivot], ref array[maxIndex]);
            return pivot;
        }

        static void Swap(ref int x, ref int y)
        {
            int t = x;
            x = y;
            y = t;
        }
    }
}
