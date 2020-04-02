using System.Data;
using Microsoft.Extensions.Logging;
using Npgsql;

namespace EasyNetQSample.Bus
{
    public interface IDbConnectionFactory
    {
        IDbConnection Create();
    }

    public class DbConnectionFactory : IDbConnectionFactory
    {
        private readonly IConfigurationSetting _configuration;
        private readonly ILogger<DbConnectionFactory> _logger;

        public DbConnectionFactory(IConfigurationSetting configuration, ILogger<DbConnectionFactory> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public IDbConnection Create()
        {
            _logger.LogInformation($"Creating a connection to {_configuration.ConnectionString}");
            return new NpgsqlConnection(_configuration.ConnectionString);
        }
    }
}
