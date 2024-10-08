﻿using _0_Framework.Application;
using ShopManagement.Application.Contracts.Product;
using System.ComponentModel.DataAnnotations;

namespace ShopManagement.Application.Contracts.ProductPicture
{
    public class CreateProductPicture
    {
        [Range(1, long.MaxValue, ErrorMessage = ValidationMessages.IsRequire)]
        public long ProductId { get; set; }
        [Required(ErrorMessage = ValidationMessages.IsRequire)]
        public string Picture { get; set; }
        [Required(ErrorMessage = ValidationMessages.IsRequire)]
        public string PictureAlt { get; set; }
        [Required(ErrorMessage = ValidationMessages.IsRequire)]
        public string PictureTitle { get; set; }
        public List<ProductViewModel> Products { get; set; }
    }
}
