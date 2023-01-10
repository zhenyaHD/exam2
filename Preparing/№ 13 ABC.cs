using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preparing
{

    public class ABC
    {
        List<string> Sorted = new List<string>(); // итоговый отсортированный массив
        string[] Start; // сюда будет записан изначальный массив, но все его элементы будут в верхнем регистре
        int[] Links; // массив, содержащий указатели на какие-то элементы
        List<int[]> table = new List<int[]>(); // таблица с уровнями
        string[] RawArray;

        public List<string> Sort(string[] arr)
        {
            Start = ToUpper(arr);
            int[] first = CreateMinusesArray();
            table.Add(first);
            Links = new int[arr.Length];
            for (int i = 0; i < Links.Length; i++) Links[i] = -1;
            RawArray = arr;
            CreateFirstLevel();
            ClearLevel(0);
            return Sorted;
        }

        public void CreateFirstLevel()
        {
            for (int i = 0; i < Start.Length; i++)
            {
                int ind = Start[i][0] - 65;
                Links[i] = table[0][ind];
                table[0][ind] = i;
            }
        }

        public void ClearLevel(int level)
        {
            for (int i = 0; i < 26; i++)
            {
                if (table[level][i] != -1)
                {
                    MarkChain(table[level][i], level + 1);
                    table[level][i] = -1;
                }
            }
            table.RemoveAt(level);
        }

        public void MarkChain(int index, int newLevelIndex)
        {
            if (Links[index] == -1)
            {
                Sorted.Add(RawArray[index]);
            }
            else
            {
                table.Add(CreateMinusesArray());
                while (Links[index] != -1)
                {
                    if (Start[index].Length == newLevelIndex)
                    {
                        Sorted.Add(RawArray[index]);
                        int l = Links[index];
                        Links[index] = -1;
                        index = l;
                    }
                    else
                    {
                        int l = Links[index];
                        Links[index] = table[newLevelIndex][Start[index][newLevelIndex] - 65];
                        table[newLevelIndex][Start[index][newLevelIndex] - 65] = index;
                        index = l;
                    }

                }
                if (Start[index].Length == newLevelIndex)
                {
                    Sorted.Add(RawArray[index]);
                    int l = Links[index];
                    Links[index] = -1;
                    index = l;
                }
                else
                {
                    int link = Links[index];
                    Links[index] = table[newLevelIndex][Start[index][newLevelIndex] - 65];
                    table[newLevelIndex][Start[index][newLevelIndex] - 65] = index;
                }
                ClearLevel(newLevelIndex);
            }
        }
        public int[] CreateMinusesArray()
        {
            int[] arr = new int[26];
            for (int i = 0; i < arr.Length; i++) 
                arr[i] = -1;

            return arr;
        }

        public string[] ToUpper(string[] arr)
        {
            string[] result = new string[arr.Length];
            for (int i = 0; i < arr.Length; i++)
            {
                result[i] = arr[i].ToUpper();
            }
            return result;
        }
    }
}