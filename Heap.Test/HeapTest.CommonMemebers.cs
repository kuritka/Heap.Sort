using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using FluentAssertions;
using NUnit.Framework;

namespace Heap.Test
{
    public partial class HeapTest
    {

        private CultureInfo _culture;

        [TestFixtureSetUp]
        public void Init()
        {
            _culture = Thread.CurrentThread.CurrentCulture; 
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
        }

        [TestFixtureTearDown]
        public void Dispose()
        {
            Thread.CurrentThread.CurrentCulture = _culture;
        }


        private List<decimal> GetJoinedMarketData()
        {
            var data = new List<decimal>();
            foreach (var marketData in CoffeeWheat.TestData.Data.MarketContainer)
            {
                data.AddRange(marketData.WeeklyData.Select(d => d.Close));
            }
            return data;
        }

        private List<int> GetLongUnSortedArray(int length)
        {
            var rand = new Random();
            return Enumerable.Repeat(0, length).Select(i => rand.Next(Int32.MinValue,Int32.MaxValue)).ToList();
        }

        private List<int> GetLongSortedArray(int length)
        {
            return Enumerable.Range(0, length).Select(i => 100 + 10 * i).ToList();
        }


        private void WriteResult<T>(Heap<T> heap, IEnumerable<T> data ) where T : IComparable<T>
        {
            if (data == null || !data.Any() )
                Console.WriteLine("count: 0; steps: 0; N*lg(N): 0; ratio: 0");
            else
            {
                var log = Math.Log(data.Count(), 2);
                Console.WriteLine("count: {0:N0}; steps: {1:N0}; N*lg(N): {2:N0}; ratio: {3:p}", data.Count(),
                    heap.Steps, data.Count()*log, heap.Steps/(data.Count()*log));

            }
        }


        private void AssertData<T>(IList<T> data) where T : IComparable<T>
        {
            var heap = new Heap<T>(data.ToArray());
            var sortedData = heap.Sort();
            Assert.AreEqual(sortedData, data.OrderByDescending(d => d));
            WriteResult(heap, data);
        }

        [Test]
        public void MedionOnSequentiallyAddedItems()
        {
            var silver = CoffeeWheat.TestData.Data.Silver.WeeklyData.Select(d => d.Close).ToList();
            var medians = new List<decimal>(silver.Count());
            var tempList = new List<decimal>();
            foreach (var close in silver)
            {
                tempList.Add(close);
                var median = tempList.OrderByDescending(d => d).ElementAt((tempList.Count - 1)/2);
                medians.Add(median);
            }
            var i = 0;
            var processor = new RealTimeProcessor<decimal>();
            silver.ForEach(d =>
                {
                    processor.Insert(d);
                    processor.Median().Should().Be(medians.ElementAt(i++));
                });
        }
   }
}

