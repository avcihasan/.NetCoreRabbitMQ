using MassTransit;
using RabbitMQ.ESBMassTransit.Consumer.Consumers;

string rabbitMQUri = "amqps://dqmpnror:GjrEnPhUvvnz4LGpp_CneJ0HFQujHrTt@puffin.rmq2.cloudamqp.com/dqmpnror";

string queueName = "mass-transit-queue";

IBusControl bus = Bus.Factory.CreateUsingRabbitMq(factory =>
{
    factory.Host(rabbitMQUri);

    factory.ReceiveEndpoint(queueName: queueName, configureEndpoint: endpoint =>
    {
        endpoint.Consumer<ExampleMessageConsumer>();
    });
});

await bus.StartAsync();

Console.Read();