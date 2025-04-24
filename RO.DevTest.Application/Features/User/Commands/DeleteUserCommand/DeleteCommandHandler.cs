using MediatR;
using RO.DevTest.Application.Contracts.Infrastructure;
using RO.DevTest.Domain.Enums;
using RO.DevTest.Domain.Exception;

namespace RO.DevTest.Application.Features.User.Commands.DeleteUserCommand;

public class DeleteUserCommandHandler(IIdentityAbstractor identity) : IRequestHandler<DeleteUserCommand, Unit>
{
    public async Task<Unit> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var user = await identity.FindUserByIdAsync(request.Id.ToString());
        if (user == null)
            throw new BadRequestException("Usuário não encontrado");

        bool isSelf = request.RequestingUserId == request.Id.ToString();
        bool isAdmin = request.RequestingUserRole == nameof(UserRoles.Admin);

        if (!isAdmin && !isSelf)
            throw new BadRequestException("Você não tem permissão para excluir este usuário");

        await identity.DeleteUser(user);
        return Unit.Value;
    }
}