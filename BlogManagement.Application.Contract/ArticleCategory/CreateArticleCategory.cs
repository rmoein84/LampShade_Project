using _0_Framework.Application;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogManagement.Application.Contract.ArticleCategory
{
    public class CreateArticleCategory
    {
        [Required(ErrorMessage = ValidationMessages.IsRequire)]
        public string Title { get; set; }
        public IFormFile Picture { get; set; }

        [Required(ErrorMessage = ValidationMessages.IsRequire)]
        public string PictureAlt { get; set; }

        [Required(ErrorMessage = ValidationMessages.IsRequire)]
        public string PictureTitle { get; set; }

        [Required(ErrorMessage = ValidationMessages.IsRequire)]
        public string Description { get; set; }

        [Required(ErrorMessage = ValidationMessages.IsRequire)]
        public int ShowOrder { get; set; }

        [Required(ErrorMessage = ValidationMessages.IsRequire)]
        public string Slug { get; set; }

        [Required(ErrorMessage = ValidationMessages.IsRequire)]
        public string Keywords { get; set; }

        [Required(ErrorMessage = ValidationMessages.IsRequire)]
        public string MetaDescription { get; set; }
        public string CanonicalAddress { get; set; }
    }
}
