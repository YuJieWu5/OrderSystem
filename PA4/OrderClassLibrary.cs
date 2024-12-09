//using System;
//using System.Collections.Generic;
//using System.Data.SQLite;

namespace OrderClassLibrary
{
    public class Order
    {
        public int OrderNumber { get; set; }
        public DateTime OrderDate { get; set; }
        public string CustomerName { get; set; }
        public string CustomerPhone { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal TariffAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public List<DetailItem> DetailItems { get; set; }

        public Order()
        {
            DetailItems = new List<DetailItem>();
        }

        public void AddItem(DetailItem item)
        {
            DetailItems.Add(item);
        }

        // Deep Copy Constructor
        public Order(Order other)
        {
            OrderNumber = other.OrderNumber;
            OrderDate = other.OrderDate;
            CustomerName = other.CustomerName;
            CustomerPhone = other.CustomerPhone;
            TaxAmount = other.TaxAmount;
            TariffAmount = other.TariffAmount;
            TotalAmount = other.TotalAmount;
            DetailItems = new List<DetailItem>();
            foreach (var item in other.DetailItems)
            {
                DetailItems.Add(new DetailItem(item));
            }
        }

        public void CalculateTotals()
        {
            decimal subtotal = 0;
            foreach (var item in DetailItems)
            {
                item.CalculateTariff();
                subtotal += item.StockPrice * item.Quantity + item.TariffAmount;
            }

            TaxAmount = subtotal * 0.10M;
            TotalAmount = subtotal + TaxAmount;
        }
    }

    public class DetailItem
    {
        public int OrderNumber { get; set; }
        public int DetailNumber { get; set; }
        public string StockID { get; set; }
        public string StockName { get; set; }
        public decimal StockPrice { get; set; }
        public int Quantity { get; set; }
        public decimal TariffAmount { get; set; }

        public DetailItem() { }

        // Deep Copy Constructor
        public DetailItem(DetailItem other)
        {
            OrderNumber = other.OrderNumber;
            DetailNumber = other.DetailNumber;
            StockID = other.StockID;
            StockName = other.StockName;
            StockPrice = other.StockPrice;
            Quantity = other.Quantity;
            TariffAmount = other.TariffAmount;
        }

        public void CalculateTariff()
        {
            if (StockID.StartsWith("ELECT", StringComparison.OrdinalIgnoreCase))
            {
                TariffAmount = StockPrice * Quantity * 0.05M;
            }
            else
            {
                TariffAmount = 0;
            }
        }
    }
}
