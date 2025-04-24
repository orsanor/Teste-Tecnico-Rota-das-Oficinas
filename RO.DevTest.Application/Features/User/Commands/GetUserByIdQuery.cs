using MediatR;

namespace RO.DevTest.Application.Features.User.Commands
{
    public abstract class GetUserByIdQuery(string id) : IRequest<Domain.Entities.User>
    {
        public Guid Id { get; set; } = new Guid(id);
    }
}