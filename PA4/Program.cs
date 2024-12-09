using System;
using System.Collections.Generic;
using OrderClassLibrary;

namespace Driver
{
    class Program
    {
        static void Main(string[] args)
        {
            Order order = new Order
            {
                OrderNumber = 1000,
                OrderDate = new DateTime(2025, 10, 25),
                CustomerName = "Vivian Wu",
                CustomerPhone = "253-312-4578"
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

            order.CalculateTotals();

            Console.WriteLine($"Order Number: {order.OrderNumber}");
            Console.WriteLine($"Customer Name: {order.CustomerName}");
            Console.WriteLine($"Total Amount: {order.TotalAmount:C}");
            Console.WriteLine($"Tax Amount: {order.TaxAmount:C}");


            Console.WriteLine("\n\nTest Write Order to Database");
            var orderHandler = new OrderHandler();
            if (orderHandler.ProcessorOrder(order))
            {
                orderHandler.exportOrder(order, "database");
            }


            Console.WriteLine("\n\nTest Write Order to JSON");
            if (orderHandler.ProcessorOrder(order))
            {
                orderHandler.exportOrder(order, "JSON");
            }

        }
    }
}
