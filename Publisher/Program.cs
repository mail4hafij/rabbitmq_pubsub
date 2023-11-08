using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using Newtonsoft.Json;
using System.Text;

namespace Publisher
{
    public class Program
    {
        static void Main(string[] args)
        {
            var configuration = new ConfigurationBuilder().
                AddJsonFile("appsettings.json", false, true).
                Build();


            // RabbitMQ publish start
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
                var exchange = configuration["RabbitExchange"];
                var route = configuration["RabbitRoute"];
                var rm = new RabbitMessage()
                {
                    Name = "test",
                    Message = "test message"
                };

                var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(rm));
                channel.ExchangeDeclare(exchange: exchange, type: ExchangeType.Fanout);
                channel.BasicPublish(exchange: exchange, route, null, body);
            }
        }


    }

    public class RabbitMessage
    {
        public string Name { get; set; }
        public string Message { get; set; }
    }
}