using System.Collections.Generic;

namespace FrothyFrolics_server_.Models
{
    public interface IUserRepository
    {
        public IEnumerable<User> GetAll();
        public User GetById(int id);
        public void Add(User user);
        public void Update(User user);
        public void Delete(string email);
        public User GetByEmail(string email);
    }
}
