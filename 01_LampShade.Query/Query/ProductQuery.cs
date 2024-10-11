using _0_Framework.Application;
using _01_LampShade.Query.Contracts.Product;
using DiscountManagement.Infrastructure.EFCore;
using InventoryManagement.Infrasturcture.EFCore;
using ShopManagement.Domain.ProductAgg;
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
            var products = _context.Products.OrderByDescending(x=>x.Id).Take(6).ToList();
            return MapProducts(products, _inventoryContext, _discountContext);
        }
        private static List<ProductQueryModel> MapProducts(List<Product> products,
    InventoryContext inventoryContext, DiscountContext discountContext)
        {
            var result = new List<ProductQueryModel>();
            foreach (var product in products)
            {
                var productPrice = inventoryContext.Inventory.Select(x =>
                new { x.ProductId, x.UnitPrice }).FirstOrDefault(x =>
                x.ProductId == product.Id);
                var discount = discountContext.CustomerDiscounts
                    .Where(x => x.StartDate < DateTime.Now && x.EndDate > DateTime.Now)
                    .Select(x => new { x.ProductId, x.DiscountRate })
                    .FirstOrDefault(x => x.ProductId == product.Id);
                var item = new ProductQueryModel
                {
                    Id = product.Id,
                    Title = product.Title,
                    Category = product.Category.Title,
                    Picture = product.Picture,
                    PictureAlt = product.PictureAlt,
                    PictureTitle = product.PictureTitle,
                    Slug = product.Slug,
                };
                if (productPrice != null)
                {
                    item.Price = productPrice.UnitPrice.ToMoney();
                    if (discount != null)
                    {
                        item.DiscountRate = discount.DiscountRate;
                        var discountAmount = Math.Round((productPrice.UnitPrice * discount.DiscountRate) / 100);
                        item.PriceWithDiscount = (productPrice.UnitPrice - discountAmount).ToMoney();
                    }
                }
                result.Add(item);
            }
            return result;
        }
    }
}
