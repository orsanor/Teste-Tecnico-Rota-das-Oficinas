using MediatR;

namespace RO.DevTest.Application.Features.Products.Commands.CreateProductsCommand;

public class CreateProductCommand : IRequest<CreateProductResult>
{
    public string? Name { get; set; } = string.Empty;
    public float Price { get; set; }
    public int Quantity { get; set; }
    public string? Description { get; set; } = string.Empty;
}