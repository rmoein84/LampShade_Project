using _01_LampShade.Query.Contracts.Product;
using _01_LampShade.Query.Contracts.ProductCategory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ShopManagement.Application.Contracts.Order;
using System.Net;

namespace ServiceHost.Pages
{
    public class CartModel : PageModel
    {
        private readonly IProductQuery _productQuery;

        public List<CartItem> Products = new List<CartItem>();
        public CartModel(IProductQuery productQuery)
        {
            _productQuery = productQuery;
        }

        public void OnGet()
        {
            var value = Request.Cookies["cart-items"];
            if (!string.IsNullOrWhiteSpace(value))
            {
                var cartItems = JsonConvert.DeserializeObject<List<CartItem>>(value);
                Products = _productQuery.CheckInventoryStatus(cartItems);
            }
        }
        public IActionResult OnGetGoToCheckOut()
        {
            var value = Request.Cookies["cart-items"];
            if (!string.IsNullOrWhiteSpace(value))
            {
                var cartItems = JsonConvert.DeserializeObject<List<CartItem>>(value);
                Products = _productQuery.CheckInventoryStatus(cartItems);
            }
            if (Products.Any(x => !x.IsInStock))
                return RedirectToPage("/Cart");
            return RedirectToPage("/Checkout");
        }
    }
}
