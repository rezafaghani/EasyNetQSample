using System;
using System.Data;
using System.IO;
using Autofac;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Exceptions;

namespace EasyNetQSample.Bus
{
    public class LogModule : Module
    {
        public static IConfiguration Configuration { get; } = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true)
            .AddEnvironmentVariables()
            .Build();
        protected override void Load(ContainerBuilder builder)
        {

            Log.Logger = CreateSerilogLogger(Configuration);

            builder.RegisterInstance(Log.Logger).As<ILogger>().SingleInstance();
           // builder.RegisterGeneric(typeof(Logger<>)).As(typeof(ILogger<>)).SingleInstance();

            // TODO: Move to its own module
            builder.RegisterType<ConfigurationSetting>().As<IConfigurationSetting>().SingleInstance();
            builder.RegisterType<DbConnectionFactory>().As<IDbConnectionFactory>().SingleInstance();
            builder.Register(x => x.Resolve<IDbConnectionFactory>().Create()).As<IDbConnection>().InstancePerLifetimeScope();
        }
        /// <summary>
        /// Add Logger  Configuration
        /// </summary>
        /// <param name="configuration"></param>
        /// <returns></returns>
        private static ILogger CreateSerilogLogger(IConfiguration configuration)
        {
            return new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .Enrich.WithProperty("ApplicationContext", "AppName")
                .Enrich.WithExceptionDetails() //add exception logging
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .ReadFrom.Configuration(configuration)//read log setting from json
                .CreateLogger();
        }
    }
}
