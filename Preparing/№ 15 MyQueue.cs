using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preparing
{
    public class MyQueue<T>
    {
        private MyList<T> elems = new MyList<T>();

        public void Enqueue(T value)
        {
            elems.Add(value);
        }

        public T Dequeue()
        {
            T value = elems.GetFirst();
            elems.RemoveFirst();
            return value;
        }

        public T Peek()
        {
            return elems.GetFirst();
        }

        public void Clear()
        {
            elems = new MyList<T>();
        }

        public bool IsEmpty()
        {
            return elems.Count == 0;
        }

        public bool Contains(T value)
        {
            return elems.Contains(value);
        }
    }
}
