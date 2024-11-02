using CSharpFunctionalExtensions;
using InternetShop.Application.Interfaces.Auth;
using InternetShop.Application.Interfaces.Messaging;
using InternetShop.Domain.Common;
using InternetShop.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace InternetShop.Application.Users.Commands
{
    public record LoginUserCommand(string UserName, string Password) : ICommand;

    public class LoginUserHandler : ICommandHandler<string, LoginUserCommand>
    {
        private readonly UserManager<User> _userManager;
        private readonly IJwtProvider jwtProvider;

        public LoginUserHandler(UserManager<User> userManager, IJwtProvider jwtProvider)
        {
            _userManager = userManager;
            this.jwtProvider = jwtProvider;
        }

        public async Task<Result<string, ErrorList>> Handle(LoginUserCommand command)
        {
            var user = await _userManager.FindByNameAsync(command.UserName);
            if (user is null)
            {
                return Errors.General.NotFound().ToErrorList();
            }

            var passwordConfirmed = await _userManager.CheckPasswordAsync(user, command.Password);

            if (!passwordConfirmed)
            {
                return Errors.User.InvalidCredentials().ToErrorList();
            }

            var token = jwtProvider.GenerateToken(user);

            return token;
        }
    }
}
