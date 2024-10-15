namespace _01_LampShade.Query.Contracts.ProductCategory
{
    public interface IProductCategoryQuery
    {
        ProductCategoryQueryModel GetProductCategoriesWithProductsBy(string slug);
        List<ProductCategoryQueryModel> GetAll();
        List<ProductCategoryQueryModel> GetProductCategoriesWithProducts();
    }
}
