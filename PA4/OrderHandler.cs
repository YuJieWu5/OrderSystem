using System;

namespace OrderClassLibrary
{
	public class OrderHandler
	{
		public bool ProcessorOrder(Order order)
		{
			if(order == null)
			{
				throw new ArgumentException(nameof(order), "order cannot be null.");
			}

			//calculate total amount
			order.CalculateTotals();

            // Error handling for customer information
            if (string.IsNullOrEmpty(order.CustomerName) || string.IsNullOrEmpty(order.CustomerPhone))
            {
                Console.WriteLine("Order validation failed: Missing customer information.");
                return false;
            }

            Console.WriteLine("Order processed successfully.");
            return true;
        }

		public void exportOrder(Order order, string saveType)
		{
			if (order == null)
			{
				throw new ArgumentException(nameof(order), "Order cannot be null.");
			}

			switch (saveType)
			{
				case "database":
                    var dbHandler = new DatabaseHandler();
                    dbHandler.SaveOrder(order);
                    dbHandler.PrintOrders();
                    break;

                case "JSON":
                    var jsonHandler = new JSONHandler();
                    jsonHandler.SaveOrderToJSON(order);
                    jsonHandler.PrintJSON(order.OrderNumber);
                    break;

                default:
                    throw new ArgumentException($"Invalid save type: {saveType}", nameof(saveType));
            }

            Console.WriteLine($"Order saved to {saveType} successfully.");
        }
	}
}

