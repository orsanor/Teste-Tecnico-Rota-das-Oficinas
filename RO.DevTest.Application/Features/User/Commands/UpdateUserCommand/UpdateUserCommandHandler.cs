using MediatR;
using Microsoft.AspNetCore.Identity;
using RO.DevTest.Application.Contracts.Infrastructure;
using RO.DevTest.Domain.Enums;
using RO.DevTest.Domain.Exception;

namespace RO.DevTest.Application.Features.User.Commands.UpdateUserCommand;

public class UpdateUserCommandHandler(IIdentityAbstractor identity) : IRequestHandler<UpdateUserCommand, Unit>
{
    public async Task<Unit> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var targetUser = await identity.FindUserByIdAsync(request.Id.ToString());
        if (targetUser == null)
            throw new BadRequestException("Usuário não encontrado");

        bool isSelf = request.RequestingUserId == request.Id.ToString();


        targetUser.Name = request.Name;
        targetUser.Email = request.Email;


        if (!Enum.TryParse<UserRoles>(request.Role, true, out var parsedRole))
        {
            throw new BadRequestException("Função informada é inválida.");
        }

        IdentityResult roleResult = await identity.AddToRoleAsync(targetUser, parsedRole);
        if (!roleResult.Succeeded)
        {
            throw new BadRequestException(roleResult);
        }

        await identity.UpdateUserAsync(targetUser);
        return Unit.Value;
    }
}