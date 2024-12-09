using System;
using System.IO;
using System.Text.Json;

namespace OrderClassLibrary
{
	public class JSONHandler
	{
        private const string JsonDirectory = "SavedOrders";

        public JSONHandler()
        {
            if (!Directory.Exists(JsonDirectory))
            {
                Directory.CreateDirectory(JsonDirectory);
            }
        }

        public void SaveOrderToJSON(Order order)
        {
            if (order == null)
            {
                throw new ArgumentNullException(nameof(order), "Order cannot be null.");
            }

            string json = JsonSerializer.Serialize(order, new JsonSerializerOptions { WriteIndented = true });
            string filePath = Path.Combine(JsonDirectory, $"Order_{order.OrderNumber}.json");

            File.WriteAllText(filePath, json);
            Console.WriteLine($"Order {order.OrderNumber} saved to JSON at {filePath}");
        }

        public void PrintJSON(int orderNumber)
        {
            string filePath = Path.Combine(JsonDirectory, $"Order_{orderNumber}.json");

            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                Console.WriteLine($"Order {orderNumber} JSON data:\n{json}");
            }
            else
            {
                Console.WriteLine($"JSON file for Order {orderNumber} not found.");
            }
        }
    }
}

