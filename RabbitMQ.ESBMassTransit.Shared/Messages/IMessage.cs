﻿namespace RabbitMQ.ESBMassTransit.Shared.Messages
{
    public interface IMessage
    {
        public string Text { get; set; }
    }
}
