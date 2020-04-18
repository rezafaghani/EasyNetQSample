using System;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using EasyNetQSample.Bus;
using EasyNetQSample.Command.PackgeCommands;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace EasyNetQSample.Sender
{
    class Program
    {


        static void Main(string[] args)
        {
            ////setup our DI
            //var serviceProvider = new ServiceCollection()
            //    .AddLogging()
            //    .AddSingleton<BusModule>()
            // The Microsoft.Extensions.DependencyInjection.ServiceCollection
            // has extension methods provided by other .NET Core libraries to
            // regsiter services with DI.
            var serviceCollection = new ServiceCollection();

            // The Microsoft.Extensions.Logging package provides this one-liner
            // to add logging services.
            serviceCollection.AddLogging();

            var containerBuilder = new ContainerBuilder();

            containerBuilder.Populate(serviceCollection);
            //configure console logging
            containerBuilder.RegisterModule<BusModule>();
                //.RegisterModule<LogModule>(); 

            // Creating a new AutofacServiceProvider makes the container
            // available to your app using the Microsoft IServiceProvider
            // interface so you can use those abstractions rather than
            // binding directly to Autofac.
            var container = containerBuilder.Build();
            var serviceProvider = new AutofacServiceProvider(container); ;

            var logger = serviceProvider.GetService<ILoggerFactory>()
                .CreateLogger<Program>();
            //logger.AddConsole(LogLevel.Debug)
            logger.LogDebug("Starting application");
            try
            {
                var commands = serviceProvider.GetService<ICommandDispatcher>();
                var eventDispatcher = serviceProvider.GetService<IEventDispatcher>();
                while (true)
                {

                    Console.WriteLine("s: send , q: quit , p: publish");
                    var sendOrder = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(sendOrder) && sendOrder.ToLower() == "s")
                        Task.Run(async () => await commands.Send(new AddPackageManuallyCommand
                        {
                            Id = Guid.NewGuid()
                        }));
                    else if (!string.IsNullOrWhiteSpace(sendOrder) && sendOrder.ToLower() == "p")
                        Task.Run(async () => await eventDispatcher.Publish(new AddPackageManuallyEvent
                        {
                            Id = Guid.NewGuid()
                        }));
                    else if (!string.IsNullOrWhiteSpace(sendOrder) && sendOrder.ToLower() == "q")
                    {
                        Console.WriteLine("Quit");
                        break;
                    }
                    
                }
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
    }
}
