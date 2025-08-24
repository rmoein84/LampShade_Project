using _01_LampShade.Query.Contracts.Product;
using Microsoft.AspNetCore.Mvc;

namespace ServiceApi.Controllers.Admin
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShopController : ControllerBase
    {
        private readonly IProductQuery _productQuery;

        public ShopController(IProductQuery productQuery)
        {
            _productQuery = productQuery;
        }

        #region Products

        [HttpPost("GetProducts")]
        public IActionResult GetProductsList()
        {
            var result = _productQuery.Search("");
            return Ok(result);
        }

        #endregion
    }
}
