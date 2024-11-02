using CSharpFunctionalExtensions;
using InternetShop.Domain.Common;

namespace InternetShop.Application.Interfaces.Messaging;

public interface ICommandHandler<TResponse, in TCommand> where TCommand : ICommand
{
    public Task<Result<TResponse, ErrorList>> Handle(TCommand command);
}

public interface ICommandHandler<in TCommand> where TCommand : ICommand
{
    public Task<UnitResult<ErrorList>> Handle(TCommand command);
}