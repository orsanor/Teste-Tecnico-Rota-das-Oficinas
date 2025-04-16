namespace RO.DevTest.Domain.Entities;

public class Client
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public DateTime RegistrationDate  { get; set; }
}