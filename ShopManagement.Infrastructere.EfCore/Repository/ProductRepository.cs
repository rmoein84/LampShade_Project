using _0_Framework.Application;
using _0_Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Application.Contracts.Product;
using ShopManagement.Domain.ProductAgg;

namespace ShopManagement.Infrastructere.EFCore.Repository
{
    public class ProductRepository : RepositoryBase<long, Product>, IProductRepository
    {
        private readonly ShopContext _context;

        public ProductRepository(ShopContext shopContext) : base(shopContext)
        {
            _context = shopContext;
        }

        public EditProduct GetDetails(long id)
        {
            return _context.Products.Select(x => new EditProduct
            {
                Id = x.Id,
                Title = x.Title,
                Code = x.Code,
                CategoryId = x.CategoryId,
                Description = x.Description,
                Keywords = x.Keywords,
                MetaDescription = x.MetaDescription,
                Picture = x.Picture,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle,
                ShortDescription = x.ShortDescription,
                Slug = x.Slug,
            }).FirstOrDefault(x => x.Id == id);
        }

        public List<ProductViewModel> GetProducts()
        {
            return _context.Products.Select(x => new ProductViewModel
            {
                Id = x.Id,
                Title = x.Title,
            }).ToList();
        }

        public List<ProductViewModel> Search(ProductSearchModel searchMoodel)
        {
            var query = _context.Products.Include(x => x.Category).Select(x => new ProductViewModel
            {
                Id = x.Id,
                Title = x.Title,
                Code = x.Code,
                CategoryId = x.CategoryId,
                Picture = x.Picture,
                Category = x.Category.Title,
                CreationDate = x.CreationDate.ToFarsi()
            });
            if (!string.IsNullOrWhiteSpace(searchMoodel.Title))
                query = query.Where(x => x.Title.Contains(searchMoodel.Title));
            if (!string.IsNullOrWhiteSpace(searchMoodel.Code))
                query = query.Where(x => x.Code.Contains(searchMoodel.Code));
            if (searchMoodel.CategoryId != 0)
                query = query.Where(x => x.CategoryId == searchMoodel.CategoryId);
            return query.ToList();


        }
    }
}
