using Autofac;
using EasyNetQ;
using EasyNetQSample.Bus;
using EasyNetQSample.Command.PackgeCommands;
using Serilog;
using System.Threading;

namespace EasyNetQSample.Reader
{
    class App
    {
        private readonly IBus _bus;
        private readonly ILogger _logger;
        private readonly IComponentContext _resolver;

        public App(IBus bus, ILogger logger, IComponentContext resolver)
        {
            _bus = bus;
            _logger = logger;
            _resolver = resolver;
        }

        public void Run()
        {
            // Setup commands
            Setup<AddPackageManuallyCommand,bool>();
            SetupToHandle<AddPackageManuallyEvent,bool>();
        }
        private void Setup<TCommand, TResponse>()
            where TCommand : class, ICommand
        {
            _logger.Information($"Setting up to handle command {typeof(TCommand).Name}");
            var x = nameof(TCommand);
            _bus.Receive<TCommand>(typeof(TCommand).Name, request => _resolver.Resolve<ICommandExecutor>().ExecuteCommand<TCommand, TResponse>(request,new CancellationToken(false)));
        }
        private void SetupToHandle<TEvent, TResponse>()
            where TEvent : class, IEvent
        {
            _logger.Information($"Setting up to handle event {typeof(TEvent).Name}");
            _bus.Subscribe<TEvent>(typeof(Program).FullName, evnt => _resolver.Resolve<IEventExecutor>().ExecuteEvent<TEvent, TResponse>(evnt, new CancellationToken(false)));
        }
    }
}
