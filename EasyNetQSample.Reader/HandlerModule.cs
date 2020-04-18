using Autofac;
using EasyNetQSample.Bus;

namespace EasyNetQSample.Reader
{
    public class HandlerModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // Command Handlers
            builder.RegisterAssemblyTypes(GetType().Assembly)
                .AsClosedTypesOf(typeof(ICommandHandler<,>)).AsImplementedInterfaces();
            builder.RegisterAssemblyTypes(GetType().Assembly)
               .AsClosedTypesOf(typeof(IEventHandler<>)).AsImplementedInterfaces();
        }
    }
}
