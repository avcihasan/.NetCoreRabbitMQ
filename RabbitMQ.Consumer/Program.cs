﻿using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

//Bağlantı Oluşturma
ConnectionFactory factory = new();
factory.Uri = new("amqps://dqmpnror:GjrEnPhUvvnz4LGpp_CneJ0HFQujHrTt@puffin.rmq2.cloudamqp.com/dqmpnror");

//Bağlantı Aktifleştirme ve Kanal Açma
using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();

//Queue Oluşturma
channel.QueueDeclare(queue: "First-Queue", exclusive: false); //Consumer ile publisher de kuyruklar birebir aynı olamlıdır

//Queueya Mesaj Alma
BasicGetResult result = channel.BasicGet(queue: "First-Queue", false);
EventingBasicConsumer consumer = new(channel);
channel.BasicConsume(queue: "First-Queue", false, consumer);
consumer.Received += (sender, e) =>
{
    // Kuyruğa gelen mesajların işlendiği yerdir 
    //e.Body : Kuyuruktaki mesajın verisini  bütünsel olarak  getirir
    //e.Body.Span ya da e.Body.ToArray() : Kuyruktaki  mesajın byte veririsini getirir

    Console.WriteLine(Encoding.UTF8.GetString(e.Body.Span));
};

Console.Read();