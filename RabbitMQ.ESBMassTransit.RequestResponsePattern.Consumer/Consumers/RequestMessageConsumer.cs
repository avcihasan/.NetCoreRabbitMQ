using MassTransit;
using RabbitMQ.ESBMassTransit.Shared.RequestResponsePatterMessages;

namespace RabbitMQ.ESBMassTransit.RequestResponsePattern.Consumer.Consumers
{
    public class RequestMessageConsumer : IConsumer<RequestMessage>
    {
        public async Task Consume(ConsumeContext<RequestMessage> context)
        {
            Console.WriteLine(context.Message.Text);
            await context.RespondAsync<ResponseMessage>(new ResponseMessage() { Text=$"{context.Message.Number}. response to request"});
        }
    }
}
