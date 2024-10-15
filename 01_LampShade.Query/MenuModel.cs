using _01_LampShade.Query.Contracts.Article;
using _01_LampShade.Query.Contracts.ArticleCategory;
using _01_LampShade.Query.Contracts.ProductCategory;

namespace _01_LampShade.Query
{
    public class MenuModel
    {
        public List<ProductCategoryQueryModel> ProductCategories { get; set; }
        public List<ArticleCategoryQueryModel> ArticleCategories { get; set; }
    }
}
