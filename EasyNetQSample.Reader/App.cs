using Autofac;
using EasyNetQ;
using EasyNetQSample.Bus;
using EasyNetQSample.Command.PackgeCommands;
using Serilog;

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
            Setup<AddPackageManuallyCommand>();
            //Setup<ChangePackageCategoryCommand>();
        }
        private void Setup<TCommand>()
            where TCommand : class, ICommand
        {
            _logger.Information($"Setting up to handle command {typeof(TCommand).Name}");
            var x = nameof(TCommand);
            _bus.Receive<TCommand>(typeof(TCommand).Name, request => _resolver.Resolve<ICommandExecutor>().ExecuteCommand(request));
        }
    }
}
