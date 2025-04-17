using System.Text.Json.Serialization;
using MediatR;

namespace RO.DevTest.Application.Features.Products.Commands.UpdateProductsCommand
{
    [method: JsonConstructor]
    public class UpdateProductCommand(Guid id, string name, decimal price, int quantity, string description) : IRequest<bool>
    {
        public Guid Id { get; set; } = id;
        public string Name { get; set; } = name;
        public decimal Price { get; set; } = price;
        public int Quantity { get; set; } = quantity;
        public string Description { get; set; } = description;
    }
}