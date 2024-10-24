using _0_Framework.Infrastructure;
using _01_LampShade.Query.Contracts;
using _01_LampShade.Query.Contracts.Product;
using _01_LampShade.Query.Contracts.ProductCategory;
using _01_LampShade.Query.Contracts.Slide;
using _01_LampShade.Query.Query;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ShopManagement.Application;
using ShopManagement.Application.Contracts.Order;
using ShopManagement.Application.Contracts.Product;
using ShopManagement.Application.Contracts.ProductCategory;
using ShopManagement.Application.Contracts.ProductPicture;
using ShopManagement.Application.Contracts.Slide;
using ShopManagement.Configuration.Permissions;
using ShopManagement.Domain.OrderAgg;
using ShopManagement.Domain.ProductAgg;
using ShopManagement.Domain.ProductCategoryAgg;
using ShopManagement.Domain.ProductPictureAgg;
using ShopManagement.Domain.Services;
using ShopManagement.Domain.SlideAgg;
using ShopManagement.Infrastructere.EFCore;
using ShopManagement.Infrastructere.EFCore.Repository;
using ShopManagement.Infrastructure.AccountAcl;
using ShopManagement.Infrastructure.InventoryAcl;

namespace ShopManagement.Configuration
{
    public class ShopManagementBootstrapper
    {
        public static void Configure(IServiceCollection services, string connectionString)
        {
            /* ProductCategory Configuration */
            services.AddTransient<IProductCategoryApplication, ProductCategoryApplication>();
            services.AddTransient<IProductCategoryRepository, ProductCategoryRepository>();

            /* Product Configuration */
            services.AddTransient<IProductApplication, ProductApplication>();
            services.AddTransient<IProductRepository, ProductRepository>();

            /* ProductPicture Configuration */
            services.AddTransient<IProductPictureApplication, ProductPictureApplication>();
            services.AddTransient<IProductPictureRepository, ProductPictureRepository>();

            /* Slide Configuration */
            services.AddTransient<ISlideApplication, SlideApplication>();
            services.AddTransient<ISlideRepository, SlideRepository>();

            /* Order Configuration */
            services.AddTransient<IOrderApplication, OrderApplication>();
            services.AddTransient<IOrderRepository, OrderRepository>();

            /* CartCalculator Configuration */
            services.AddTransient<ICartCalculatorService, CartCalculatorService>();

            services.AddSingleton<ICartService, CartService>();
            services.AddTransient<IShopInventoryAcl, ShopInvenoryAcl>();
            services.AddTransient<IShopAccountAcl, ShopAccountAcl>();

            /* Queries Configuration */
            services.AddTransient<ISlideQuery, SlideQuery>();
            services.AddTransient<IProductCategoryQuery, ProductCategoryQuery>();
            services.AddTransient<IProductQuery, ProductQuery>();

            services.AddTransient<IPermissionExposer, ShopPermissionExposer>();


            /* DBContext Configuration */
            services.AddDbContext<ShopContext>(x => x.UseSqlServer(connectionString));
        }
    }
}
