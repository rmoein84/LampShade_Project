using _0_Framework.Domain;
using ShopManagement.Application.Contracts.Product;

namespace ShopManagement.Domain.ProductAgg
{
    public interface IProductRepository : IRepository<long, Product>
    {
        List<ProductViewModel> Search(ProductSearchModel searchMoodel);
        EditProduct GetDetails(long id);
        Product GetWithCategory(long id);
        List<ProductViewModel> GetProducts();
    }
}
