using CSharpFunctionalExtensions;
using InternetShop.Application.Interfaces.Auth;
using InternetShop.Application.Interfaces.Messaging;
using InternetShop.Domain.Common;
using InternetShop.Domain.Entities;
using InternetShop.Domain.Interfaces.Repositories;
using InternetShop.Domain.ValueObjects;

namespace InternetShop.Application.Users.Commands
{
    public record RegisterUserCommand (
        string surname,
        string name,
        string? lastname,
        string gender,
        string numberPhone,
        string login,
        string password,
        string email) : ICommand;

    public class RegisterUserHandler : ICommandHandler<RegisterUserCommand>
    {
        private readonly IUserRepository userRepository;
        private readonly IPasswordHasher passwordHasher;

        public RegisterUserHandler(IUserRepository userRepository, IPasswordHasher passwordHasher)
        {
            this.userRepository = userRepository;
            this.passwordHasher = passwordHasher;
        }

        public async Task<UnitResult<ErrorList>> Handle(RegisterUserCommand command)
        {
            var hashedPassword = passwordHasher.Generate(command.password);
            var fullName = FullName.Create(command.surname, command.name, command.lastname).Value;
            var phoneNumber = PhoneNumber.Create(command.numberPhone).Value;
            var gender = Gender.Create(command.gender).Value;
            var credential = Credentials.Create(command.login, hashedPassword).Value;
            var email = Email.Create(command.email).Value;

            var user = User.Create(fullName, gender, phoneNumber, credential, UserRole.User, email);

            await userRepository.Add(user.Value);

            return Result.Success<ErrorList>();
        }
    }
}
