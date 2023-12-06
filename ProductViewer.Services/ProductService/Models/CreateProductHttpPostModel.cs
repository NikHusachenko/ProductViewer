﻿using Microsoft.AspNetCore.Http;

namespace ProductViewer.Services.ProductService.Models
{
    public class CreateProductHttpPostModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }
        public float Rate { get; set; }
        public int Count { get; set; }
        public IFormFile Image { get; set; }
    }
}