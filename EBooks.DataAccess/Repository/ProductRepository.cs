using EBooks.DataAccess.Data;
using EBooks.DataAccess.Repository.IRepository;
using EBooks.Models;

namespace EBooks.DataAccess.Repository;

public class ProductRepository : Repository<Product>, IProductRepository
{
    private readonly ApplicationDbContext _db;

    public ProductRepository(ApplicationDbContext db) : base(db)
    {
        this._db = db;
    }
    
    public void Update(Product obj)
    {
        _db.Products.Update(obj);
    }
}