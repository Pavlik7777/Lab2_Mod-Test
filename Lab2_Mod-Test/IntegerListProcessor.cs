using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
//Задание 4
namespace Lab2_Mod_Test
{
    public class IntegerListProcessor
    {
        public List<int> Numbers { get; private set; }

        public IntegerListProcessor(IEnumerable<int> numbers)
        {
            if (numbers == null)
                throw new ArgumentNullException(nameof(numbers), "Коллекция не может быть null.");

            Numbers = new List<int>(numbers);
        }

        public int RemoveOutliers(double threshold)
        {
            if (threshold < 0)
                throw new ArgumentOutOfRangeException(nameof(threshold), "Предельная величина не может быть отрицательной.");
            if (Numbers.Count == 0)
                throw new InvalidOperationException("Невозможно вычислить среднее для пустого списка.");

            double average = Numbers.Average();
            int removedCount = Numbers.RemoveAll(x => Math.Abs(x - average) > threshold);
            return removedCount;
        }

        public void SortDescending()
        {
            Numbers.Sort((a, b) => b.CompareTo(a));
        }
    }

    [TestClass]
    public class IntegerListProcessorTests
    {
        [TestMethod]
        public void Test1_RemoveOutliers_RemovesExpectedElements()
        {
            var processor = new IntegerListProcessor(new List<int> { 2, 8, 10, 12, 18 });

            int removed = processor.RemoveOutliers(3.0);

            Assert.AreEqual(2, removed);
            CollectionAssert.AreEqual(new List<int> { 8, 10, 12 }, processor.Numbers);
        }

        [TestMethod]
        public void Test2_BoundaryDeviation_KeepsElements()
        {
            var processor = new IntegerListProcessor(new List<int> { 5, 10, 15 });

            int removed = processor.RemoveOutliers(5.0);

            Assert.AreEqual(0, removed);
            CollectionAssert.AreEqual(new List<int> { 5, 10, 15 }, processor.Numbers);
        }

        [TestMethod]
        public void Test3_AllElementsExceedThreshold_EmptiesList()
        {
            var processor = new IntegerListProcessor(new List<int> { 1, 100 });

            int removed = processor.RemoveOutliers(10.0);

            Assert.AreEqual(2, removed);
            Assert.AreEqual(0, processor.Numbers.Count);
        }

        [TestMethod]
        public void Test4_SortDescending_SortsCorrectly()
        {
            var processor = new IntegerListProcessor(new List<int> { 4, 15, -2, 8, 15 });

            processor.SortDescending();

            CollectionAssert.AreEqual(new List<int> { 15, 15, 8, 4, -2 }, processor.Numbers);
        }

        [TestMethod]
        public void Test5_SortEmptyList_RemainsEmpty()
        {
            var processor = new IntegerListProcessor(new List<int>());

            processor.SortDescending();

            Assert.AreEqual(0, processor.Numbers.Count);
        }

        [TestMethod]
        public void Test6_NegativeThreshold_ThrowsException()
        {
            var processor = new IntegerListProcessor(new List<int> { 1, 2, 3 });
            Assert.ThrowsExactly<ArgumentOutOfRangeException>(() => processor.RemoveOutliers(-1.0));
        }

        [TestMethod]
        public void Test7_RemoveOutliersOnEmptyList_ThrowsException()
        {
            var processor = new IntegerListProcessor(new List<int>());
            Assert.ThrowsExactly<InvalidOperationException>(() => processor.RemoveOutliers(5.0));
        }

        [TestMethod]
        public void Test8_NullCollection_ThrowsException()
        {
            Assert.ThrowsExactly<ArgumentNullException>(() => new IntegerListProcessor(null));
        }
    }
}
