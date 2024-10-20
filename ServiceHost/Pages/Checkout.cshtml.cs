using _01_LampShade.Query.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using ShopManagement.Application.Contracts.Order;
using ShopManagement.Domain.ProductAgg;

namespace ServiceHost.Pages
{
    public class CheckoutModel : PageModel
    {
        public Cart Cart;
        private readonly ICartCalculatorService _cartCalculatorService;

        public CheckoutModel(ICartCalculatorService cartCalculatorService)
        {
            _cartCalculatorService = cartCalculatorService;
        }

        public void OnGet()
        {
            var value = Request.Cookies["cart-items"];
            if (!string.IsNullOrWhiteSpace(value))
            {
                var cartItems = JsonConvert.DeserializeObject<List<CartItem>>(value);
                Cart = _cartCalculatorService.ComputeCart(cartItems);
            }
        }
    }
}
