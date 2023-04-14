using MassTransit;
using RabbitMQ.ESBMassTransit.Shared.Messages;

string rabbitMQUri = "amqps://dqmpnror:GjrEnPhUvvnz4LGpp_CneJ0HFQujHrTt@puffin.rmq2.cloudamqp.com/dqmpnror";

string queueName = "mass-transit-queue";

IBusControl bus=Bus.Factory.CreateUsingRabbitMq(factory =>
{
    factory.Host(rabbitMQUri);
});


ISendEndpoint sendEndpoint=await bus.GetSendEndpoint(new($"{rabbitMQUri}/{queueName}"));

Console.Write("Gönderilecek Mesaj :");
string message = Console.ReadLine();

await sendEndpoint.Send<IMessage>(new ExampleMessage() { Text=message});


Console.Read();