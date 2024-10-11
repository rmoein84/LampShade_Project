using _01_LampShade.Query.Contracts.ProductCategory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages
{
    public class ProductCategoryModel : PageModel
    {
        private readonly IProductCategoryQuery _productCategoryQuery;
        public ProductCategoryQueryModel ProductCategory { get; set; }
        public ProductCategoryModel(IProductCategoryQuery productCategoryQuery)
        {
            _productCategoryQuery = productCategoryQuery;
        }

        public void OnGet(string slug)
        {
            ProductCategory = _productCategoryQuery.GetProductCategoriesWithProductsBy(slug);
        }
    }
}
