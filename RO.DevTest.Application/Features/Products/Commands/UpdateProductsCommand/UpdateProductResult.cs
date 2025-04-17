namespace RO.DevTest.Application.Features.Products.Commands.UpdateProductsCommand;

public class UpdateProductResult(Domain.Entities.Product product)
{
    public string? Name { get; set; } = product.Name;
    public float Price { get; set; } = product.Price;
    public int Quantity { get; set; } = product.Quantity;
    public string? Description { get; set; } = product.Description;
}