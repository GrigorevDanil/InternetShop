using CSharpFunctionalExtensions;
using InternetShop.Application.Interfaces.Auth;
using InternetShop.Application.Interfaces.Messaging;
using InternetShop.Domain.Common;
using InternetShop.Domain.Interfaces.Repositories;

namespace InternetShop.Application.Users.Commands
{
    public record LoginUserCommand(
        string login,
        string password) : ICommand;

    public class LoginUserHandler : ICommandHandler<string, LoginUserCommand>
    {
        private readonly IUserRepository userRepository;
        private readonly IPasswordHasher passwordHasher;
        private readonly IJwtProvider jwtProvider;

        public LoginUserHandler(IUserRepository userRepository, IPasswordHasher passwordHasher, IJwtProvider jwtProvider)
        {
            this.userRepository = userRepository;
            this.passwordHasher = passwordHasher;
            this.jwtProvider = jwtProvider;
        }

        public async Task<Result<string, ErrorList>> Handle(LoginUserCommand command)
        {
            var user = await userRepository.GetByLogin(command.login);
            if (user != null)
            {
                var result = passwordHasher.Verify(command.password, user.Credentials.PasswordHash);

                if (result == false) return Errors.User.InvalidCredentials().ToErrorList();

                var token = jwtProvider.GenerateToken(user);

                return token;
            }
            return Errors.General.NotFound().ToErrorList();
        }
    }
}
