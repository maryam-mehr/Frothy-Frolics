using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using Dapper;

namespace FrothyFrolics_server_.Models
{
    public class UserRepository : IUserRepository
    {
        private readonly IRepository<User> _userRepository;
        private readonly string _connectionString;

        public UserRepository(IRepository<User> repository, string connectionString)
        {
            _userRepository = repository;
            _connectionString = connectionString;
        }

        public IEnumerable<User> GetAll()
        {
            return _userRepository.GetAll();
        }

        public User GetById(int id)
        {
            return _userRepository.GetById(id);
        }

        public void Add(User user)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var query = @"
            INSERT INTO [User] (username, phoneNo, loyaltyPoints, email, balance) 
            VALUES (@Username, @PhoneNo, @LoyaltyPoints, @Email, @Balance)";

                connection.Execute(query, new
                {
                    Username = user.username,
                    PhoneNo = user.phoneNo,
                    LoyaltyPoints = user.loyaltyPoints,
                    Email = user.email,
                    Balance = user.balance
                });
            }
        }



        public void Update(User user)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var query = "UPDATE [User] SET loyaltyPoints = @loyaltyPoints, balance = @balance WHERE email = @Email";

                connection.Execute(query, new { loyaltyPoints = user.loyaltyPoints, balance = user.balance, Email = user.email });
            }
        }


        public void Delete(string gmail)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var query = "DELETE FROM [User] WHERE email = @Email";

                connection.Execute(query, new { Email = gmail });
            }
        }


        public User GetByEmail(string email)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM [User] WHERE email = @Email";

                return connection.QuerySingleOrDefault<User>(query, new { Email = email });
            }
        }

    }
}
