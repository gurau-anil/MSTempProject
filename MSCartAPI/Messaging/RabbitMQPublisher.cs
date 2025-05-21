using Microsoft.AspNetCore.Connections;
using System.Text.Json;
using System.Text;
using MSCartAPI.Models;
using RabbitMQ.Client;
using RabbitMQ.Client.Exceptions;
using Newtonsoft.Json;

namespace MSCartAPI.Messaging
{
    public class RabbitMQPublisher : IRabbitMQPublisher
    {
        private readonly string _hostname;
        private readonly string _exchangeName;

        public RabbitMQPublisher(IConfiguration config)
        {
            _hostname = config["RabbitMQ:HostName"] ?? "localhost";
            _exchangeName = "cart_events";
        }

        public async Task PublishCartItemAddedAsync(CartItemAddedEvent evt)
        {
            var factory = new ConnectionFactory() { HostName = _hostname };

            try
            {
                await using var connection = await factory.CreateConnectionAsync();
                await using var channel = await connection.CreateChannelAsync();

                await channel.ExchangeDeclareAsync(_exchangeName, ExchangeType.Fanout);

                var message = JsonConvert.SerializeObject(evt);
                var body = Encoding.UTF8.GetBytes(message);

                // Use the correct exchange name here
                await channel.BasicPublishAsync(
                    exchange: _exchangeName,
                    routingKey: string.Empty,
                    body: body
                );

                Console.WriteLine("[RabbitMQPublisher] Message published successfully.");
            }
            catch (OperationInterruptedException ex)
            {
                Console.WriteLine($"[RabbitMQPublisher] RabbitMQ operation error: {ex.Message}");
                throw;
            }
            catch (BrokerUnreachableException ex)
            {
                Console.WriteLine($"[RabbitMQPublisher] RabbitMQ broker unreachable: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[RabbitMQPublisher] Unexpected error: {ex.Message}");
                throw;
            }
        }

    }
}
