using MediatR;

namespace RO.DevTest.Application.Features.Products.Commands.CreateProductsCommand;

public class CreateProductCommand(string name, int price, int quantity, string description)
    : IRequest<Guid>
{
    public string Name { get; set; } = name;
    public int Price { get; set; } = price;
    public int Quantity { get; set; } = quantity;
    public string Description { get; set; } = description;
}