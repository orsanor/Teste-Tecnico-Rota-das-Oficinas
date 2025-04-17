namespace RO.DevTest.Application.Features.Products.Commands.CreateProductsCommand
{
    public class CreateProductResult(Domain.Entities.Product product)
    {
        public Guid Id { get; set; } = product.Id;
        public string? Name { get; set; } = product.Name;
        public float Price { get; set; } = product.Price;
        public int Quantity { get; set; } = product.Quantity;
        public string? Description { get; set; } = product.Description;
    }
}