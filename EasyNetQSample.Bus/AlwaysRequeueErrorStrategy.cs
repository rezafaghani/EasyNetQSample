using EasyNetQ.Consumer;
using System;
using System.IO;

namespace EasyNetQSample.Bus
{
    public sealed class AlwaysRequeueErrorStrategy : IConsumerErrorStrategy
    {
        public void Dispose()
        {
        }

        public AckStrategy HandleConsumerError(ConsumerExecutionContext context, Exception exception)
        {
            //FileNotFoundException
            if (exception.GetType().Equals(typeof(FileNotFoundException)))
            {
                return AckStrategies.NackWithoutRequeue;
            }
            return AckStrategies.NackWithRequeue;
        }

        public AckStrategy HandleConsumerCancelled(ConsumerExecutionContext context)
        {
            return AckStrategies.NackWithRequeue;
        }
    }
}
