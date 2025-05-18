using MSProductAPI.Models.Events;

namespace MSProductAPI.Messaging.Interfaces
{
    public interface IRabbitMQPublisher
    {
        Task PublishProductCreatedAsync(ProductCreatedEvent productEvent);
    }
}
