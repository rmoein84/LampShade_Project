using ShopManagement.Application.Contracts.Order;

namespace _01_LampShade.Query.Contracts.Product
{
    public interface IProductQuery
    {
        List<ProductQueryModel> GetLatestProducts();
        List<ProductQueryModel> Search(string value);
        ProductQueryModel GetDetails(string slug);
        List<CartItem> CheckInventoryStatus(List<CartItem> cartItems);
    }
}
