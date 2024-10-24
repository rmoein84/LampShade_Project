using _0_Framework.Application;
using _0_Framework.Application.ZarinPal;
using _01_LampShade.Query.Contracts;
using _01_LampShade.Query.Contracts.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using ShopManagement.Application.Contracts;
using ShopManagement.Application.Contracts.Order;
using ShopManagement.Domain.ProductAgg;

namespace ServiceHost.Pages
{
    [Authorize]
    public class CheckoutModel : PageModel
    {
        public Cart Cart;
        private readonly IAuthHelper _authHelper;
        private readonly ICartService _cartService;
        private readonly IProductQuery _productQuery;
        private readonly IZarinPalFactory _zarinPalFactory;
        private readonly IOrderApplication _orderApplication;
        private readonly ICartCalculatorService _cartCalculatorService;
        public CheckoutModel(ICartCalculatorService cartCalculatorService, ICartService cartService, IProductQuery productQuery, IOrderApplication orderApplication, IZarinPalFactory zarinPalFactory, IAuthHelper authHelper)
        {
            _cartCalculatorService = cartCalculatorService;
            _cartService = cartService;
            _productQuery = productQuery;
            _orderApplication = orderApplication;
            _zarinPalFactory = zarinPalFactory;
            _authHelper = authHelper;
        }

        public void OnGet()
        {
            var value = Request.Cookies["cart-items"];
            if (!string.IsNullOrWhiteSpace(value))
            {
                var cartItems = JsonConvert.DeserializeObject<List<CartItem>>(value);
                Cart = _cartCalculatorService.ComputeCart(cartItems);
                _cartService.Set(Cart);
            }
        }
        public IActionResult OnPostPay(int paymentMethod)
        {
            var cart = _cartService.Get();
            cart.SetPaymentMethod(paymentMethod);
            var result = _productQuery.CheckInventoryStatus(cart.Items);
            if (result.Any(x => !x.IsInStock))
                return RedirectToPage("./Cart");
            var orderId = _orderApplication.PlaceOrder(cart);
            if (paymentMethod == 1)
            {
                var paymentResponse = _zarinPalFactory.CreatePaymentRequest(
                    cart.PayAmount.ToString(), "", "", 
                    "خرید از درگاه لوازم خانگی و دکوری", orderId);

                return Redirect(
                        $"https://{_zarinPalFactory.Prefix}.zarinpal.com/pg/StartPay/{paymentResponse.Authority}");
            }
            else
            {
                var paymentResult = new PaymentResult()
                    .Succeeded("سفارش شما با موفقیت انجام شد", null);
                return RedirectToPage("./PaymentResult", paymentResult);
            }
        }
        public IActionResult OnGetCallBack([FromQuery] string authority, [FromQuery] string status, [FromQuery] long oId)
        {
            var payAmount = _orderApplication.GetAmountBy(oId);
            var verificationResponse = _zarinPalFactory.CreateVerificationRequest(authority, payAmount.ToString());

            var result = new PaymentResult();
            if (status == "OK" && verificationResponse.Code == 100)
            {
                var issueTrackingNo = _orderApplication.PaymentSucceeded(oId, verificationResponse.Ref_id);
                Response.Cookies.Delete("cart-items");
                result = result.Succeeded("پرداخت موفقیت آمیز بود", issueTrackingNo);
                return RedirectToPage("/PaymentResult", result);
            }
            result = result.Failed("پرداخت ناموفق بود");
            return RedirectToPage("/PaymentResult", result);
        }
    }
}
