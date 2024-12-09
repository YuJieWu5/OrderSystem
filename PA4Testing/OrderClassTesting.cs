using NUnit.Framework;
using OrderClassLibrary;
using System;

namespace PA4Testing;
[TestFixture]
public class OrderTests
{
    [Test]
    public void TestOrderCalculation()
    {
        // Arrange
        Order order = new Order
        {
            OrderNumber = 1000,
            OrderDate = DateTime.Now,
            CustomerName = "Vivian Wu",
            CustomerPhone = "206-771-5555"
        };

        order.DetailItems.Add(new DetailItem
        {
            OrderNumber = 1000,
            DetailNumber = 1,
            StockID = "ELECT001",
            StockName = "42 Inch TV",
            StockPrice = 300.00M,
            Quantity = 1
        });

        order.DetailItems.Add(new DetailItem
        {
            OrderNumber = 1000,
            DetailNumber = 2,
            StockID = "ELECT044",
            StockName = "Battery",
            StockPrice = 50.00M,
            Quantity = 1
        });

        // Act
        order.CalculateTotals();

        // Assert
        Assert.AreEqual(404.25M, order.TotalAmount);
        Assert.AreEqual(36.75M, order.TaxAmount);
    }
}
