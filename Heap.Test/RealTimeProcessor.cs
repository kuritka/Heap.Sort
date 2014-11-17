using System;
using System.Collections.Generic;
using System.Linq;


namespace Heap.Test
{
    public class RealTimeProcessor<T>  where T : IComparable<T>
    {
        private readonly Heap<T> _heap;

        public RealTimeProcessor()
        {
            _heap = new Heap<T>();
        }

        public RealTimeProcessor(IEnumerable<T> items)
        {
            _heap = new Heap<T>(items);
        }


        public void Insert(T item)
        {
            _heap.Add(item);
        }


        public T Median()
        {
            var sortedData = _heap.Sort().ToArray();
            return sortedData.ElementAtOrDefault((sortedData.Length-1) / 2);
        }

    }

   
}
