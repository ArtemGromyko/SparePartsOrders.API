using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using SparePartsOrders.BLL.Contracts;
using SparePartsOrders.DAL.Contracts;
using SparePartsOrders.DAL.Entities;
using SparePartsOrders.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SparePartsOrders.BLL.Services
{
    public class OrdersReceiverService : BackgroundService
    {
        private IServiceProvider _sp;
        private ConnectionFactory _factory;
        private IConnection _connection;
        private IModel _channel;

        public OrdersReceiverService(IServiceProvider sp)
        {
            _sp = sp;
            var configuration = sp.GetRequiredService<IConfiguration>();
            _factory = new ConnectionFactory()
            {
                HostName = configuration["RabbitMQ:hostName"],
                Port = int.Parse(configuration["RabbitMQ:port"])
            };
            _connection = _factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(queue: "orders", durable: false, exclusive: false, autoDelete: false, arguments: null);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            if (stoppingToken.IsCancellationRequested)
            {
                _channel.Dispose();
                _connection.Dispose();
                return Task.CompletedTask;
            }

            var consumer = new EventingBasicConsumer(_channel);

            consumer.Received += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine(message);
                var orderDto = JsonConvert.DeserializeObject<OrderDto>(message);

                using (var scope = _sp.CreateScope())
                {
                    var ordersRepository= scope.ServiceProvider.GetRequiredService<IOrderRepository>();
                    var mapper = scope.ServiceProvider.GetRequiredService<IMapper>();
                    var configuration = scope.ServiceProvider.GetRequiredService<IConfiguration>();

                    var order = mapper.Map<Order>(orderDto);
                    await ordersRepository.CreateOrderAsync(order);
                }
            };

            _channel.BasicConsume(queue: "orders", autoAck: true, consumer: consumer);

            return Task.CompletedTask;
        }
    }
}
