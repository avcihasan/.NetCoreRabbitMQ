using MassTransit;
using RabbitMQ.ESBMassTransit.Shared.Messages;

namespace RabbitMQ.ESBMassTransit.Consumer.Consumers
{
    public class ExampleMessageConsumer : IConsumer<IMessage>
    {
        public Task Consume(ConsumeContext<IMessage> context)
        {
            Console.WriteLine($"Gelene Mesaj : {context.Message.Text}");

            return Task.CompletedTask;
        }
    }
}
