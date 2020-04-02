using Autofac;
using EasyNetQSample.Bus;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace EasyNetQSample.Reader
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //CreateHostBuilder(args).Build().Run();
            BuildContainer().Resolve<App>().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)

                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<Worker>();

                });

        private static IContainer BuildContainer()
        {
            var builder = new ContainerBuilder();
            builder
                .RegisterModule<HandlerModule>()
                .RegisterModule<BusModule>()
                .RegisterModule<LogModule>();

            builder.RegisterType<App>();
            builder.RegisterType<Worker>()
                .As<IHostedService>();


            return builder.Build();
        }
    }
}
