using Microsoft.AspNetCore.Connections;
using MSOrderAPI.Entities;

namespace MSOrderAPI.Messaging.Interfaces
{
    public interface IOrderEventPublisher
    {
        Task PublishOrderCreatedAsync(Order order);
    }
}
