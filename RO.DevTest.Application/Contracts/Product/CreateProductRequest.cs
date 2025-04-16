using MediatR;

namespace RO.DevTest.Application.Contracts.Product;

public class CreateProductRequest : IRequest<Guid>
{
    public string Name { get; set; }
    public int Price { get; set; }
    public int Quantity { get; set; }
    public string Description { get; set; }
}