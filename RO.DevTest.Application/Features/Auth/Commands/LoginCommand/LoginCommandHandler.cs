using MediatR;
using Microsoft.AspNetCore.Identity;
using RO.DevTest.Application.Interfaces;
using RO.DevTest.Domain.Exception;

namespace RO.DevTest.Application.Features.Auth.Commands.LoginCommand
{
    public class LoginCommandHandler(
        UserManager<Domain.Entities.User> userManager,
        SignInManager<Domain.Entities.User> signInManager,
        IJwtTokenGenerator tokenGenerator)
        : IRequestHandler<LoginCommand, LoginResponse>
    {
        public async Task<LoginResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await userManager.FindByNameAsync(request.Username);

            if (user == null)
                throw new BadRequestException("Usuário ou senha inválidos.");

            var result = await signInManager.CheckPasswordSignInAsync(user, request.Password, false);

            if (!result.Succeeded)
                throw new BadRequestException("Usuário ou senha inválidos.");

            var roles = await userManager.GetRolesAsync(user);
            var token = tokenGenerator.GenerateToken(user, roles.ToList());

            return new LoginResponse()
            {
                AccessToken = token,
                Roles = roles,
                IssuedAt = DateTime.UtcNow,
                ExpirationDate = DateTime.UtcNow.AddHours(1) 
            };

        }
    }
}