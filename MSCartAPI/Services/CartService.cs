using MSCartAPI.Entities;
using MSCartAPI.Messaging;
using MSCartAPI.Models;
using MSCartAPI.Repositories.Interfaces;
using MSCartAPI.Services.Interfaces;

namespace MSCartAPI.Services
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _repository;
        private readonly IRabbitMQPublisher _publisher;
        private readonly IProductInfoService _productInfoService;


        public CartService(ICartRepository repository, IRabbitMQPublisher publisher, IProductInfoService productInfoService)
        {
            _repository = repository;
            _publisher = publisher;
            _productInfoService = productInfoService;
        }

        public async Task<Cart?> GetCartByIdAsync(string cartId)
        {
            return await _repository.GetCartByIdAsync(cartId);
        }

        public async Task<IEnumerable<Cart>> GetAllCartsAsync()
        {
            return await _repository.GetAllCartsAsync();
        }

        public async Task AddCartAsync(Cart cart)
        {
            await _repository.AddCartAsync(cart);
            await _repository.SaveChangesAsync();
        }

        public async Task AddItemToCartAsync(string cartId, CartItem cartItem)
        {
            var cart = await _repository.GetCartByIdAsync(cartId);
            if (cart == null)
                throw new KeyNotFoundException("Cart not found");

            cartItem.CartId = cartId;
            await _repository.AddCartItemAsync(cartItem);
            await _repository.SaveChangesAsync();

            var evt = new CartItemAddedEvent(cartId, cartItem.ProductId, cartItem.Quantity, DateTime.UtcNow);
            await _publisher.PublishCartItemAddedAsync(evt);
        }

        public async Task RemoveItemFromCartAsync(string cartId, string cartItemId)
        {
            var cart = await _repository.GetCartByIdAsync(cartId);
            if (cart == null)
                throw new KeyNotFoundException("Cart not found");

            await _repository.RemoveCartItemAsync(cartItemId);
            await _repository.SaveChangesAsync();
        }

        public async Task ClearCartAsync(string cartId)
        {
            var cart = await _repository.GetCartByIdAsync(cartId);
            if (cart == null)
                throw new KeyNotFoundException("Cart not found");

            foreach (var item in cart.Items)
            {
                await _repository.RemoveCartItemAsync(item.CartItemId);
            }
            await _repository.SaveChangesAsync();
        }

        public async Task<CartWithProductDetailsDto?> GetCartWithProductDetailsAsync(string cartId)
        {
            var cart = await _repository.GetCartByIdAsync(cartId);
            if (cart == null)
                return null;

            var detailedItems = new List<CartItemWithProductDto>();
            foreach (var item in cart.Items)
            {
                var product = await _productInfoService.GetProductByIdAsync(item.ProductId);
                detailedItems.Add(new CartItemWithProductDto
                {
                    CartItemId = item.CartItemId,
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    Product = product
                });
            }

            return new CartWithProductDetailsDto
            {
                CartId = cart.CartId,
                Items = detailedItems
            };
        }
    }

}
