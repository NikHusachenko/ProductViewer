﻿namespace ProductViewer.Services.ProductService.Models.Query
{
    public class ProductOrderingQueryModel
    {
        public int Page { get; set; } = 1;
        public float MinimalPrice { get; set; }
        public float MaximumPrice { get; set; }
        public float MinimalRate { get; set; }
        public bool IsAvailable { get; set; }
        public string? Type { get; set; } = string.Empty;
        public string? Direction { get; set; } = string.Empty;
    }
}