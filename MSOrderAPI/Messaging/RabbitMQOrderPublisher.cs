using MSOrderAPI.Entities;
using MSOrderAPI.Messaging.Interfaces;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Exceptions;
using System.Text;

namespace MSOrderAPI.Messaging
{
    public class RabbitMQOrderPublisher : IOrderEventPublisher
    {
        private readonly string _hostname;
        private readonly string _exchangeName;

        public RabbitMQOrderPublisher(IConfiguration config)
        {
            _hostname = config["RabbitMQ:HostName"] ?? "localhost";
            _exchangeName = "order_events";
        }

        public async Task PublishOrderCreatedAsync(Order order)
        {
            var factory = new ConnectionFactory() { HostName = _hostname };

            try
            {
                await using var connection = await factory.CreateConnectionAsync();
                await using var channel = await connection.CreateChannelAsync();


                await channel.ExchangeDeclareAsync(_exchangeName, ExchangeType.Fanout);

                var message = JsonConvert.SerializeObject(order);
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
