using MassTransit;
using RabbitMQ.ESBMassTransit.Shared.Messages;

namespace RabbitMQ.ESBMassTransit.WorkerService.Consumer.Consumers
{
    public class ExampleMessageConsumer : IConsumer<IMessage>
    {
        public Task Consume(ConsumeContext<IMessage> context)
        {
            Console.WriteLine($"Gelen Mesaj : {context.Message.Text}");
            return Task.CompletedTask;
        }
    }
}
