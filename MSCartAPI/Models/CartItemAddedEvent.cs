namespace MSCartAPI.Models
{
    public record CartItemAddedEvent(string CartId, int ProductId, int Quantity, DateTime AddedAt);

}
