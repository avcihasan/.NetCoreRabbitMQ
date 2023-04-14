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

//EventingBasicConsumer consumer = new(channel);
//channel.BasicConsume(queue:queueName,autoAck:true,consumer:consumer);

//consumer.Received += (sender, e) =>
//{
//    Console.WriteLine(Encoding.UTF8.GetString(e.Body.Span)); ;
//};


#endregion


#region Publish/Subscribe (Pub/Sub) Tasarımı
//string exchangeName = "pub-sub-exchange";

//channel.ExchangeDeclare(exchange: exchangeName, type: ExchangeType.Fanout);
//string queueName = channel.QueueDeclare().QueueName;

//channel.QueueBind(queue:queueName,exchange:exchangeName,routingKey:string.Empty);


//EventingBasicConsumer consumer = new(channel);
//channel.BasicConsume(queue:queueName,autoAck:true,consumer:consumer);

//consumer.Received += (sender,e) => 
//{
//    Console.WriteLine(Encoding.UTF8.GetString(e.Body.Span));
//};


#endregion



#region Work Queue Tasarımı
//string queueName = "work-queue";
//channel.QueueDeclare(queue: queueName, durable: false, exclusive: false, autoDelete: false);

//EventingBasicConsumer consumer = new(channel);
//channel.BasicConsume(queue:queueName,autoAck:true,consumer:consumer);

////Ölçeklendirme
//channel.BasicQos(prefetchCount:1,prefetchSize:0,global:false);

//consumer.Received += (sender, e) =>
//{
//    Console.WriteLine(Encoding.UTF8.GetString(e.Body.Span));
//};

#endregion


#region Request/Response Tasarımı

string requestQueueName = "request-response-queue";
channel.QueueDeclare(queue: requestQueueName, durable: false, exclusive: false, autoDelete: false);

EventingBasicConsumer consumer = new(channel);
channel.BasicConsume(queue:requestQueueName,autoAck:false,consumer:consumer);

consumer.Received += (sender, e) =>
{
    string message = Encoding.UTF8.GetString(e.Body.Span);
    Console.WriteLine(message);

    byte[] responseMessage= Encoding.UTF8.GetBytes($"işlem tamamlandı : {message}");
    IBasicProperties properties = channel.CreateBasicProperties();
    properties.CorrelationId = e.BasicProperties.CorrelationId;
    channel.BasicPublish(
        exchange:string.Empty,
        routingKey:e.BasicProperties.ReplyTo,
        body:responseMessage);
};

#endregion

Console.Read();