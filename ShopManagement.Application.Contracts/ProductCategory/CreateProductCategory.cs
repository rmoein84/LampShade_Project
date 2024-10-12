using _0_Framework.Application;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace ShopManagement.Application.Contracts.ProductCategory
{
    public class CreateProductCategory
    {
        [Required(ErrorMessage = ValidationMessages.IsRequire)]
        public string Title { get; set; }
        public string Description { get; set; }
        [FileExtensionLimitation(new string[] { ".jpeg", ".jpg", ".png"}, ErrorMessage = ValidationMessages.InvalidFileFormat)]
        [MaxFileSize(3 * 1024 * 1024, ErrorMessage = ValidationMessages.MaxFileSize)]
        public IFormFile Picture { get; set; }
        public string PictureAlt { get; set; }
        public string PictureTitle { get; set; }
        [Required(ErrorMessage = ValidationMessages.IsRequire)]
        public string Keywords { get; set; }

        [Required(ErrorMessage = ValidationMessages.IsRequire)]
        public string MetaDescription { get; set; }

        [Required(ErrorMessage = ValidationMessages.IsRequire)]
        public string Slug { get; set; }
    }
}
