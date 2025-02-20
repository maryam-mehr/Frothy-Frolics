using Microsoft.Data.SqlClient;
using static Dapper.SqlMapper;

namespace FrothyFrolics_server_.Models
{
    public class FoodRepository : IFoodRepository
    {
        IRepository<FoodItem> _foodRepository;
        String _connectionString;
        public FoodRepository(IRepository<FoodItem> repository,string connectionString)
        {
            _foodRepository = repository;
            _connectionString = connectionString;
        }

        public IEnumerable<FoodItem> GetAll()
        {
            return _foodRepository.GetAll();
        }

        public List<FoodItem> GetByCategory(string category)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var tableName = "FoodItem";

                var query = $"SELECT * FROM {tableName} WHERE category = @category;";

                
                return connection.Query<FoodItem>(query, new { category }).ToList();
            }
        }

        public FoodItem GetByTitle(string title)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var query = $"SELECT * FROM FoodItem WHERE title = @title;";
                return connection.QuerySingleOrDefault<FoodItem>(query, new { title });
            }
        }
        public FoodItem GetById(int id)
        {
            return _foodRepository.GetById(id);
        }
        public void Add(FoodItem foodItem)
        {
            _foodRepository.Add(foodItem);
        }
        public void Update(FoodItem foodItem)
        {
            _foodRepository.Update(foodItem);
        }
        public void Delete(int id)
        {
            _foodRepository.Delete(id); 
        }


    }

}
