using Microsoft.EntityFrameworkCore;
using RO.DevTest.Domain.Abstract;
using RO.DevTest.Domain.Entities;

namespace RO.DevTest.Persistence.Repositories;

public class ProductRepository(DefaultContext context) : IProductRepository
{
    async Task IProductRepository.AddAsync(Product? product)
    {
        if (product != null) await context.Product.AddAsync(product);
        await context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Product product)
    {
        if (product == null)
        {
            throw new ArgumentNullException(nameof(product));
        }

        var existingProduct = await context.Product.FindAsync(product.Id);

        if (existingProduct == null)
        {
            throw new InvalidOperationException("Produto não encontrado para atualização.");
        }
        
        existingProduct.Name = product.Name;
        existingProduct.Price = product.Price;
        existingProduct.Quantity = product.Quantity;
        existingProduct.Description = product.Description;

        await context.SaveChangesAsync();
    }

    async Task IProductRepository.DeleteAsync(Guid id)
    {
        var product = await context.Product.FindAsync(id);
        if (product != null)
        {
            context.Product.Remove(product);
            await context.SaveChangesAsync();
        }
        else
        {
            throw new KeyNotFoundException($"Product with id {id} not found.");
        }
    }

    async Task<Product> IProductRepository.GetByIdAsync(Guid id)
    {
        return (await context.Product.FindAsync(id))!;
    }

    async Task<IEnumerable<Product>> IProductRepository.GetAllAsync()
    {
        return (await context.Product.ToListAsync())!;
    }
    
    public IQueryable<Product> GetAllQueryable()
    {
        return context.Product.AsQueryable();
    }

}