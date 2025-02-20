using System.Collections.Generic;

namespace FrothyFrolics_server_.Models
{
    public interface IOrderRepository
    {
        IEnumerable<Order> GetAll();
        List<Order> GetByEmail(string email);
        Order GetById(int id);
        void Add(Order order);
        void Update(Order order);
        void Delete(string email);
    }
}
