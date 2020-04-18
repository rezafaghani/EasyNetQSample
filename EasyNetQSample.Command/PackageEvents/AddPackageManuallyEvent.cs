using EasyNetQSample.Bus;
using FluentValidation;
using MediatR;
using System;

namespace EasyNetQSample.Command.PackgeCommands
{
    //[Queue("TestMessagesQueue", ExchangeName = "EasyNetQ")]
    public class AddPackageManuallyEvent : INotification, IEvent
    {
        public Guid Id { get; set; }
    }
    public class AddPackageManuallyEventValidator : MessageValidator<AddPackageManuallyEvent>
    {
        public AddPackageManuallyEventValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            //RuleFor(x => x.Name).NotEmpty().MaximumLength(50);
        }
    }
}
