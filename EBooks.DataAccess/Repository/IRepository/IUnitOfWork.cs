namespace EBooks.DataAccess.Repository.IRepository;

public interface IUnitOfWork
{
    ICategoryRepository Category { get; }
    IProductRepository Product { get; }
    ICartRepository Cart { get; }
    ICartProductRepository CartProduct { get; }
    void Save();
}