using MediatR;

namespace RO.DevTest.Application.Features.User.Commands.DeleteUserCommand;

public class DeleteUserCommand : IRequest<Unit>
{
    public Guid Id { get; set; }
    public string? RequestingUserId { get; set; }
    public string? RequestingUserRole { get; set; }
}