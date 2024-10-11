using _0_Framework.Application;
using _01_LampShade.Query.Contracts.Product;
using _01_LampShade.Query.Contracts.ProductCategory;
using DiscountManagement.Infrastructure.EFCore;
using InventoryManagement.Infrasturcture.EFCore;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Domain.ProductAgg;
using ShopManagement.Infrastructere.EFCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01_LampShade.Query.Query
{
    public class ProductCategoryQuery : IProductCategoryQuery
    {
        private readonly ShopContext _context;
        private readonly InventoryContext _inventoryContext;
        private readonly DiscountContext _discountContext;
        public ProductCategoryQuery(ShopContext context, InventoryContext inventoryContext, DiscountContext discountContext)
        {
            _context = context;
            _inventoryContext = inventoryContext;
            _discountContext = discountContext;
        }

        public List<ProductCategoryQueryModel> GetProductCategories()
        {
            return _context.ProductCategories
                .AsNoTracking()
                .Select(x => new ProductCategoryQueryModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    Picture = x.Picture,
                    PictureAlt = x.PictureAlt,
                    PictureTitle = x.PictureTitle,
                    Slug = x.Slug,
                }).ToList();
        }

        public List<ProductCategoryQueryModel> GetProductCategoriesWithProducts()
        {
            return _context.ProductCategories
                .AsNoTracking()
                .Include(x => x.Products)
                .ThenInclude(x => x.Category)
                .Select(x => new ProductCategoryQueryModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    Products = MapProducts(x.Products, _inventoryContext, _discountContext),
                }).ToList();
        }

        public ProductCategoryQueryModel GetProductCategoriesWithProductsBy(string slug)
        {
            var result = _context.ProductCategories
                .AsNoTracking()
                .Include(x => x.Products)
                .ThenInclude(x => x.Category)
                .Select(x => new ProductCategoryQueryModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    Products = MapProducts(x.Products, _inventoryContext, _discountContext),
                    Description = x.Description,
                    Keywords = x.Keywords,
                    MetaDescription = x.MetaDescription,
                    Slug = x.Slug,
                })
                .FirstOrDefault(x => x.Slug == slug);
            return result;
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
                    .Select(x => new { x.ProductId, x.DiscountRate, x.EndDate })
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
                    CategorySlug = product.Category.Slug,
                };
                if (productPrice != null)
                {
                    item.Price = productPrice.UnitPrice.ToMoney();
                    if (discount != null)
                    {
                        item.DiscountRate = discount.DiscountRate;
                        item.DiscountExpireDate = discount.EndDate.ToString();
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
