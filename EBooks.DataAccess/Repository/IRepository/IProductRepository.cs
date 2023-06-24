using EBooks.Models;

namespace EBooks.DataAccess.Repository.IRepository;

public interface IProductRepository : IRepository<Product>
{
    public void Update(Product obj);
}