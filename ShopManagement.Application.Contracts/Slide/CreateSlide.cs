using _0_Framework.Application;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace ShopManagement.Application.Contracts.Slide
{
    public class CreateSlide
    {
        [FileExtensionLimitation([".jpeg", ".jpg", ".png"], ErrorMessage = ValidationMessages.InvalidFileFormat)]
        [MaxFileSize(3 * 1024 * 1024, ErrorMessage = ValidationMessages.MaxFileSize)]
        [Required(ErrorMessage = ValidationMessages.IsRequire)]
        public IFormFile Picture { get; set; }
        [Required(ErrorMessage = ValidationMessages.IsRequire)]
        public string PictureAlt { get; set; }
        [Required(ErrorMessage = ValidationMessages.IsRequire)]
        public string PictureTitle { get; set; }
        [Required(ErrorMessage = ValidationMessages.IsRequire)]
        public string Heading { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        [Required(ErrorMessage = ValidationMessages.IsRequire)]
        public string BtnText { get; set; }
        public string Link { get; set; }
    }
}
