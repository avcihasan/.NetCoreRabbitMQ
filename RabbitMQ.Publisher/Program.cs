using RabbitMQ.Client;
using System.Text;

#region Basic
////Bağlantı Oluşturma
//ConnectionFactory factory = new();
//factory.Uri = new("amqps://dqmpnror:GjrEnPhUvvnz4LGpp_CneJ0HFQujHrTt@puffin.rmq2.cloudamqp.com/dqmpnror");

////Bağlantı Aktifleştirme ve Kanal Açma
//using IConnection connection = factory.CreateConnection();
//using IModel channel = connection.CreateModel();

////Queue Oluşturma
//channel.QueueDeclare(queue: "First-Queue", exclusive: false); // queue adı First-Queue

////Queueya Mesaj Gönderme
////byte[] message =Encoding.UTF8.GetBytes("Merhaba");
////channel.BasicPublish(exchange:"",routingKey:"First-Queue",body:message);
////exchange:"" ile default olan direct exchange seçildi. Bu exchange rotueing key olarak kuyruk ismini aldığı için kuyurk ismi ilgili yere yazıldı.

//for (int i = 0; i < 100; i++)
//{
//    Task.Delay(1000);
//    byte[] message = Encoding.UTF8.GetBytes("Hi RabbitMQ" + i);
//    channel.BasicPublish(exchange: "", routingKey: "First-Queue", body: message);
//}
//Console.Read();
#endregion

#region DirectExchange
//ConnectionFactory factory = new();
//factory.Uri = new("amqps://dqmpnror:GjrEnPhUvvnz4LGpp_CneJ0HFQujHrTt@puffin.rmq2.cloudamqp.com/dqmpnror");

//using IConnection connection = factory.CreateConnection();
//using IModel channel = connection.CreateModel();

//channel.ExchangeDeclare(exchange:"dirext-exchange-example",type:ExchangeType.Direct);

//while (true)
//{
//    string message = Console.ReadLine();
//    byte[] byteMessage = Encoding.UTF8.GetBytes(message);

//    channel.BasicPublish(
//        exchange: "dirext-exchange-example",
//        routingKey: "dirext-queue-example",
//        body:byteMessage
//        );
//}
#endregion

#region FanoutExchange
//ConnectionFactory factory = new();
//factory.Uri = new("amqps://dqmpnror:GjrEnPhUvvnz4LGpp_CneJ0HFQujHrTt@puffin.rmq2.cloudamqp.com/dqmpnror");

//using IConnection connection =factory.CreateConnection();
//using IModel channel= connection.CreateModel();

//channel.ExchangeDeclare(exchange:"fanout-exhcange-example",type:ExchangeType.Fanout);

//for (int i = 0; i < 100; i++)
//{
//    await Task.Delay(250);
//    byte[] message = Encoding.UTF8.GetBytes($"Fanout Excahne {i}");

//    channel.BasicPublish(
//        exchange: "fanout-exhcange-example",
//        routingKey: String.Empty, 
//        body: message);
//}
//Console.Read();
#endregion

#region TopicExchange
ConnectionFactory factory= new();
factory.Uri = new("amqps://dqmpnror:GjrEnPhUvvnz4LGpp_CneJ0HFQujHrTt@puffin.rmq2.cloudamqp.com/dqmpnror");

using IConnection connection = factory.CreateConnection();  
using IModel channel = connection.CreateModel();

channel.ExchangeDeclare(exchange:"topic-exchange-example",type:ExchangeType.Topic);

for (int i = 0; i < 100; i++)
{
    await Task.Delay(250);
    byte[] message = Encoding.UTF8.GetBytes($"Topic Exchange {i}");

    Console.Write("Topic gir : ");
    string topic = Console.ReadLine();
    channel.BasicPublish(
        exchange: "topic-exchange-example",
        routingKey: topic,
        body:message);

}
Console.Read();
#endregion