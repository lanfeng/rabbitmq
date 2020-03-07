using System;
using RabbitMQ.Client;
using System.Text;

namespace Send
{
    class Sender
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Before sending...");

            var factory = new ConnectionFactory(){
                HostName = "localhost"
            };

            using(var connection = factory.CreateConnection()){
                using(var channel = connection.CreateModel()){
                    channel.QueueDeclare(
                        queue: "qhello",
                        durable:false,
                        exclusive: false,
                        autoDelete: false,
                        arguments:null
                    );

                    string message = "hello rabbit-mq";
                    var body = Encoding.UTF8.GetBytes(message);

                    channel.BasicPublish(
                        exchange:"",
                        routingKey: "hello",
                        basicProperties: null,
                        body: body
                    );

                    Console.WriteLine($"{message} sent");
                }
            }

            Console.WriteLine("After sending...");
            Console.ReadLine();
        }
    }
}
