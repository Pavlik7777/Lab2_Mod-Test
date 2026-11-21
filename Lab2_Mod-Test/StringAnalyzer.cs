using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
//Задание 1
namespace Lab2_Mod_Test
{
    public class StringAnalyzer
    {
        public string FindShortestWord(string[] words)
        {
            if (words == null)
                throw new ArgumentNullException(nameof(words), "Массив не может быть null.");
            if (words.Length == 0)
                throw new ArgumentException("Массив не должен быть пустым.", nameof(words));

            string shortest = null;

            foreach (string word in words)
            {
                if (word == null)
                    throw new ArgumentException("Элемент массива не может быть null.", nameof(words));

                if (shortest == null || word.Length < shortest.Length)
                    shortest = word;
            }

            return shortest;
        }
    }
    [TestClass]
    public class StringAnalyzerTests
    {
        private StringAnalyzer _analyzer;

        [TestInitialize]
        public void Setup() => _analyzer = new StringAnalyzer();

        [TestMethod]
        public void Test1_DifferentLengths_ReturnsShortest()
        {
            string[] input = { "apple", "cat", "banana", "doggy" };
            Assert.AreEqual("cat", _analyzer.FindShortestWord(input));
        }
        [TestMethod]
        public void Test2_MultipleSameMinLength_ReturnsFirst()
        {
            string[] input = { "sun", "sky", "cloud", "sea" };
            Assert.AreEqual("sun", _analyzer.FindShortestWord(input));
        }
        [TestMethod]
        public void Test3_SingleElement_ReturnsElement()
        {
            string[] input = { "hello" };
            Assert.AreEqual("hello", _analyzer.FindShortestWord(input));
        }
        [TestMethod]
        public void Test4_ShortestAtEnd_ReturnsShortest()
        {
            string[] input = { "elephant", "giraffe", "ant" };
            Assert.AreEqual("ant", _analyzer.FindShortestWord(input));
        }
        [TestMethod]
        public void Test5_ContainsEmptyString_ReturnsEmptyString()
        {
            string[] input = { "test", "", "code" };
            Assert.AreEqual("", _analyzer.FindShortestWord(input));
        }
        [TestMethod]
        public void Test6_EmptyArray_ThrowsArgumentException()
        {
            Assert.ThrowsExactly<ArgumentException>(() => _analyzer.FindShortestWord(Array.Empty<string>()));
        }
        [TestMethod]
        public void Test7_NullArray_ThrowsArgumentNullException()
        {
            Assert.ThrowsExactly<ArgumentNullException>(() => _analyzer.FindShortestWord(null));
        }
        [TestMethod]
        public void Test8_ArrayWithNullElement_ThrowsArgumentException()
        {
            string[] input = { "word", null, "hi" };
            Assert.ThrowsExactly<ArgumentException>(() => _analyzer.FindShortestWord(input));
        }
    }
}
