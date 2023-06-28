using EBooks.DataAccess.Data;
using EBooks.DataAccess.Repository.IRepository;
using EBooks.Models;

namespace EBooks.DataAccess.Repository;

public class CartRepository : Repository<Cart>, ICartRepository
{
    private readonly ApplicationDbContext _db;

    public CartRepository(ApplicationDbContext db) : base(db)
    {
        _db = db;
    }

    public void Update(Product product)
    {
        throw new NotImplementedException();
    }
}