using _0_Framework.Domain;
using BlogManagement.Application.Contract.ArticleCategory;

namespace BlogManagement.Domain.ArticleCategoryAgg
{
    public interface IArticleCategoryRepository : IRepository<long, ArticleCategory>
    {
        List<ArticleCategoryViewModel> GetAll();
        EditArticleCategory GetDetails(long id);
        string GetSlugBy(long id);
        List<ArticleCategoryViewModel> Search(ArticleCategorySearchModel searchModel);

    }
}

