using EBooks.DataAccess.Data;
using EBooks.DataAccess.Repository.IRepository;

namespace EBooks.DataAccess.Repository;

public class UnitOfWork : IUnitOfWork
{
    private ApplicationDbContext _db;
    public ICategoryRepository Category { get; private set; }
    public IProductRepository Product { get; private set; }
    
    public UnitOfWork(ApplicationDbContext db)
    {
        this._db = db;
        this.Category = new CategoryRepository(_db);
        this.Product = new ProductRepository(_db);
    }
    
    public void Save()
    {
        _db.SaveChanges();
    }
}