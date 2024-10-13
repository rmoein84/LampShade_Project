using _0_Framework.Application;
using _01_LampShade.Query.Contracts.Product;
using DiscountManagement.Infrastructure.EFCore;
using InventoryManagement.Infrasturcture.EFCore;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Domain.ProductAgg;
using ShopManagement.Domain.ProductPictureAgg;
using ShopManagement.Infrastructere.EFCore;

namespace _01_LampShade.Query.Query
{
    public class ProductQuery : IProductQuery
    {
        private readonly ShopContext _context;
        private readonly InventoryContext _inventoryContext;
        private readonly DiscountContext _discountContext;
        public ProductQuery(ShopContext context, InventoryContext inventoryContext, DiscountContext discountContext)
        {
            _context = context;
            _inventoryContext = inventoryContext;
            _discountContext = discountContext;
        }

        public List<ProductQueryModel> GetLatestProducts()
        {
            var products = _context.Products
                .AsNoTracking()
                .Include(x => x.Category)
                .OrderByDescending(x => x.Id)
                .Take(6)
                .ToList();
            return MapProducts(products, _inventoryContext, _discountContext);
        }

        public List<ProductQueryModel> Search(string value)
        {
            var query = _context.Products
                .AsNoTracking()
                .Include(x => x.Category)
                .OrderByDescending(x => x.Id);
            List<Product> products;
            if (!string.IsNullOrWhiteSpace(value))
            {
                products = query.Where(x => x.Title.Contains(value)).ToList();
            }
            else
            {
                products = query.ToList();

            }
            return MapProducts(products, _inventoryContext, _discountContext);
        }
        private static List<ProductQueryModel> MapProducts(List<Product> products,
    InventoryContext inventoryContext, DiscountContext discountContext)
        {
            var result = new List<ProductQueryModel>();
            foreach (var product in products)
            {
                var inventory = inventoryContext.Inventory
                    .AsNoTracking()
                    .Select(x => new { x.ProductId, x.UnitPrice, x.InStock })
                    .FirstOrDefault(x => x.ProductId == product.Id);
                var discount = discountContext.CustomerDiscounts
                   .AsNoTracking()
                   .Where(x => x.StartDate < DateTime.Now && x.EndDate > DateTime.Now)
                   .Select(x => new { x.ProductId, x.DiscountRate, x.EndDate })
                   .FirstOrDefault(x => x.ProductId == product.Id);
                var item = new ProductQueryModel
                {
                    Id = product.Id,
                    Title = product.Title,
                    Category = product.Category.Title,
                    CategorySlug = product.Category.Slug,
                    Picture = product.Picture,
                    PictureAlt = product.PictureAlt,
                    PictureTitle = product.PictureTitle,
                    Slug = product.Slug,
                    Code = product.Code,
                    Description = product.Description,
                    ShortDescription = product.ShortDescription,
                    MetaDescription = product.MetaDescription,
                    Keywords = product.Keywords,
                };
                if (inventory != null)
                {
                    item.Price = inventory.UnitPrice.ToMoney();
                    item.IsInStock = inventory.InStock;
                    if (discount != null)
                    {
                        item.DiscountRate = discount.DiscountRate;
                        item.DiscountExpireDate = discount.EndDate.ToString();
                        var discountAmount = Math.Round((inventory.UnitPrice * discount.DiscountRate) / 100);
                        item.PriceWithDiscount = (inventory.UnitPrice - discountAmount).ToMoney();
                    }
                }
                if (product.ProductPictures != null)
                {
                    item.Pictures = MapProductPictures(product.ProductPictures);
                }
                result.Add(item);
            }
            return result;
        }

        private static List<ProductPictureQueryModel> MapProductPictures(List<ProductPicture> productPictures)
        {
            return productPictures
                .Where(x=>!x.IsRemoved)
                .Select(x=> new ProductPictureQueryModel
                {
                    Picture = x.Picture,
                    PictureAlt = x.PictureAlt,
                    PictureTitle = x.PictureTitle,
                    ProductId = x.ProductId,
                })
                .ToList();
        }
        public ProductQueryModel GetDetails(string slug)
        {
            var product = _context.Products
                .AsNoTracking()
                .Include(x => x.Category)
                .Include(x=>x.ProductPictures)
                .ToList();

            if (product == null) return new ProductQueryModel();

            return MapProducts(product, _inventoryContext, _discountContext)
                .FirstOrDefault(x => x.Slug == slug);
        }
    }
}
