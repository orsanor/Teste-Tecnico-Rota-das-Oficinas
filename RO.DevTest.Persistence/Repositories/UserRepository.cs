using RO.DevTest.Application.Contracts.Persistance.Repositories;
using RO.DevTest.Domain.Entities;

namespace RO.DevTest.Persistence.Repositories;

public class UserRepository(DefaultContext context): BaseRepository<User>(context), IUserRepository
{
    public Task<User> GetUserByIdAsync(Guid requestId)
    {
        throw new NotImplementedException();
    }
}
