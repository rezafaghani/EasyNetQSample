using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace EasyNetQSample.Reader
{
    public class Worker : BackgroundService
    {
        private readonly ILogger _logger;

        public Worker(ILogger logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.Information("Worker running at: {time}", DateTimeOffset.Now);

                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
