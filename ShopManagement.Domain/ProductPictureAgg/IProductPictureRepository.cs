using _0_Framework.Domain;
using ShopManagement.Application.Contracts.ProductPicture;

namespace ShopManagement.Domain.ProductPictureAgg
{
    public interface IProductPictureRepository : IRepository<long, ProductPicture>
    {
        EditProductPicture GetDetials(long id);
        ProductPicture GetWithProductAndCategory(long id);
        
        List<ProductPictureViewModel> Search(ProductPictureSearchModel searchModel);
    }
}
