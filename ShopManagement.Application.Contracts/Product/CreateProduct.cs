using _0_Framework.Application;
using ShopManagement.Application.Contracts.ProductCategory;
using System.ComponentModel.DataAnnotations;

namespace ShopManagement.Application.Contracts.Product
{
    public class CreateProduct
    {
        [Required(ErrorMessage = ValidationMessages.IsRequire)]
        public string Title { get; set; }
        [Required(ErrorMessage = ValidationMessages.IsRequire)]
        public string Code { get; set; }
        [Required(ErrorMessage = ValidationMessages.IsRequire)]
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public string Picture { get; set; }
        public string PictureAlt { get; set; }
        public string PictureTitle { get; set; }
        [Required(ErrorMessage = ValidationMessages.IsRequire)]
        public string Slug { get; set; }
        [Required(ErrorMessage = ValidationMessages.IsRequire)]
        public string Keywords { get; set; }
        [Required(ErrorMessage = ValidationMessages.IsRequire)]
        public string MetaDescription { get; set; }
        [Range(1, long.MaxValue, ErrorMessage = ValidationMessages.IsRequire)]
        public long CategoryId { get; set; }
        public List<ProductCategoryViewModel> Categories { get; set; }
    }
}
