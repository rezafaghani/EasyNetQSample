using EasyNetQSample.Bus;
using EasyNetQSample.Command.PackgeCommands;
using MediatR;
using Serilog;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EasyNetQSample.Reader.Handlers
{
    public class AddPackageManuallyEventHandler : INotificationHandler<AddPackageManuallyEvent>, IEventHandler<AddPackageManuallyEvent>
    {
        private readonly ILogger _logger;
        public AddPackageManuallyEventHandler(ILogger logger)
        {
            _logger = logger;
        }

        public async Task Handle(AddPackageManuallyEvent notification, CancellationToken cancellationToken)
        {
            _logger.Information("Worker running at: {time}", DateTimeOffset.Now);
        }


    }
}
