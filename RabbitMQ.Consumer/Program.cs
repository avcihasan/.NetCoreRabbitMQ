using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

#region Basic
////Bağlantı Oluşturma
//ConnectionFactory factory = new();
//factory.Uri = new("amqps://dqmpnror:GjrEnPhUvvnz4LGpp_CneJ0HFQujHrTt@puffin.rmq2.cloudamqp.com/dqmpnror");

////Bağlantı Aktifleştirme ve Kanal Açma
//using IConnection connection = factory.CreateConnection();
//using IModel channel = connection.CreateModel();

////Queue Oluşturma
//channel.QueueDeclare(queue: "First-Queue", exclusive: false); //Consumer ile publisher de kuyruklar birebir aynı olamlıdır

////Queueya Mesaj Alma
//BasicGetResult result = channel.BasicGet(queue: "First-Queue", false);
//EventingBasicConsumer consumer = new(channel);
//channel.BasicConsume(queue: "First-Queue", false, consumer);
//consumer.Received += (sender, e) =>
//{
//    // Kuyruğa gelen mesajların işlendiği yerdir 
//    //e.Body : Kuyuruktaki mesajın verisini  bütünsel olarak  getirir
//    //e.Body.Span ya da e.Body.ToArray() : Kuyruktaki  mesajın byte veririsini getirir

//    Console.WriteLine(Encoding.UTF8.GetString(e.Body.Span));
//};

//Console.Read();
#endregion


#region DirectExchange
//ConnectionFactory factory = new();
//factory.Uri = new("amqps://dqmpnror:GjrEnPhUvvnz4LGpp_CneJ0HFQujHrTt@puffin.rmq2.cloudamqp.com/dqmpnror");

//using IConnection connection = factory.CreateConnection();
//using IModel channel = connection.CreateModel();

////1.Adım
////
//channel.ExchangeDeclare(exchange: "dirext-exchange-example", type: ExchangeType.Direct);

////2.Adım
////
//string queueName = channel.QueueDeclare().QueueName;

////3.Adım
//channel.QueueBind(
//    queue:queueName,
//    exchange: "dirext-exchange-example",
//    routingKey: "dirext-queue-example");

//EventingBasicConsumer consumer = new(channel);
//channel.BasicConsume(
//    queue: queueName,
//    autoAck: true,
//    consumer:consumer) ;

//consumer.Received += (sender, e) =>
//{
//    string message = Encoding.UTF8.GetString(e.Body.Span);
//    Console.WriteLine(message);
//};
//Console.Read();

#endregion

#region FanoutExchange
ConnectionFactory factory = new();
factory.Uri = new("");

using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();

channel.ExchangeDeclare(exchange: "fanout-exhcange-example", type: ExchangeType.Fanout);

Console.Write("Kuyruk Adı : ");
string queueName = Console.ReadLine();

channel.QueueDeclare(queue:queueName,exclusive:false);

channel.QueueBind(
    queue:queueName,
    exchange: "fanout-exhcange-example",
    routingKey:String.Empty);

EventingBasicConsumer consumer = new(channel);
channel.BasicConsume(queue:queueName,autoAck:true,consumer:consumer);
consumer.Received += (sender, e) =>
{
    string message = Encoding.UTF8.GetString(e.Body.Span);
    Console.WriteLine(message);
};
Console.Read();
#endregion
