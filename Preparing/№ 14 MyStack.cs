using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preparing
{
    public class MyStack<T>
    {
        private MyList<T> elems = new MyList<T>();

        public void Push(T value)
        {
            elems.Add(value);
        }

        public T Pop()
        {
            T value = elems.GetLast();
            elems.RemoveLast();
            return value;
        }

        public T Peek()
        {
            return elems.GetLast();
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
