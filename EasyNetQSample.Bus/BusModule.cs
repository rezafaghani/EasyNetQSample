using System;
using Autofac;
using EasyNetQ;
using EasyNetQ.Consumer;
using EasyNetQ.Topology;

namespace EasyNetQSample.Bus
{
    public class BusModule : Module
    {
        //private static IConfiguration Configuration { get; } = new ConfigurationBuilder()
        //    .SetBasePath(Directory.GetCurrentDirectory())
        //    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        //    .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true)
        //    .AddEnvironmentVariables()
        //    .Build();
        protected override void Load(ContainerBuilder builder)
        {
            //Configuration.GetValue<string>("BusQueue")

            var bus = RabbitHutch.CreateBus("host=localhost", x => x.Register(c => new AdvancedBusEventHandlers((s, e) =>
                {
                    var advancedBus = (IAdvancedBus)s;
                    Console.WriteLine(advancedBus.IsConnected); // This will print true.
                    //advancedBus.ExchangeDeclare("EasyNetQ", ExchangeType.Fanout);
                    


                })).Register<IConsumerErrorStrategy>(_ => new AlwaysRequeueErrorStrategy()));
            
            
            builder.RegisterInstance(bus).SingleInstance();

            builder.RegisterType<Dispatcher>().As<ICommandDispatcher>();
            builder.RegisterType<Dispatcher>().As<IQueryDispatcher>();
            builder.RegisterType<Dispatcher>().As<IEventDispatcher>();

            builder.RegisterType<Executor>().As<ICommandExecutor>();
            builder.RegisterType<Executor>().As<IEventExecutor>();
            builder.RegisterType<Executor>().As<IQueryExecutor>();

            builder.RegisterType<Validator>().As<IValidator>();

            builder.RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAssemblies())
                .AsClosedTypesOf(typeof(IMessageValidator<>)).AsImplementedInterfaces();
        }
    }
}
