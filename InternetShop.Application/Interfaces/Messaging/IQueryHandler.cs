namespace InternetShop.Application.Interfaces.Messaging;

public interface IQueryHandler<TResponse, in TQuery> where TQuery : IQuery
{
    public Task<TResponse> Handle(TQuery query);
}

public interface IQueryHandler<TResponse>
{
    public Task<TResponse> Handle();
}
