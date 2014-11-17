using System;
using System.Collections.Generic;


namespace Heap.Test
{
    
    public class Heap<T> where T : IComparable<T>
    {
        readonly List<T> _heap ;

        public int Steps = 0;

        public Heap()
        {
            _heap = new List<T>();
        }


        public Heap(IEnumerable<T> array) : this()
        {
            if (array == null)
            {
                return;
            }
            foreach (var item in array)
            {
                Add(item);
            }
        }

        public void Add(T item)
        {
            _heap.Add(item);

            var size = _heap.Count - 1;

            var i = size;

            while (i > 1)
            {
                var nextItem = i / 2;
                if (item.CompareTo(_heap[nextItem]) < 0)
                    Swap(i, nextItem);
                i = nextItem;
            }
        }

        public int Count
        {
            get { return _heap.Count; }
        }


        public IEnumerable<T> Sort()
        {
            Steps = 0;
            for (var i = _heap.Count / 2 - 1; i >= 0; i--)
            {
                FixTop(_heap.Count - 1, i);
            }
            for (var i = _heap.Count- 1; i > 0; i--)
            {
                Swap(i, 0);
                FixTop( i - 1, 0);
            }
            return _heap;
        }



        private void FixTop(int bottom, int topIndex)
        {
            var tmp = _heap[topIndex];
            var succ = topIndex * 2 + 1;
            if (succ < bottom && _heap[succ].CompareTo(_heap[succ + 1]) > 0) succ++;

            while (succ <= bottom && tmp.CompareTo(_heap[succ]) > 0)
            {
                Steps++;
                _heap[topIndex] = _heap[succ];
                topIndex = succ;
                succ = succ * 2 + 1;
                if (succ < bottom && _heap[succ].CompareTo(_heap[succ + 1]) > 0) succ++;
            }
            _heap[topIndex] = tmp;
        }

        
        private void Swap(int position1, int position2)
        {
            var temp = _heap[position2];
            _heap[position2] = _heap[position1];
            _heap[position1] = temp;
        }
    }
}
