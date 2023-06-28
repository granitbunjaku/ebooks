using EBooks.DataAccess.Data;
using EBooks.DataAccess.Repository.IRepository;
using EBooks.Models;

namespace EBooks.DataAccess.Repository;

public class CartProductRepository : Repository<CartProduct>, ICartProductRepository
{
    private readonly ApplicationDbContext _db;

    public CartProductRepository(ApplicationDbContext db) : base(db)
    {
        _db = db;
    }
}