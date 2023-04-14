using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

ConnectionFactory factory = new();
factory.Uri = new("amqps://dqmpnror:GjrEnPhUvvnz4LGpp_CneJ0HFQujHrTt@puffin.rmq2.cloudamqp.com/dqmpnror");

using IConnection conneciton = factory.CreateConnection();
using IModel channel = conneciton.CreateModel();

#region P2P (Point to Point) Tasarımı

//string queueName = "point-to-point";

//channel.QueueDeclare(queue:queueName,durable:false,exclusive:false,autoDelete:false);

//byte[] message = Encoding.UTF8.GetBytes("Merhaba");
//channel.BasicPublish(exchange: string.Empty, routingKey: queueName, body: message);

#endregion

#region Publish/Subscribe (Pub/Sub) Tasarımı
//string exchangeName = "pub-sub-exchange";
//channel.ExchangeDeclare(exchange:exchangeName,type:ExchangeType.Fanout);

//byte[] message = Encoding.UTF8.GetBytes("Merhaba");
//channel.BasicPublish(exchange:exchangeName,routingKey:string.Empty,body:message);

#endregion

#region Work Queue Tasarımı
//string queueName = "work-queue";
//channel.QueueDeclare(queue:queueName,durable:false,exclusive:false,autoDelete:false);

//for (int i = 0; i < 100; i++)
//{
//    byte[] message = Encoding.UTF8.GetBytes($"Merhaba {i}");
//    channel.BasicPublish(exchange:string.Empty,routingKey:queueName,body:message);
//}

#endregion

#region Request/Response Tasarımı

string requestQueueName = "request-response-queue";
channel.QueueDeclare(queue: requestQueueName, durable: false, exclusive: false, autoDelete: false);

string replyQueueName = channel.QueueDeclare().QueueName;
string correlationId = Guid.NewGuid().ToString();

#region Request Oluşturma ve Gönderme

IBasicProperties properties = channel.CreateBasicProperties();
properties.CorrelationId = correlationId;
properties.ReplyTo = replyQueueName;

for (int i = 0; i < 100; i++)
{
    byte[] message = Encoding.UTF8.GetBytes($"Merhaba {i}");
    channel.BasicPublish(exchange: string.Empty, routingKey: replyQueueName, body: message, basicProperties: properties);
}
#endregion
#region Response Kuyruğu Dinleme

EventingBasicConsumer consumer = new(channel);
channel.BasicConsume(queue: replyQueueName, autoAck: true, consumer: consumer);

consumer.Received += (sender, e) =>
{
    Console.WriteLine(Encoding.UTF8.GetString(e.Body.Span)); ;
};

#endregion
#endregion



Console.Read();