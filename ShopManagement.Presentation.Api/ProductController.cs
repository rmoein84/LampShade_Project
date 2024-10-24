﻿using _01_LampShade.Query.Contracts.Product;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ShopManagement.Presentation.Api
{

    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductQuery _productQuery;

        public ProductController(IProductQuery productQuery)
        {
            _productQuery = productQuery;
        }
        [HttpGet]
        public List<ProductQueryModel> GetLatestProducts()
        {
            return _productQuery.GetLatestProducts();
        }
    }
}
