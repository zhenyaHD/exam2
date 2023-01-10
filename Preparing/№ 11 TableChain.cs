using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preparing
{
    public class Table
    {
        private class Pair
        {
            public int Key;
            public List<int> Value = new List<int>();
        }

        private Pair[] table = new Pair[1000];

        public void Insert(int elem)
        {
            int hash = CalculateHash(elem);
            if (table[hash] != null)
            {
                table[hash] = new Pair();
                table[hash].Key = hash;
                table[hash].Value.Insert(0, elem);
            }
            else table[hash].Value.Insert(0, elem);
        }

        public void Delete(int elem)
        {
            int hash = CalculateHash(elem);
            table[hash].Value.Remove(elem);
        }

        public void Find(int elem)
        {
            int hash = CalculateHash(elem);
            foreach (Pair pair in table)
            {
                if (pair != null)
                {
                    if (pair.Value.Contains(elem))
                    {
                        Console.WriteLine($"Элемент {elem} был найден с хеш-кодом {hash}");
                        return;
                    }
                }
            }
            Console.WriteLine("Элемент не был найден");
        }

        private int CalculateHash(int elem)
        {
            return HashFunctions.MyHash(elem);
        }
    }

    public static class HashFunctions
    {
        public static int UsualHash(int elem)
        {
            return elem % 1000;
        }

        public static int MyHash(int elem)
        {
            return (int)(Math.Sqrt(Math.Sqrt(elem)) * 10000 % 1000);
        }

        public static int GetSum(int elem)
        {
            int b = elem;
            int c = 0;
            string e = elem.ToString();
            for (int i = 0; i < e.Length; i++)
            {
                int t = b % 10;
                c += t;
                b /= 10;
            }
            return c;
        }

        public static int Bebra(int elem)
        {
            return Math.Abs(GetSum(elem) % 10000);
        }

        public static int ODoubleHashAdditional(int elem)
        {
            return Math.Abs(GetSum(elem) % (1000 - 1) + 1);
        }
    }
}
