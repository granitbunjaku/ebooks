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
        var objFromDb = _db.Products.FirstOrDefault(p => p.Id == obj.Id);

        if (objFromDb is not null)
        {
            objFromDb.Title = obj.Title;
            objFromDb.ISBN = obj.ISBN;
            objFromDb.Price50 = obj.Price50;
            objFromDb.ListPrice = obj.ListPrice;
            objFromDb.Price100 = obj.Price100;
            objFromDb.Description = obj.Description;
            objFromDb.CategoryId = obj.CategoryId;
            objFromDb.Author = obj.Author;
            if (obj.ImageUrl != null)
            {
                objFromDb.ImageUrl = obj.ImageUrl;
            }
        }
    }
}