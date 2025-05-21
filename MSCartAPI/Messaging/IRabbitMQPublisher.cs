using MSCartAPI.Models;

namespace MSCartAPI.Messaging
{
    public interface IRabbitMQPublisher
    {
        Task PublishCartItemAddedAsync(CartItemAddedEvent evt);
    }
}
