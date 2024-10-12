using _0_Framework.Application;
using Microsoft.AspNetCore.Http;
using ShopManagement.Application.Contracts.Product;
using System.ComponentModel.DataAnnotations;

namespace ShopManagement.Application.Contracts.ProductPicture
{
    public class CreateProductPicture
    {
        [Range(1, long.MaxValue, ErrorMessage = ValidationMessages.IsRequire)]
        public long ProductId { get; set; }

        [FileExtensionLimitation([".jpeg", ".jpg", ".png"], ErrorMessage = ValidationMessages.InvalidFileFormat)]
        [MaxFileSize(1 * 1024 * 1024, ErrorMessage = ValidationMessages.MaxFileSize)]
        public IFormFile Picture { get; set; }

        [Required(ErrorMessage = ValidationMessages.IsRequire)]
        public string PictureAlt { get; set; }
        [Required(ErrorMessage = ValidationMessages.IsRequire)]
        public string PictureTitle { get; set; }
        public List<ProductViewModel> Products { get; set; }
    }
}
