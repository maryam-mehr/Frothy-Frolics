using Dapper;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;

namespace FrothyFrolics_server_.Models
{
    public interface ICartRepository
    {
        public IEnumerable<CartItem> GetAll();             
        public List<CartItem> GetByEmail(string email);   
        public CartItem GetById(int id);                 
        public void Add(CartItem cartItem);                
        public void Update(CartItem cartItem);           
        public void Delete(string title,string email);

        public CartItem GetByTitle(string title,string email);

        public void DeleteCartItemByEmail(string email);



    }
}
