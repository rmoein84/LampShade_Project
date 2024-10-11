namespace _01_LampShade.Query.Contracts.Product
{
    public interface IProductQuery
    {
        List<ProductQueryModel> GetLatestProducts();
    }
}
