namespace ProductViewer.Services.ProductService.Models.Query
{
    public class ProductOrderingQueryModel
    {
        public int Page { get; set; } = 1;
        public float MinimalPrice { get; set; }
        public float MaximumPrice { get; set; }
        public float MinimalRate { get; set; }
        public bool IsAvailable { get; set; }
        public SortType Type { get; set; }
        public SortDirection Direction { get; set; }
    }
}