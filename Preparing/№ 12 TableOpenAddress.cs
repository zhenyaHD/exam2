using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preparing
{
    public class TableOpenAddress
    {
        public Dictionary<int, int> clusters = new Dictionary<int, int>();
        public Dictionary<int, int> table = new Dictionary<int, int>();

        public int Find(int key, int value, int type)
        {
            if (type == 0)
            {
                int hash = HashFunctions.UsualHash(value);
                for (int i = hash % 10000; i < 10000; i++)
                {
                    if (i > 9999) i %= 10000;
                    if (table.ContainsKey(i))
                    {
                        int k = table[i];
                        if (k == value) return i;
                    }
                }
                return -1;
            }
            else if (type == 1)
            {
                int hash = HashFunctions.UsualHash(key);
                int c = 1;
                for (int i = hash % 10000; i < 10000; c *= c)
                {
                    if (i > 9999) i = i % 10000;
                    if (table.ContainsKey(i))
                    {
                        int k = table[i];
                        if (k == value) return i;
                    }
                    else
                    {
                        i = i + c;
                        continue;
                    }
                }
                return -1;
            }
            else if (type == 2)
            {
                int hash = HashFunctions.Bebra(key);
                int add = HashFunctions.ODoubleHashAdditional(key);
                for (int i = 0; i < 10000; i++)
                {
                    if (table.ContainsKey(hash))
                    {
                        int k = table[hash];
                        if (k == value) return hash;
                    }
                    else if (table.ContainsKey(hash + i * add))
                    {
                        int k = table[hash + i * add];
                        if (k == value) return hash + i * add;
                    }
                }
                return -1;
            }
            return 0;
        }

        public void Insert(int key, int value, int type)
        {
            if (type == 0)
            {
                int hash = HashFunctions.UsualHash(key);
                int count = 1;
                for (int i = hash % 10000; i < 10000; i++)
                {
                    if (i > 9999) i = i % 10000;
                    if (table.ContainsKey(i))
                    {
                        count++;
                        continue;
                    }
                    else
                    {
                        table.Add(i, value);
                        clusters.Add(i, count);
                        break;
                    }
                }
            }
            else if (type == 1)
            {
                int hash = HashFunctions.UsualHash(key);
                int count = 1;
                int c = 1;
                int d = hash % 10000;
                for (int i = hash % 10000; i < 10000; count++)
                {
                    if (i > 9999) i = i % 10000;
                    if (table.ContainsKey(d + (int)Math.Pow(c, 2)))
                    {
                        i = d + (int)Math.Pow(c, 2);
                        c++;
                        continue;
                    }
                    else
                    {
                        table.Add(d + (int)Math.Pow(c, 2), value);
                        clusters.Add(d + (int)Math.Pow(c, 2), count);
                        break;
                    }
                }

            }
            else if (type == 2)
            {
                int hash = HashFunctions.Bebra(key);
                int add = HashFunctions.ODoubleHashAdditional(key);
                int count = 1;
                for (int i = 0; i < 10000; i++)
                {
                    if (!table.ContainsKey(hash))
                    {
                        table.Add(hash, value);
                        clusters.Add(hash, count);
                        return;
                    }
                    else if (!table.ContainsKey(hash + i * add))
                    {
                        table.Add(hash + i * add, value);
                        clusters.Add(hash + i * add, count);
                        return;
                    }
                    count++;
                }
                throw new IndexOutOfRangeException();
            }
        }

        public int Delete(int key, int value, int type)
        {
            if (type == 0)
            {
                int hash = HashFunctions.UsualHash(key);
                for (int i = hash % 10000; i < 10000; i++)
                {
                    if (i > 9999) i = i % 10000;
                    if (table.ContainsKey(i))
                    {
                        int k = table[i];
                        if (k == value)
                        {
                            table.Remove(i);
                            clusters.Remove(i);
                        }
                        return i;
                    }
                    else continue;
                }
                return -1;
            }
            else if (type == 1)
            {
                int hash = HashFunctions.UsualHash(key);
                int c = 1;
                for (int i = hash % 10000; i < 10000; c *= c)
                {
                    if (i > 9999) i = i % 10000;
                    if (table.ContainsKey(i))
                    {
                        int k = table[i];
                        if (k == value)
                        {
                            table.Remove(i);
                            clusters.Remove(i);
                        }
                        return i;
                    }
                    else
                    {
                        i = i + c;
                        continue;
                    }
                }
                return -1;
            }
            else if (type == 2)
            {
                int hash = HashFunctions.Bebra(key);
                int add = HashFunctions.ODoubleHashAdditional(key);
                for (int i = 0; i < 10000; i++)
                {
                    if (table.ContainsKey(hash))
                    {
                        int k = table[hash];
                        if (k == value)
                        {
                            table.Remove(hash);
                            clusters.Remove(hash);
                        }
                        return hash;
                    }
                    else if (table.ContainsKey(hash + i * add))
                    {
                        int k = table[hash + i * add];
                        if (k == value)
                        {
                            table.Remove(hash + i * add);
                            clusters.Remove(hash + i * add);
                        }
                        return hash + i * add;
                    }
                }
                return -1;
            }
            return 0;
        }
    }
}
