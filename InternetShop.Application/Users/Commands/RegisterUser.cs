using CSharpFunctionalExtensions;
using InternetShop.Application.Interfaces.Messaging;
using InternetShop.Domain.Common;
using InternetShop.Domain.Entities;
using InternetShop.Domain.ValueObjects;
using Microsoft.AspNetCore.Identity;

namespace InternetShop.Application.Users.Commands
{
    public record RegisterUserCommand(string Surname, string Name, string? Lastname, string Email, string UserName, string Password) : ICommand;

    public class RegisterUserHandler : ICommandHandler<RegisterUserCommand>
    {
        private readonly UserManager<User> _userManager;

        public RegisterUserHandler(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<UnitResult<ErrorList>> Handle(RegisterUserCommand command)
        {
            var user = User.Create(
                FullName.Create(command.Surname, command.Name, command.Lastname).Value,
                Gender.Male);

            user.Value.Email = command.Email;
            user.Value.UserName = command.UserName;;

            var result = await _userManager.CreateAsync(user.Value, command.Password);
            if (result.Succeeded) return Result.Success<ErrorList>();

            var errors = result.Errors.Select(e => Error.Failure(e.Code, e.Description)).ToList();

            return new ErrorList(errors);

        }
    }
}
