using CaseManagement.WorkerProcess.Procesess;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;

using System.Text;


namespace CaseManagement.WorkerProcess
{
    public class Worker
    {
        private static string _defaultQueue = "task_queue";

        public static void Main()
        {
            RunWorker();
        }

        private static void RunWorker()
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {

                ConfigureQueueChannel(channel);
                var consumer = new EventingBasicConsumer(channel);
                ProcessMessage(channel, consumer);
                PullMessageFromQueue(channel, consumer);

                Console.WriteLine(" Press [enter] to exit.");
                Console.ReadLine();
            }

        }

        private static void ConfigureQueueChannel(IModel channel)
        {
            channel.QueueDeclare(queue: _defaultQueue,
                                 durable: true,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);
            Console.WriteLine("[*] Waiting for messages...");
         
        }

        private static void ProcessMessage(IModel channel, EventingBasicConsumer consumer)
        {

            consumer.Received += (model, ea) =>
            {
                var body = ea.Body;
                var message = Encoding.UTF8.GetString(body);

                var processor = new SimpleProcess();
                processor.DoProcess(message);

                channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
            };

            
        }
        private static void PullMessageFromQueue(IModel channel, EventingBasicConsumer consumer)
        {
            channel.BasicConsume(queue: _defaultQueue,
                                     noAck: false,
                                     consumer: consumer);
        }
    }
}
