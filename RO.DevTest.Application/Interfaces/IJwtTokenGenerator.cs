using RO.DevTest.Domain.Entities;

namespace RO.DevTest.Application.Interfaces
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(User user, List<string> roles);
    }
}