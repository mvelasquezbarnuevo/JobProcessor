﻿using CaseManagement.Producer.Interfaces.Tasks;
using CaseManagement.Producer.Tasks;
using RabbitMQ.Client;
using System;
using System.Configuration;
using System.Text;

namespace CaseManagement.Producer
{
    public class Producer
    {
        private  string _defaultQueue = "task_queue";
        private  ConnectionFactory _factory;

        public Producer()
        {
        }

        public  void Run(string[] args)
        {
          

            Initialize();
            using (var connection = _factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                ConfigureQueueChannel(channel);
                var taskId = DateTime.Now.Second.ToString();
                PublishMessage(channel, new SimpleTask(taskId));
            }

          
        }

       

        private  void PublishMessage(IModel channel, ITask task)
        {
            if (channel == null)
                throw new ArgumentNullException("channel");

            if (task == null)
                throw new ArgumentNullException("task");

            string message = task.GetTaskType();
            var body = Encoding.UTF8.GetBytes(message);

            var properties = channel.CreateBasicProperties();
            properties.Persistent = true;

            channel.BasicPublish(exchange: "",
                                     routingKey: _defaultQueue,
                                     basicProperties: properties,
                                     body: body);
            Console.WriteLine(" [x] Sending: {0}", message);

        }

        private  void ConfigureQueueChannel(IModel channel)
        {
            if (channel == null)
                throw new ArgumentNullException("channel");

            channel.QueueDeclare(queue: _defaultQueue,
                                 durable: true,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);
        }

        private  void Initialize()
        {
            var serverName = ConfigurationManager.AppSettings["ServerName"];
            if (string.IsNullOrEmpty(serverName))
                throw new ArgumentException("Cannot be empty", "ServerName");

            Console.WriteLine(string.Format("Connecting to {0} server", serverName));

            _factory = new ConnectionFactory() { HostName = serverName };

        }
    }
}

