using CalcClassBr;
using System.Data.SqlClient;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace testing
{
    [TestClass]
    public class UnitTest1
    {
        // Рядок підключення для SQL Server
        string connectionString = "Server=localhost;Database=testing;Trusted_Connection=True;";


        [TestMethod]
        public void TestCorrectSubMethod()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT a, b, expected FROM SubtractionTests", connection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    // Arrange: Отримання даних з бази
                    long a = Convert.ToInt64(reader["a"]);
                    long b = Convert.ToInt64(reader["b"]);
                    int expected = Convert.ToInt32(reader["expected"]);

                    // Act: Виклик методу Sub
                    int actual = CalcClass.Sub(a, b);

                    // Assert: Перевірка результату
                    Assert.AreEqual(expected, actual, "The subtraction result is incorrect.");
                }

                //int res = CalcClass.Sub(5,10);
                //Assert.AreEqual(-6, res, "The subtraction result is incorrect.");

                reader.Close();
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestCorrectArguments()
        {
            int expected = CalcClass.Sub((long)int.MaxValue + 1,0);
            Assert.AreEqual(0,expected);
            expected = CalcClass.Sub(0,(long)int.MinValue - 1);
            Assert.AreEqual(0, expected);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestCorrectResult()
        {
            int expected = CalcClass.Sub((long)int.MaxValue, -1);
            Assert.AreEqual(0, expected);
            expected = CalcClass.Sub((long)int.MinValue, 1);
            Assert.AreEqual(0, expected);
        }
    }
}
