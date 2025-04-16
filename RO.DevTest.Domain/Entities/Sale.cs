namespace RO.DevTest.Domain.Entities;

public class Sale
{
    public Guid Id { get; set; }
    public DateTime Date { get; set; }
    public decimal Total { get; set; }
}