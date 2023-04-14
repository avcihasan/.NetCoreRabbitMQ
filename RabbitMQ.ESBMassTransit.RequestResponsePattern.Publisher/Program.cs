using MassTransit;
using RabbitMQ.ESBMassTransit.Shared.RequestResponsePatterMessages;

string rabbitMQUri = "amqps://dqmpnror:GjrEnPhUvvnz4LGpp_CneJ0HFQujHrTt@puffin.rmq2.cloudamqp.com/dqmpnror";
string requestQueue = "request-queue";

IBusControl bus =Bus.Factory.CreateUsingRabbitMq(factory =>
{
    factory.Host(rabbitMQUri);
});

await bus.StartAsync();

var request = bus.CreateRequestClient<RequestMessage>(new Uri($"{rabbitMQUri}/{requestQueue}") );

int i = 1;
while (true)
{
    await Task.Delay(250);
    var response = await request.GetResponse<ResponseMessage>(new RequestMessage() {Number=i,Text=$"{i++}. request !" });

    Console.WriteLine($"Response : {response.Message.Text}");
}

