using TradingCompanyDal.Interfaces;
using TradingCompanyDto;
using Microsoft.Data.SqlClient;

namespace TradingCompanyDal.Concrete
{

        public class CustomerDal : ICustomerDal
        {
            private readonly string _connectionString = "Data Source=localhost;Initial Catalog=Software;Integrated Security=True;TrustServerCertificate=True";

        public Customer Create(Customer customer)
            {
                using SqlConnection connection = new SqlConnection(_connectionString);
                connection.Open();

                SqlCommand command = connection.CreateCommand();
                command.CommandText = "INSERT INTO Customers (Name, Phone, BirthDate) OUTPUT inserted.CustomerID VALUES (@name, @phone, @birthDate)";
                command.Parameters.AddWithValue("@name", customer.Name);
                command.Parameters.AddWithValue("@phone", customer.Phone);
                command.Parameters.AddWithValue("@birthDate", customer.BirthDate);

                customer.CustomerID = (int)command.ExecuteScalar();
                return customer;
            }

            public List<Customer> GetAll()
            {
                List<Customer> customers = new List<Customer>();
                using SqlConnection connection = new SqlConnection(_connectionString);
                connection.Open();

                SqlCommand command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM Customers";
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    customers.Add(new Customer
                    {
                        CustomerID = (int)reader["CustomerID"],
                        Name = (string)reader["Name"],
                        Phone = (string)reader["Phone"],
                        BirthDate = (DateTime)reader["BirthDate"]
                    });
                }
                return customers;
            }

            public Customer GetByID(int customerID)
            {
                using SqlConnection connection = new SqlConnection(_connectionString);
                connection.Open();

                SqlCommand command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM Customers WHERE CustomerID = @id";
                command.Parameters.AddWithValue("@id", customerID);

                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    return new Customer
                    {
                        CustomerID = (int)reader["CustomerID"],
                        Name = (string)reader["Name"],
                        Phone = (string)reader["Phone"],
                        BirthDate = (DateTime)reader["BirthDate"]
                    };
                }
                return null;
            }

            public Customer Update(Customer customer)
            {
                using SqlConnection connection = new SqlConnection(_connectionString);
                connection.Open();

                SqlCommand command = connection.CreateCommand();
                command.CommandText = "UPDATE Customers SET Name=@name, Phone=@phone, BirthDate=@birthDate WHERE CustomerID=@id";
                command.Parameters.AddWithValue("@name", customer.Name);
                command.Parameters.AddWithValue("@phone", customer.Phone);
                command.Parameters.AddWithValue("@birthDate", customer.BirthDate);
                command.Parameters.AddWithValue("@id", customer.CustomerID);

                command.ExecuteNonQuery();
                return customer;
            }

            public bool Delete(int customerID)
            {
                using SqlConnection connection = new SqlConnection(_connectionString);
                connection.Open();

                SqlCommand command = connection.CreateCommand();
                command.CommandText = "DELETE FROM Customers WHERE CustomerID=@id";
                command.Parameters.AddWithValue("@id", customerID);

                int rows = command.ExecuteNonQuery();
                return rows > 0;
            }
        }
   
}
