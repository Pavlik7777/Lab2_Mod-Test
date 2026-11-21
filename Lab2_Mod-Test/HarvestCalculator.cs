using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Lab2_Mod_Test
{
    public class HarvestCalculator
    {
        public double CalculateYield(double yieldPerSqMeter, double width, double length, int precision)
        {
            if (yieldPerSqMeter < 0)
                throw new ArgumentOutOfRangeException(nameof(yieldPerSqMeter), "Урожайность не может быть отрицательной.");
            if (width < 0)
                throw new ArgumentOutOfRangeException(nameof(width), "Ширина грядки не может быть отрицательной.");
            if (length < 0)
                throw new ArgumentOutOfRangeException(nameof(length), "Длина грядки не может быть отрицательной.");
            if (precision < 0 || precision > 15)
                throw new ArgumentOutOfRangeException(nameof(precision), "Точность должна быть в диапазоне от 0 до 15.");

            double totalYield = yieldPerSqMeter * width * length;
            return Math.Round(totalYield, precision, MidpointRounding.AwayFromZero);
        }
    }

    [TestClass]
    public class HarvestCalculatorTests
    {
        private HarvestCalculator _calculator;

        [TestInitialize]
        public void Setup() => _calculator = new HarvestCalculator();

        [TestMethod]
        public void Test1_StandardValues_ReturnsRoundedYield()
        {
            Assert.AreEqual(30.0, _calculator.CalculateYield(2.5, 3.0, 4.0, 2));
        }

        [TestMethod]
        public void Test2_FractionalValues_Precision3_ReturnsExpected()
        {
            Assert.AreEqual(9.564, _calculator.CalculateYield(1.234, 2.5, 3.1, 3));
        }

        [TestMethod]
        public void Test3_ZeroPrecision_RoundsToInteger()
        {
            Assert.AreEqual(15.0, _calculator.CalculateYield(3.7, 2.0, 2.0, 0));
        }

        [TestMethod]
        public void Test4_ZeroYield_ReturnsZero()
        {
            Assert.AreEqual(0.0, _calculator.CalculateYield(0.0, 5.0, 10.0, 2));
        }

        [TestMethod]
        public void Test5_NegativeYield_ThrowsException()
        {
            Assert.ThrowsExactly<ArgumentOutOfRangeException>(() => _calculator.CalculateYield(-2.5, 3.0, 4.0, 2));
        }

        [TestMethod]
        public void Test6_NegativeWidth_ThrowsException()
        {
            Assert.ThrowsExactly<ArgumentOutOfRangeException>(() => _calculator.CalculateYield(2.5, -3.0, 4.0, 2));
        }

        [TestMethod]
        public void Test7_NegativePrecision_ThrowsException()
        {
            Assert.ThrowsExactly<ArgumentOutOfRangeException>(() => _calculator.CalculateYield(2.5, 3.0, 4.0, -1));
        }
    }
}
