using Microsoft.AspNetCore.Mvc;
using MSCartAPI.Entities;
using MSCartAPI.Models;
using MSCartAPI.Services.Interfaces;

namespace MSCartAPI.Controllers
{
    [ApiController]
    [Route("api/cart")]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpGet]
        [Route("get")]
        public async Task<ActionResult<Cart>> GetCart([FromQuery] string cartId)
        {
            var cart = await _cartService.GetCartByIdAsync(cartId);
            if (cart == null)
                return NotFound();
            return Ok(cart);
        }

        [HttpGet]
        [Route("get-all")]
        public async Task<ActionResult<IEnumerable<Cart>>> GetAllCarts()
        {
            var carts = await _cartService.GetAllCartsAsync();
            return Ok(carts);
        }

        [HttpPost]
        [Route("create")]
        public async Task<ActionResult> CreateCart([FromBody] Cart cart)
        {
            await _cartService.AddCartAsync(cart);
            return CreatedAtAction(nameof(GetCart), new { cartId = cart.CartId }, cart);
        }

        [HttpPost]
        [Route("add-item")]
        public async Task<ActionResult> AddItemToCart([FromQuery] string cartId, [FromBody] CartItem cartItem)
        {
            await _cartService.AddItemToCartAsync(cartId, cartItem);
            return NoContent();
        }

        [HttpDelete]
        [Route("remove-item")]
        public async Task<ActionResult> RemoveItemFromCart([FromQuery] string cartId, [FromQuery] string cartItemId)
        {
            await _cartService.RemoveItemFromCartAsync(cartId, cartItemId);
            return NoContent();
        }

        [HttpDelete]
        [Route("clear")]
        public async Task<ActionResult> ClearCart([FromQuery] string cartId)
        {
            await _cartService.ClearCartAsync(cartId);
            return NoContent();
        }

        [HttpGet]
        [Route("get-with-products")]
        public async Task<ActionResult<CartWithProductDetailsDto>> GetCartWithProducts([FromQuery] string cartId)
        {
            var cart = await _cartService.GetCartWithProductDetailsAsync(cartId);
            if (cart == null)
                return NotFound();
            return Ok(cart);
        }
    }
}
