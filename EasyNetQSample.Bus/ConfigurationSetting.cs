using System;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace EasyNetQSample.Bus
{
    public interface IConfigurationSetting 
    {
        
        string ConnectionString { get; }
        
    }

    public class ConfigurationSetting : IConfigurationSetting
    {
        public static IConfiguration Configuration { get; } = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true)
            .AddEnvironmentVariables()
            .Build();

       
        public string ConnectionString => Configuration.GetConnectionString("BezorgtConnection"); //$"host={Host};port={Port};database={Database};username={UserName};password={Password}";

    }
}
