using _0_Framework.Domain;
using ShopManagement.Application.Contracts.ProductCategory;
using System.Linq.Expressions;

namespace ShopManagement.Domain.ProductCategoryAgg
{
    public interface IProductCategoryRepository : IRepository<long, ProductCategory>
    {
        EditProductCategory GetDetails(long id);
        List<ProductCategoryViewModel> GetProductCategories();
        string GetSlugBy(long id);
        List<ProductCategoryViewModel> Search(ProductCategorySearchModel searchModel);

    }
}
