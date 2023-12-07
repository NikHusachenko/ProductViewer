namespace ProductViewer.Client.Models.Product
{
    public class ProductOrderModel
    {
        public int Page { get; set; } = 1;
        public int PageCount { get; set; }
        public float MinimalPrice { get; set; }
        public float MaximumPrice { get; set; }
        public float MinimalRate { get; set; }
        public bool IsAvailable { get; set; }
        public string Type { get; set; } = string.Empty;
        public string Direction { get; set; } = string.Empty;
    }
}