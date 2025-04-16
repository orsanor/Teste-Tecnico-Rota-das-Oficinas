using Microsoft.EntityFrameworkCore;
using RO.DevTest.Domain.Abstract;
using RO.DevTest.Domain.Entities;

namespace RO.DevTest.Persistence.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly DefaultContext _context;

    public ProductRepository(DefaultContext context)
    {
        _context = context;
    }

    async Task IProductRepository.AddAsync(Product? product)
    {
        await _context.Product.AddAsync(product);
        await _context.SaveChangesAsync();
    }

    async Task<Product> IProductRepository.GetByIdAsync(Guid id)
    {
        return await _context.Product.FindAsync(id);
    }

    async Task<IEnumerable<Product?>> IProductRepository.GetAllAsync()
    {
        return await _context.Product.ToListAsync();
    }
}