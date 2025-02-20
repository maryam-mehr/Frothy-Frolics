using Microsoft.Data.SqlClient;
using static Dapper.SqlMapper;

namespace FrothyFrolics_server_.Models
{
    public class CartRepository : ICartRepository
    {
        IRepository<CartItem> _cartItemRepository;
        string _connectionString;

        public CartRepository(IRepository<CartItem> repository, string connectionString)
        {
            _cartItemRepository = repository;
            _connectionString = connectionString;
        }

        public IEnumerable<CartItem> GetAll()
        {
            return _cartItemRepository.GetAll();
        }

        public List<CartItem> GetByEmail(string email)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var tableName = "CartItem";

                var query = $"SELECT * FROM {tableName} WHERE email = @Email;";

                return connection.Query<CartItem>(query, new { email }).ToList();
            }
        }

        public CartItem GetById(int id)
        {
            return _cartItemRepository.GetById(id);
        }

        public void Add(CartItem cartItem)
        {
            _cartItemRepository.Add(cartItem);
        }

        public void Delete(string title,string email)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var query = "DELETE FROM CartItem WHERE title = @Title AND email = @Email";

                connection.QueryFirstOrDefault<CartItem>(query, new { title, email });
            }
        }

        public CartItem GetByTitle(string title,string email)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var query = "SELECT * FROM CartItem WHERE title = @title and email = @email";
                return connection.QueryFirstOrDefault<CartItem>(query, new { title ,email});
            }
        }

        public void Update(CartItem cartItem)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var query = "UPDATE CartItem SET quantity = @Quantity WHERE title = @Title AND email = @Email";

                connection.Execute(query, new { cartItem.quantity, cartItem.title, cartItem.email });
            }
        }

        public void DeleteCartItemByEmail(string email)
        {
            
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var query = "DELETE FROM CartItem WHERE email = @Email";

                connection.Execute(query, new { email });
            }
        }


    }
}
