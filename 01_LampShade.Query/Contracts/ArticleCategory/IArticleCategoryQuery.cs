namespace _01_LampShade.Query.Contracts.ArticleCategory
{
    public interface IArticleCategoryQuery
    {
        ArticleCategoryQueryModel GetDetails(string slug);
        List<ArticleCategoryQueryModel> GetAll();
    }
}
