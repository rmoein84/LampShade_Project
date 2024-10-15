using _01_LampShade.Query.Contracts.Article;
using _01_LampShade.Query.Contracts.ArticleCategory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages
{
    public class ArticleModel : PageModel
    {
        public ArticleQueryModel Article {  get; set; }
        public List<ArticleQueryModel> LatestArticles {  get; set; }
        public List<ArticleCategoryQueryModel> ArticleCategories { get; set; }
        private readonly IArticleQuery _articleQuery;
        private readonly IArticleCategoryQuery _articleCategoryQuery;

        public ArticleModel(IArticleQuery articleQuery, IArticleCategoryQuery articleCategoryQuery)
        {
            _articleQuery = articleQuery;
            _articleCategoryQuery = articleCategoryQuery;
        }

        public void OnGet(string slug)
        {
            Article = _articleQuery.GetArticleBy(slug);
            LatestArticles = _articleQuery.LatestArticles();
            ArticleCategories = _articleCategoryQuery.GetAll();
        }
    }
}
