using BlogManagement.Application.Contract.Article;
using BlogManagement.Application.Contract.ArticleCategory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contracts.Product;
using ShopManagement.Application.Contracts.ProductCategory;

namespace ServiceHost.Areas.Administration.Pages.Blog.Articles
{
    public class IndexModel : PageModel
    {
        private readonly IArticleCategoryApplication _articleCategoryApplication;
        protected readonly IArticleApplication _articleApplication;
        public SelectList ArticleCategories { get; set; }
        public List<ArticleViewModel> Articles { get; set; }
        public ArticleSearchModel SearchModel { get; set; }

        public IndexModel(IArticleCategoryApplication articleCategoryApplication, IArticleApplication articleApplication)
        {
            _articleCategoryApplication = articleCategoryApplication;
            _articleApplication = articleApplication;
        }


        public void OnGet(ArticleSearchModel searchModel)
        {
            ArticleCategories = new SelectList(_articleCategoryApplication.GetAll(), "Id", "Title");
            Articles = _articleApplication.Search(searchModel);
        }
    }
}
