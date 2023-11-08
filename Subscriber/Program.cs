using Microsoft.Extensions.Configuration;
using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using Newtonsoft.Json;
using System.Text;

namespace Subscriber
{
    public class Program
    {
        static void Main(string[] args)
        {
            var configuration = new ConfigurationBuilder().
                AddJsonFile("appsettings.json", false, true).
                Build();


            // RabbitMQ subscribe start
            var _connectionFactory = new ConnectionFactory()
            {
                HostName = configuration["RabbitHost"],
                Port = int.Parse(configuration["RabbitPort"]),
                UserName = configuration["RabbitUsername"],
                Password = configuration["RabbitPassword"]
            };
            using (var connection = _connectionFactory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.ExchangeDeclare(configuration["RabbitExchange"], ExchangeType.Fanout);

                // Create and bind a queue to consume all the events from the publisher.
                var queueName = channel.QueueDeclare().QueueName;
                var exchange = configuration["RabbitExchange"];

                // Create different handlers depending on different routes
                var route = configuration["RabbitRoute"];
                channel.QueueBind(queueName, exchange, route);
                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) => ConsumeHandler(model, ea);
                /*
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    RabbitMessage rm = JsonConvert.DeserializeObject<RabbitMessage>(message);

                    Console.WriteLine(" [x] Received: {0}", rm.Name);
                };
                */
                channel.BasicConsume(queueName, autoAck: true, consumer);

                // hold the connection
                Console.ReadLine();
            }
        }

        public static void ConsumeHandler(object model, BasicDeliverEventArgs ea)
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            RabbitMessage rm = JsonConvert.DeserializeObject<RabbitMessage>(message);

            Console.WriteLine("[x] Received: {0}", rm.Name);
        }
    }

    public class RabbitMessage
    {
        public string Name { get; set; }
        public string Message { get; set; }
    }
}