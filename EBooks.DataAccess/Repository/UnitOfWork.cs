using EBooks.DataAccess.Data;
using EBooks.DataAccess.Repository.IRepository;

namespace EBooks.DataAccess.Repository;

public class UnitOfWork : IUnitOfWork
{
    private ApplicationDbContext _db;
    public ICategoryRepository Category { get; private set; }
    public IProductRepository Product { get; private set; }
    public ICartRepository Cart { get; private set; }
    public ICartProductRepository CartProduct { get; private set;}
    
    public UnitOfWork(ApplicationDbContext db)
    {
        _db = db;
        Category = new CategoryRepository(_db);
        Product = new ProductRepository(_db);
        Cart = new CartRepository(_db);
        CartProduct = new CartProductRepository(_db);
    }
    
    public void Save()
    {
        _db.SaveChanges();
    }
}