using MediatR;
using RO.DevTest.Application.Contracts.Infrastructure;
using RO.DevTest.Domain.Enums;
using RO.DevTest.Domain.Exception;
using Microsoft.AspNetCore.Http;

namespace RO.DevTest.Application.Features.User.Commands.DeleteUserCommand
{
    public class DeleteUserCommandHandler(IIdentityAbstractor identity, IHttpContextAccessor httpContextAccessor)
        : IRequestHandler<DeleteUserCommand, Unit>
    {
        public async Task<Unit> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var currentUserId = httpContextAccessor.HttpContext?.User?.FindFirst("sub")?.Value;
            var currentUserRole = httpContextAccessor.HttpContext?.User?.FindFirst("role")?.Value;
            
            if (string.IsNullOrEmpty(currentUserId) || string.IsNullOrEmpty(currentUserRole))
                throw new BadRequestException("Informações do usuário solicitante estão ausentes.");

            var user = await identity.FindUserByIdAsync(request.Id.ToString());
            if (user == null)
                throw new BadRequestException("Usuário não encontrado");

            var isSelf = currentUserId == request.Id.ToString();
            var isAdmin = currentUserRole == nameof(UserRoles.Admin);

            if (!isAdmin && !isSelf)
                throw new BadRequestException("Você não tem permissão para excluir este usuário");
            
            var result = await identity.DeleteUserAsync(user);
            if (!result.Succeeded)
                throw new BadRequestException("Erro ao excluir o usuário");

            return Unit.Value;
        }
    }
}
