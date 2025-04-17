namespace RO.DevTest.Domain.Entities;

public class Product
{
    public Guid Id { get; private set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }

    protected Product()
    {
    }

    public Product(string name, decimal price, int quantity, string description)
    {
        Id = Guid.NewGuid();
        Name = name;
        Price = price;
        Description = description;
        Quantity = quantity;
    }
}