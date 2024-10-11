using _01_LampShade.Query.Contracts.ProductCategory;
using Microsoft.AspNetCore.Mvc;
using ShopManagement.Application.Contracts.ProductCategory;

namespace ServiceHost.ViewComponents
{
    public class ProductCategoryWithProductViewComponent : ViewComponent
    {
        private readonly IProductCategoryQuery _productCategoryQuery;

        public ProductCategoryWithProductViewComponent(IProductCategoryQuery productCategoryQuery)
        {
            _productCategoryQuery = productCategoryQuery;
        }

        public IViewComponentResult Invoke()
        {
            var categories = _productCategoryQuery.GetProductCategoriesWithProducts();
            return View(categories);
        }
    }
}
