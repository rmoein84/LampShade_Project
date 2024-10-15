using _01_LampShade.Query;
using _01_LampShade.Query.Contracts.Article;
using _01_LampShade.Query.Contracts.ArticleCategory;
using _01_LampShade.Query.Contracts.ProductCategory;
using Microsoft.AspNetCore.Mvc;

namespace ServiceHost.ViewComponents
{
    public class MenuViewComponent : ViewComponent
    {
        private readonly IProductCategoryQuery _productCategoryQuery;
        private readonly IArticleCategoryQuery _articleCategoryQuery;

        public MenuViewComponent(IArticleCategoryQuery articleCategoryQuery, IProductCategoryQuery productCategoryQuery)
        {
            _articleCategoryQuery = articleCategoryQuery;
            _productCategoryQuery = productCategoryQuery;
        }

        public IViewComponentResult Invoke()
        {
            var menuModel = new MenuModel
            {
                ArticleCategories = _articleCategoryQuery.GetAll(),
                ProductCategories = _productCategoryQuery.GetAll(),
            };
            return View(menuModel);
        }
    }
}
