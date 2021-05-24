using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Design;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using WebApplication1.Data;
using WebApplication1.DataTransmission;

namespace WebApplication1.DataTransmission
{
    public class RPCClient : IMessageSystem
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly string _replyQueueName;
        private readonly EventingBasicConsumer _consumer;
        private readonly BlockingCollection<string> _respQueue = new BlockingCollection<string>();
        private readonly IBasicProperties _props;

        private static readonly Lazy<RPCClient> lazy = new Lazy<RPCClient>(() => new RPCClient());

        public static RPCClient Instance => lazy.Value;


        public RPCClient()
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _replyQueueName = _channel.QueueDeclare().QueueName;
            _consumer = new EventingBasicConsumer(_channel);

            _props = _channel.CreateBasicProperties();
            var correlationId = Guid.NewGuid().ToString();
            _props.CorrelationId = correlationId;
            _props.ReplyTo = _replyQueueName;

            _consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var response = Encoding.UTF8.GetString(body);
                if (ea.BasicProperties.CorrelationId == correlationId)
                {
                    _respQueue.Add(response);
                }
            };
        }


        public string Call(string message)
        {
            var messageBytes = Encoding.UTF8.GetBytes(message);
            _channel.BasicPublish(
                exchange: "",
                routingKey: "rpc_queue",
                basicProperties: _props,
                body: messageBytes);

            _channel.BasicConsume(
                consumer: _consumer,
                queue: _replyQueueName,
                autoAck: true);

            return _respQueue.Take();
        }


        public string SendMessage(string message)
        {
            var messageBytes = Encoding.UTF8.GetBytes(message);
            _channel.BasicPublish(
                exchange: "",
                routingKey: "rpc_queue",
                basicProperties: _props,
                body: messageBytes);

            _channel.BasicConsume(
                consumer: _consumer,
                queue: _replyQueueName,
                autoAck: true);

            return _respQueue.Take();
        }
        
        public void Close()
        {
            _connection.Close();
        }

        public void Add(DbObject temp)
        {
            string message = "Add#";
            message = message + temp.ConvertToJson();
            var response = SendMessage(message);
            Console.WriteLine( response == "Received:Add" ? "Successful Add" : "Add Failed");
        }

        public void update(DbObject temp)
        {
            string message = "Update#";
            message = message + temp.ConvertToJson();
            var response = SendMessage(message);
            Console.WriteLine(response == "Received#Update" ? "Successful Add" : "Add Failed");
        }

        public void Remove(string ID)
        {
            string message = "Remove#"+ID;
            string response = SendMessage(message);
            Console.WriteLine(response == ($"Received#Remove") ? "Successful Remove" : "Remove Failed");
        }

        public T Find<T>(string ID)
        {
            string message = "Get#"+ID;
            string response = SendMessage(message);
            string[] words = response.Split("#");

            return JsonConvert.DeserializeObject<T>(words[1]);
        }

        public List<T> ToListAsync<T>()
        {
            string message = "List";
            string response = SendMessage(message);
            string[] words = response.Split("#");

            return JsonConvert.DeserializeObject<List<T>>(words[1]);
        }
    }
}

