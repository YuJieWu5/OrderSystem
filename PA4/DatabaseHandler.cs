using System;
//using System.Data.SQLite;
using Microsoft.Data.Sqlite;


namespace OrderClassLibrary
{
	
    public class DatabaseHandler
    {
        private const string ConnectionString = "Data Source=orders.db;";


        public DatabaseHandler()
        {
            SqliteConnection sqlite_conn;
            sqlite_conn = InitializeDatabase();
            CreateTable(sqlite_conn);

        }

        private SqliteConnection InitializeDatabase()
        {
            SqliteConnection sqlite_conn;
            // Create a new database connection:
            sqlite_conn = new SqliteConnection(ConnectionString);
            // Open the connection:
            try
            {
                sqlite_conn.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return sqlite_conn;
        }

        static void CreateTable(SqliteConnection conn)
        {

            SqliteCommand sqlite_cmd;
            string dropOrdersTable = "DROP TABLE IF EXISTS Orders;";
            string dropDetailsTable = "DROP TABLE IF EXISTS DetailItems;";

            // Create new tables
            string createOrdersTable = @"
            CREATE TABLE Orders (
                OrderNumber INTEGER PRIMARY KEY,
                OrderDate TEXT,
                CustomerName TEXT,
                CustomerPhone TEXT,
                TaxAmount REAL,
                TariffAmount REAL,
                TotalAmount REAL
            );";

            string createDetailsTable = @"
            CREATE TABLE DetailItems (
                OrderNumber INTEGER,
                DetailNumber INTEGER,
                StockID TEXT,
                StockName TEXT,
                StockPrice REAL,
                Quantity INTEGER,
                TariffAmount REAL,
                PRIMARY KEY (OrderNumber, DetailNumber),
                FOREIGN KEY (OrderNumber) REFERENCES Orders(OrderNumber)
            );";

            // Execute commands
            sqlite_cmd = conn.CreateCommand();
            sqlite_cmd.CommandText = dropOrdersTable;
            sqlite_cmd.ExecuteNonQuery();

            sqlite_cmd.CommandText = dropDetailsTable;
            sqlite_cmd.ExecuteNonQuery();

            sqlite_cmd.CommandText = createOrdersTable;
            sqlite_cmd.ExecuteNonQuery();

            sqlite_cmd.CommandText = createDetailsTable;
            sqlite_cmd.ExecuteNonQuery();

        }

        //save the order to database
        public void SaveOrder(Order order)
        {
            if (order == null)
            {
                throw new ArgumentNullException(nameof(order), "Order cannot be null.");
            }

            using (var connection = new SqliteConnection(ConnectionString))
            {
                connection.Open();
                var insertOrder = @"
                    INSERT OR REPLACE INTO Orders (OrderNumber, OrderDate, CustomerName, CustomerPhone, TaxAmount, TariffAmount, TotalAmount)
                    VALUES (@OrderNumber, @OrderDate, @CustomerName, @CustomerPhone, @TaxAmount, @TariffAmount, @TotalAmount);";

                using (var command = new SqliteCommand(insertOrder, connection))
                {
                    command.Parameters.AddWithValue("@OrderNumber", order.OrderNumber);
                    command.Parameters.AddWithValue("@OrderDate", order.OrderDate.ToString("yyyy-MM-dd HH:mm:ss"));
                    command.Parameters.AddWithValue("@CustomerName", order.CustomerName);
                    command.Parameters.AddWithValue("@CustomerPhone", order.CustomerPhone);
                    command.Parameters.AddWithValue("@TaxAmount", order.TaxAmount);
                    command.Parameters.AddWithValue("@TariffAmount", order.TariffAmount);
                    command.Parameters.AddWithValue("@TotalAmount", order.TotalAmount);
                    command.ExecuteNonQuery();
                }
            }

            Console.WriteLine($"Order {order.OrderNumber} saved to the database.");
        }

        //print out the result from database
        public void PrintOrders()
        {
            using (var connection = new SqliteConnection(ConnectionString))
            {
                connection.Open();
                var selectOrders = "SELECT * FROM Orders;";
                using (var command = new SqliteCommand(selectOrders, connection))
                using (var reader = command.ExecuteReader())
                {
                    Console.WriteLine("Orders in the database:");
                    while (reader.Read())
                    {
                        Console.WriteLine($"OrderNumber: {reader["OrderNumber"]}, CustomerName: {reader["CustomerName"]}, TotalAmount: {reader["TotalAmount"]}");
                    }
                }
            }
        }
    }
}