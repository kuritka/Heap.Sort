using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace Heap.Test
{

    [TestFixture]
    partial class HeapTest
    {
        [Test]
        public void HeapSortTestOnSimpleSampleData()
        {
            var data = new[] { 1, 3, 6, 5, 9, 8,-2 };
            AssertData(data);
        }

        [Test]
        public void HeapSortTestOnCocoaMarketData()
        {
            var data = CoffeeWheat.TestData.Data.Cocoa.WeeklyData.Select(d => d.Close).ToArray();
            AssertData(data);
        }

        [Test]
        public void HeapSortTestOnJoinedMarketData()
        {
            var data = GetJoinedMarketData();
            AssertData(data);
        }


        [Test]
        public void HeapSortTestOnBigAscendingSortedList()
        {
            var data = GetLongSortedArray(500000);
            AssertData(data);
        }


        [Test]
        public void HeapSortTestOnBigUnsortedList()
        {
            var data = GetLongUnSortedArray(500000);
            AssertData(data);
        }


        [Test]
        public void HeapSortTestOnBigDescendingSortedList()
        {
            var data = GetLongUnSortedArray(500000);
            data.Reverse();
            AssertData(data);
        }


        [Test]
        public void HeapSortTestOnOneElement()
        {
            var data = new[] { Int32.MinValue};
            AssertData(data);
        }

        [Test]
        public void HeapSortTestOnTwoElements()
        {
            var data = new[] { Int32.MinValue,1 };
            AssertData(data);
            data = new[] { 1,Int32.MinValue };
            AssertData(data);
        }


        [Test]
        public void HeapSortTestOnEmptyList()
        {
            AssertData(new List<int>());
        }

        [Test]
        public void HeapSortTestOnNullList()
        {
            var heap = new Heap<decimal>(null);
            var sortedData = heap.Sort();
            Assert.AreEqual(sortedData, new List<decimal>());
        }

        [Test]
        public void MedianOnEmptyCollectionTest()
        {
            var rtp = new RealTimeProcessor<int>();
            rtp.Median().Should().Be(0);
        }


        [Test]
        public void MedianOnOneElementTest()
        {
            var rtp = new RealTimeProcessor<int>();
            rtp.Insert(-100);
            rtp.Median().Should().Be(-100);
        }


        [Test]
        public void MedianOnTwoElementTest()
        {
            var rtp = new RealTimeProcessor<int>();
            rtp.Insert(100);
            rtp.Insert(-100);
            rtp.Median().Should().Be(100);
            rtp = new RealTimeProcessor<int>();
            rtp.Insert(-100);
            rtp.Insert(100);
            rtp.Median().Should().Be(100);
        }

        [Test]
        public void MedianOnThreeElementTest()
        {
            var rtp = new RealTimeProcessor<int>();
            rtp.Insert(100);
            rtp.Insert(-100);
            rtp.Insert(50);
            rtp.Median().Should().Be(50);
        }

        [Test]
        public void MedianOnCocoaMarketTest()
        {
            var data = CoffeeWheat.TestData.Data.Cocoa.WeeklyData.Select(d => d.Close).ToArray();
            var rtp = new RealTimeProcessor<decimal>(data);
            rtp.Median().Should().Be(data.OrderByDescending(d=>d).ElementAt((data.Length-1) / 2));
        }


        [Test]
        public void MedianOnJoinedMarketsTest()
        {
            var data = GetJoinedMarketData(); 
            var rtp = new RealTimeProcessor<decimal>(data);
            rtp.Median().Should().Be(data.OrderByDescending(d => d).ElementAt((data.Count - 1) / 2));
        }

    }
}
