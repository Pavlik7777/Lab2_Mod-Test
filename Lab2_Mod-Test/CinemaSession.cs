using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Lab2_Mod_Test
{
    public class CinemaSession
    {
        public int Rows { get; }
        public int SeatsPerRow { get; }
        public int[,] SeatsMatrix { get; }
        public decimal TicketPrice { get; }

        public CinemaSession(int rows, int seatsPerRow, int[,] seatsMatrix, decimal ticketPrice)
        {
            if (rows <= 0)
                throw new ArgumentOutOfRangeException(nameof(rows), "Число рядов должно быть больше 0.");
            if (seatsPerRow <= 0)
                throw new ArgumentOutOfRangeException(nameof(seatsPerRow), "Число мест в ряду должно быть больше 0.");
            if (ticketPrice < 0)
                throw new ArgumentOutOfRangeException(nameof(ticketPrice), "Цена билета не может быть отрицательной.");
            if (seatsMatrix == null)
                throw new ArgumentNullException(nameof(seatsMatrix));
            if (seatsMatrix.GetLength(0) != rows || seatsMatrix.GetLength(1) != seatsPerRow)
                throw new ArgumentException("Размеры матрицы не соответствуют числу рядов и мест.");

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < seatsPerRow; j++)
                {
                    if (seatsMatrix[i, j] != 0 && seatsMatrix[i, j] != 1)
                        throw new ArgumentException("Матрица может содержать только значения 0 и 1.");
                }
            }

            Rows = rows;
            SeatsPerRow = seatsPerRow;
            SeatsMatrix = (int[,])seatsMatrix.Clone();
            TicketPrice = ticketPrice;
        }

        public int GetFreeSeatsCount()
        {
            int freeCount = 0;
            for (int i = 0; i < Rows; i++)
                for (int j = 0; j < SeatsPerRow; j++)
                    if (SeatsMatrix[i, j] == 0) freeCount++;
            return freeCount;
        }

        public double GetOccupiedPercentage()
        {
            int totalSeats = Rows * SeatsPerRow;
            int occupiedSeats = totalSeats - GetFreeSeatsCount();
            return Math.Round((double)occupiedSeats / totalSeats * 100.0, 2);
        }

        public decimal CalculateRevenue()
        {
            int totalSeats = Rows * SeatsPerRow;
            int occupiedSeats = totalSeats - GetFreeSeatsCount();
            return occupiedSeats * TicketPrice;
        }
    }

    [TestClass]
    public class CinemaSessionTests
    {
        [TestMethod]
        public void Test1_HalfOccupied_ReturnsCorrectFreeSeats()
        {
            int[,] matrix = { { 1, 0, 1 }, { 0, 1, 0 } };
            var session = new CinemaSession(2, 3, matrix, 500m);

            Assert.AreEqual(3, session.GetFreeSeatsCount());
        }

        [TestMethod]
        public void Test2_HalfOccupied_Returns50Percent()
        {
            int[,] matrix = { { 1, 0, 1 }, { 0, 1, 0 } };
            var session = new CinemaSession(2, 3, matrix, 500m);

            Assert.AreEqual(50.0, session.GetOccupiedPercentage());
        }

        [TestMethod]
        public void Test3_HalfOccupied_ReturnsCorrectRevenue()
        {
            int[,] matrix = { { 1, 0, 1 }, { 0, 1, 0 } };
            var session = new CinemaSession(2, 3, matrix, 500m);

            Assert.AreEqual(1500m, session.CalculateRevenue());
        }

        [TestMethod]
        public void Test4_EmptyHall_ReturnsZeroRevenueAndPercent()
        {
            int[,] matrix = { { 0, 0 }, { 0, 0 } };
            var session = new CinemaSession(2, 2, matrix, 400m);

            Assert.AreEqual(4, session.GetFreeSeatsCount());
            Assert.AreEqual(0.0, session.GetOccupiedPercentage());
            Assert.AreEqual(0m, session.CalculateRevenue());
        }

        [TestMethod]
        public void Test5_FullHall_Returns100PercentAndFullRevenue()
        {
            int[,] matrix = { { 1, 1 }, { 1, 1 } };
            var session = new CinemaSession(2, 2, matrix, 400m);

            Assert.AreEqual(0, session.GetFreeSeatsCount());
            Assert.AreEqual(100.0, session.GetOccupiedPercentage());
            Assert.AreEqual(1600m, session.CalculateRevenue());
        }

        [TestMethod]
        public void Test6_MismatchedMatrixSize_ThrowsException()
        {
            int[,] matrix = { { 1, 0 }, { 0, 1 } };
            Assert.ThrowsExactly<ArgumentException>(() => new CinemaSession(2, 3, matrix, 500m));
        }

        [TestMethod]
        public void Test7_NegativePrice_ThrowsException()
        {
            int[,] matrix = { { 0, 1 } };
            Assert.ThrowsExactly<ArgumentOutOfRangeException>(() => new CinemaSession(1, 2, matrix, -100m));
        }

        [TestMethod]
        public void Test8_InvalidMatrixElement_ThrowsException()
        {
            int[,] matrix = { { 0, 2 } };
            Assert.ThrowsExactly<ArgumentException>(() => new CinemaSession(1, 2, matrix, 500m));
        }
    }
}
