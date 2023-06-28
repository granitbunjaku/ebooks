using EBooks.Models;

namespace EBooks.DataAccess.Repository.IRepository;

public interface ICartRepository : IRepository<Cart>
{
    public void Update(Product product);
}