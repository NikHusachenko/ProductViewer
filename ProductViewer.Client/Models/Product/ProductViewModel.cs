namespace ProductViewer.Client.Models.Product
{
    public class ProductViewModel
    {
        public long Id { get; set; }
        public int Index { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }
        public float Rate { get; set; }
        public int Count { get; set; }
        public string ImageName { get; set; } = string.Empty;
        public string ImageExtension { get; set; } = string.Empty;
    }
}