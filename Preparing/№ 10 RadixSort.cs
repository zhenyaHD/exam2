using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preparing
{
    // n * k/d
    //k - количество разрядов в самом длинном ключе
    //d - разрядность данных: количество возможных значений разряда ключа
    public static class RadixSort
    {
        private static int GetMaxValue(int[] array)
        {
            int max = array[0];

            for (int i = 1; i < array.Length; i++)
                if (array[i] > max)
                    max = array[i];

            return max;
        }

        public static void Sort(int[] array)
        {
            int max = GetMaxValue(array);

            for (int exponent = 1; max / exponent > 0; exponent *= 10)
                CountingSort(array, exponent);
        }

        public static void CountingSort(int[] array, int exponent)
        {
            int[] outputArr = new int[array.Length];
            int[] occurences = new int[10];

            for (int i = 0; i < 10; i++)
                occurences[i] = 0;

            for (int i = 0; i < array.Length; i++)
                occurences[(array[i] / exponent) % 10]++;

            for (int i = 1; i < 10; i++)
                occurences[i] += occurences[i - 1];

            for (int i = array.Length - 1; i >= 0; i--)
            {
                outputArr[occurences[(array[i] / exponent) % 10] - 1] = array[i];
                occurences[(array[i] / exponent) % 10]--;
            }

            for (int i = 0; i < array.Length; i++)
                array[i] = outputArr[i];
        }
    }
}
