using _0_Framework.Application;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace BlogManagement.Application.Contract.Article
{
    public class CreateArticle
    {
        [Required(ErrorMessage = ValidationMessages.IsRequire)]
        public string Title { get; set; }
        [Required(ErrorMessage = ValidationMessages.IsRequire)]
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public IFormFile Picture { get; set; }
        [Required(ErrorMessage = ValidationMessages.IsRequire)]
        public string PictureAlt { get; set; }
        [Required(ErrorMessage = ValidationMessages.IsRequire)]
        public string PictureTitle { get; set; }
        [Required(ErrorMessage = ValidationMessages.IsRequire)]
        public string PublishDate { get; set; }
        [Required(ErrorMessage = ValidationMessages.IsRequire)]
        public string Slug { get; set; }
        [Required(ErrorMessage = ValidationMessages.IsRequire)]
        public string Keywords { get; set; }
        [Required(ErrorMessage = ValidationMessages.IsRequire)]
        public string MetaDescription { get; set; }
        public string CanonicalAddress { get; set; }
        [Range(0, long.MaxValue, ErrorMessage = ValidationMessages.IsRequire)]
        public long CategoryId { get; set; }
    }
}
