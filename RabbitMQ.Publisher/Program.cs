using RabbitMQ.Client;
using System.Text;


//Bağlantı Oluşturma
ConnectionFactory factory = new();
factory.Uri = new("amqps://dqmpnror:GjrEnPhUvvnz4LGpp_CneJ0HFQujHrTt@puffin.rmq2.cloudamqp.com/dqmpnror");

//Bağlantı Aktifleştirme ve Kanal Açma
using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();

//Queue Oluşturma
channel.QueueDeclare(queue: "First-Queue", exclusive: false); // queue adı First-Queue

//Queueya Mesaj Gönderme
//byte[] message =Encoding.UTF8.GetBytes("Merhaba");
//channel.BasicPublish(exchange:"",routingKey:"First-Queue",body:message);
//exchange:"" ile default olan direct exchange seçildi. Bu exchange rotueing key olarak kuyruk ismini aldığı için kuyurk ismi ilgili yere yazıldı.

for (int i = 0; i < 100; i++)
{
    Task.Delay(1000);
    byte[] message = Encoding.UTF8.GetBytes("Hi RabbitMQ" + i);
    channel.BasicPublish(exchange: "", routingKey: "First-Queue", body: message);
}


Console.Read();
