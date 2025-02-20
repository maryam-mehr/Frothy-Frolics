using Dapper;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;

namespace FrothyFrolics_server_.Models
{
    public class OrderRepository : IOrderRepository
    {
        private readonly string _connectionString;

        public OrderRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<Order> GetAll()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                return connection.Query<Order>("SELECT * FROM Order").ToList();
            }
        }

        public List<Order> GetByEmail(string email)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                // Escape the Order table name with square brackets
                var query = "SELECT * FROM [Order] WHERE UserEmail = @Email;";

                return connection.Query<Order>(query, new { email }).ToList();
            }
        }


        public Order GetById(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var query = "SELECT * FROM Order WHERE OrderId = @Id";
                return connection.QueryFirstOrDefault<Order>(query, new { Id = id });
            }
        }

        public void Add(Order order)
        {
            using (var connection = new SqlConnection(_connectionString))
            {


                    // Ensure table name and column names are correct
                    var query = "INSERT INTO [Order] (UserEmail, ProductDetails, CouponEarned) VALUES (@UserEmail, @ProductDetails, @CouponEarned)";

                    // Execute the query
                    connection.Execute(query, new { order.UserEmail, order.ProductDetails, order.couponEarned });

  

            }
        }



        public void Update(Order order)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var query = "UPDATE Orders SET UserEmail = @UserEmail, ProductDetails = @ProductDetails, CouponsEarned = @CouponsEarned WHERE OrderId = @OrderId";
                connection.Execute(query, order);
            }
        }

        public void Delete(string email)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var query = "DELETE FROM [Order] WHERE UserEmail = @email";
                connection.Execute(query, new { email = email });
            }
        }
    }
}
