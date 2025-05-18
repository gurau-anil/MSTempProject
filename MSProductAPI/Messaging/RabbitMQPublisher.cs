using MSProductAPI.Messaging.Interfaces;
using MSProductAPI.Models.Events;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Exceptions;
using System.Text;

namespace MSProductAPI.Messaging
{
    public class RabbitMQPublisher : IRabbitMQPublisher
    {
        private readonly IConfiguration _configuration;
        private readonly string _hostname;
        private readonly string _exchangeName;

        public RabbitMQPublisher(IConfiguration configuration)
        {
            _configuration = configuration;
            _hostname = _configuration["RabbitMQ:HostName"] ?? "localhost";
            _exchangeName = _configuration["RabbitMQ:ExchangeName"] ?? "product_exchange_v2"; // Use a fresh name
        }

        public async Task PublishProductCreatedAsync(ProductCreatedEvent productEvent)
        {
            var factory = new ConnectionFactory() { HostName = _hostname };

            try
            {
                await using var connection = await factory.CreateConnectionAsync();
                await using var channel = await connection.CreateChannelAsync();


                await channel.ExchangeDeclareAsync(_exchangeName, ExchangeType.Fanout);

                var message = JsonConvert.SerializeObject(productEvent);
                var body = Encoding.UTF8.GetBytes(message);

                await channel.BasicPublishAsync(exchange: _exchangeName, routingKey: string.Empty, body: body);


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
