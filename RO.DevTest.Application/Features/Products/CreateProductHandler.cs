using MediatR;
using RO.DevTest.Application.Contracts.Product;
using RO.DevTest.Domain.Abstract;
using RO.DevTest.Domain.Entities;

namespace RO.DevTest.Application.Features.Products;

public class CreateProductHandler(IProductRepository productRepository) : IRequestHandler<CreateProductRequest, Guid>
{
    public async Task<Guid> Handle(CreateProductRequest request, CancellationToken cancellationToken)
    {
        var product = new Product (request.Name, request.Price, request.Quantity, request.Description);
        await productRepository.AddAsync(product);
        return product.Id;
    }
}