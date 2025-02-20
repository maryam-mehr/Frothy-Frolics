using Microsoft.AspNetCore.Mvc;

namespace FrothyFrolics_server_.Models
{
    public interface IFoodRepository
    {
        public IEnumerable<FoodItem> GetAll();
        public FoodItem GetById(int id);
        public void Add(FoodItem foodItem);
        public List<FoodItem> GetByCategory(string category);

        public FoodItem GetByTitle(string title);
        public void Update(FoodItem foodItem);
        public void Delete(int id);
    }

}
