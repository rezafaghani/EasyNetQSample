﻿using System;
using System.Threading;
using System.Threading.Tasks;
using EasyNetQSample.Bus;
using EasyNetQSample.Command.PackgeCommands;
using MediatR;
using Serilog;

namespace EasyNetQSample.Reader.Handlers
{
    public class AddPackageManuallyCommandHandler
        :  IRequestHandler<AddPackageManuallyCommand, bool>, ICommandHandler<AddPackageManuallyCommand,bool>
    {

        private readonly ILogger _logger;
        public AddPackageManuallyCommandHandler(ILogger logger)
        {
            _logger = logger;
        }

        public async Task<bool> Handle(AddPackageManuallyCommand request, CancellationToken cancellationToken)
        {
            _logger.Information("Worker running at: {time}", DateTimeOffset.Now);
            return true;
        }

       
    }
}
