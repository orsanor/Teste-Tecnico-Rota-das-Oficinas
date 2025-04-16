using RO.DevTest.Domain.Entities;

namespace RO.DevTest.Domain.Abstract;

public interface IProductRepository
{
    Task AddAsync(Product? product);
    Task<Product> GetByIdAsync(Guid id);
    Task<IEnumerable<Product?>> GetAllAsync();
}