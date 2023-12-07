using Newtonsoft.Json;

namespace ProductViewer.Client.Models.Product
{
    public class ProductResponseModel
    {
        [JsonProperty("PageCount")]
        public int PageCount { get; set; }

        [JsonProperty("Products")]
        public List<ProductViewModel> Products { get; set; }

        [JsonProperty("QueryFilters")]
        public ProductOrderModel Order { get; set; }

        public ProductResponseModel()
        {
            Products = new List<ProductViewModel>();
            Order = new ProductOrderModel();
        }
    }
}