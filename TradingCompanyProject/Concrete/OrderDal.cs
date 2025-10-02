using TradingCompanyDal.Interfaces;
using TradingCompanyDto;
using Microsoft.Data.SqlClient;

namespace TradingCompanyDal.Concrete
{
        public class OrderDal : IOrderDal
        {
            private readonly string _connectionString = "Data Source=Firefly;Initial Catalog=IMDB2025;Integrated Security=True;Trust Server Certificate=True";

            public Order Create(Order order)
            {
                using SqlConnection connection = new SqlConnection(_connectionString);
                connection.Open();

                SqlCommand command = connection.CreateCommand();
                command.CommandText = "INSERT INTO Orders (CustomerID, StatusID, TotalAmount, OrderDate) OUTPUT inserted.OrderID VALUES (@customerID, @statusID, @totalAmount, @orderDate)";
                command.Parameters.AddWithValue("@customerID", order.CustomerID);
                command.Parameters.AddWithValue("@statusID", order.StatusID);
                command.Parameters.AddWithValue("@totalAmount", order.TotalAmount);
                command.Parameters.AddWithValue("@orderDate", order.OrderDate);

                order.OrderID = (int)command.ExecuteScalar();
                return order;
            }

            public List<Order> GetAll()
            {
                List<Order> orders = new List<Order>();
                using SqlConnection connection = new SqlConnection(_connectionString);
                connection.Open();

                SqlCommand command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM Orders";
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    orders.Add(new Order
                    {
                        OrderID = (int)reader["OrderID"],
                        CustomerID = (int)reader["CustomerID"],
                        StatusID = (int)reader["StatusID"],
                        TotalAmount = (decimal)reader["TotalAmount"],
                        OrderDate = (DateTime)reader["OrderDate"]
                    });
                }
                return orders;
            }

            public Order GetByID(int orderID)
            {
                using SqlConnection connection = new SqlConnection(_connectionString);
                connection.Open();

                SqlCommand command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM Orders WHERE OrderID = @id";
                command.Parameters.AddWithValue("@id", orderID);

                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    return new Order
                    {
                        OrderID = (int)reader["OrderID"],
                        CustomerID = (int)reader["CustomerID"],
                        StatusID = (int)reader["StatusID"],
                        TotalAmount = (decimal)reader["TotalAmount"],
                        OrderDate = (DateTime)reader["OrderDate"]
                    };
                }
                return null;
            }

            public Order Update(Order order)
            {
                using SqlConnection connection = new SqlConnection(_connectionString);
                connection.Open();

                SqlCommand command = connection.CreateCommand();
                command.CommandText = "UPDATE Orders SET CustomerID=@customerID, StatusID=@statusID, TotalAmount=@totalAmount, OrderDate=@orderDate WHERE OrderID=@id";
                command.Parameters.AddWithValue("@customerID", order.CustomerID);
                command.Parameters.AddWithValue("@statusID", order.StatusID);
                command.Parameters.AddWithValue("@totalAmount", order.TotalAmount);
                command.Parameters.AddWithValue("@orderDate", order.OrderDate);
                command.Parameters.AddWithValue("@id", order.OrderID);

                command.ExecuteNonQuery();
                return order;
            }

            public bool Delete(int orderID)
            {
                using SqlConnection connection = new SqlConnection(_connectionString);
                connection.Open();

                SqlCommand command = connection.CreateCommand();
                command.CommandText = "DELETE FROM Orders WHERE OrderID=@id";
                command.Parameters.AddWithValue("@id", orderID);

                int rows = command.ExecuteNonQuery();
                return rows > 0;
            }
        }
    
}
