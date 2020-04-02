using System;
using EasyNetQ;
using EasyNetQSample.Bus;
using FluentValidation;
using MediatR;

namespace EasyNetQSample.Command.PackgeCommands
{
    //[Queue("TestMessagesQueue", ExchangeName = "EasyNetQ")]
    public class AddPackageManuallyCommand : IRequest<bool>,ICommand
    {
        public Guid Id { get; set; }
    }
    public class AddPackageManuallyValidator : MessageValidator<AddPackageManuallyCommand>
    {
        public AddPackageManuallyValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            //RuleFor(x => x.Name).NotEmpty().MaximumLength(50);
        }
    }
}
