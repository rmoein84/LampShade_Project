using _01_LampShade.Query.Contracts.Article;
using _01_LampShade.Query.Contracts.ArticleCategory;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages
{
    public class ArticleCategoryModel : PageModel
    {
        public ArticleCategoryQueryModel ArticleCategory;
        public List<ArticleCategoryQueryModel> ArticleCategories;
        public List<ArticleQueryModel> LatestArticles;
        private readonly IArticleCategoryQuery _articleCategoryQuery;
        private readonly IArticleQuery _articleQuery;
        public ArticleCategoryModel(IArticleCategoryQuery articleCategoryQuery, IArticleQuery articleQuery)
        {

            _articleCategoryQuery = articleCategoryQuery;
            _articleQuery = articleQuery;
        }

        public void OnGet(string slug)
        {
            ArticleCategory = _articleCategoryQuery.GetDetails(slug);
            ArticleCategories = _articleCategoryQuery.GetAll();
            LatestArticles = _articleQuery.LatestArticles();
        }
    }
}
