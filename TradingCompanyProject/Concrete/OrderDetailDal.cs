using TradingCompanyDal.Interfaces;
using TradingCompanyDto;
using Microsoft.Data.SqlClient;

namespace TradingCompanyDal.Concrete
{
  
        public class OrderDetailDal : IOrderDetailDal
        {
            private readonly string _connectionString = "Data Source=Firefly;Initial Catalog=IMDB2025;Integrated Security=True;Trust Server Certificate=True";

            public OrderDetail Create(OrderDetail orderDetail)
            {
                using SqlConnection connection = new SqlConnection(_connectionString);
                connection.Open();

                SqlCommand command = connection.CreateCommand();
                command.CommandText = "INSERT INTO OrderDetails (OrderID, ProductID, Quantity) OUTPUT inserted.OrderDetailID VALUES (@orderID, @productID, @quantity)";
                command.Parameters.AddWithValue("@orderID", orderDetail.OrderID);
                command.Parameters.AddWithValue("@productID", orderDetail.ProductID);
                command.Parameters.AddWithValue("@quantity", orderDetail.Quantity);

                orderDetail.OrderDetailID = (int)command.ExecuteScalar();
                return orderDetail;
            }

            public List<OrderDetail> GetAll()
            {
                List<OrderDetail> details = new List<OrderDetail>();
                using SqlConnection connection = new SqlConnection(_connectionString);
                connection.Open();

                SqlCommand command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM OrderDetails";
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    details.Add(new OrderDetail
                    {
                        OrderDetailID = (int)reader["OrderDetailID"],
                        OrderID = (int)reader["OrderID"],
                        ProductID = (int)reader["ProductID"],
                        Quantity = (int)reader["Quantity"]
                    });
                }
                return details;
            }

            public OrderDetail GetByID(int orderDetailID)
            {
                using SqlConnection connection = new SqlConnection(_connectionString);
                connection.Open();

                SqlCommand command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM OrderDetails WHERE OrderDetailID = @id";
                command.Parameters.AddWithValue("@id", orderDetailID);

                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    return new OrderDetail
                    {
                        OrderDetailID = (int)reader["OrderDetailID"],
                        OrderID = (int)reader["OrderID"],
                        ProductID = (int)reader["ProductID"],
                        Quantity = (int)reader["Quantity"]
                    };
                }
                return null;
            }

            public OrderDetail Update(OrderDetail orderDetail)
            {
                using SqlConnection connection = new SqlConnection(_connectionString);
                connection.Open();

                SqlCommand command = connection.CreateCommand();
                command.CommandText = "UPDATE OrderDetails SET OrderID=@orderID, ProductID=@productID, Quantity=@quantity WHERE OrderDetailID=@id";
                command.Parameters.AddWithValue("@orderID", orderDetail.OrderID);
                command.Parameters.AddWithValue("@productID", orderDetail.ProductID);
                command.Parameters.AddWithValue("@quantity", orderDetail.Quantity);
                command.Parameters.AddWithValue("@id", orderDetail.OrderDetailID);

                command.ExecuteNonQuery();
                return orderDetail;
            }

            public bool Delete(int orderDetailID)
            {
                using SqlConnection connection = new SqlConnection(_connectionString);
                connection.Open();

                SqlCommand command = connection.CreateCommand();
                command.CommandText = "DELETE FROM OrderDetails WHERE OrderDetailID=@id";
                command.Parameters.AddWithValue("@id", orderDetailID);

                int rows = command.ExecuteNonQuery();
                return rows > 0;
            }
        }
    
}
