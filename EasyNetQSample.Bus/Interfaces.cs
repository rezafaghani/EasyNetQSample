using System.Threading;
using System.Threading.Tasks;

namespace EasyNetQSample.Bus
{
    public interface IMessage
    {

    }
    public interface IEvent : IMessage
    {
    }

    public interface ICommand : IMessage
    {
    }

    public interface IQuery : IMessage
    {
    }

    public interface IQueryResult : IMessage
    {
    }


    public interface IHandler
    {

    }

    public interface ICommandHandler<in TRequest, TResponse> : IHandler
        where TRequest : ICommand

    {
        Task<TResponse> Handle(TRequest command, CancellationToken cancellationToken);
    }

    public interface IEventHandler<in TRequest> : IHandler
        where TRequest : IEvent
    {
        Task Handle(TRequest evnt, CancellationToken cancellationToken);
    }

    // Interface for query handlers - has two type parameters for the query and the query result
    public interface IQueryHandler<in TQuery, TResult> : IHandler
       where TResult : IQueryResult
       where TQuery : IQuery
    {
        Task<TResult> Handle(TQuery query, CancellationToken cancellationToken);
    }
}