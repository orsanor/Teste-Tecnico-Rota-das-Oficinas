namespace RO.DevTest.Domain.Entities;

public class Product
{
    public Guid Id { get; private set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public float Price { get; set; }
    public int Quantity { get; set; }

    protected Product()
    {
    }

    public Product(string? name, string? description, float price, int quantity)
    {
        Id = Guid.NewGuid();
        Name = name;
        Description = description;
        Price = price;
        Quantity = quantity;
    }
}