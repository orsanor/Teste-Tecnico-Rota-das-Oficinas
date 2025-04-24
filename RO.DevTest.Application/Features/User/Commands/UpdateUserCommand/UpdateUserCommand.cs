using MediatR;
using RO.DevTest.Domain.Enums;

namespace RO.DevTest.Application.Features.User.Commands.UpdateUserCommand;

public class UpdateUserCommand : IRequest<Unit>
{
    public Guid Id { get; set; } 
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? Role { get; set; } = string.Empty; 

    public string? RequestingUserId { get; set; } 
    public string? RequestingUserRole { get; set; } 
}