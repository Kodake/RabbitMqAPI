using System;
using System.Text;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Collections.Generic;

namespace RabbitConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory() {
                UserName = "qtnsvvhb",
                Password = "2WOHVBfXg77HUB7-rP9Llsfi4beUG-b6",
                VirtualHost = "qtnsvvhb",
                HostName = "qtnsvvhb",
                Uri = new Uri("amqps://qtnsvvhb:2WOHVBfXg77HUB7-rP9Llsfi4beUG-b6@clam.rmq.cloudamqp.com/qtnsvvhb")
            };
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: "DemoQueue",
                                         durable: false,
                                         exclusive: false,
                                         autoDelete: false,
                                         arguments: null);

                    var consumer = new EventingBasicConsumer(channel);
                    consumer.Received += (model, ea) =>
                    {
                        var body = ea.Body.ToArray();
                        var message = Encoding.UTF8.GetString(body);
                        List<Item> items = new List<Item>();
                        items = JsonConvert.DeserializeObject<List<Item>>(message);
                        foreach (Item item in items)
                        {
                            Console.WriteLine(" [x] Received {0}", item.Id + " " + item.Name);
                        }
                    };
                    channel.BasicConsume(queue: "DemoQueue",
                                         autoAck: true,
                                         consumer: consumer);

                    Console.WriteLine(" Press [enter] to exit.");
                    Console.ReadLine();
                }
            }
        }
    }
}
