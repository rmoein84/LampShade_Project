namespace _01_LampShade.Query.Contracts.Product
{
    public interface IProductQuery
    {
        List<ProductQueryModel> GetLatestProducts();
        List<ProductQueryModel> Search(string value);
        ProductQueryModel GetDetails(string slug);
    }
}
