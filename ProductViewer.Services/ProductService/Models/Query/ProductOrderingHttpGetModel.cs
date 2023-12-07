using ProductViewer.Database.Entities;

namespace ProductViewer.Services.ProductService.Models.Query
{
    public class ProductOrderingHttpGetModel
    {
        public int PageCount { get; set; }
        public IEnumerable<ProductEntity> Products { get; set; }
        public ProductOrderingQueryModel QueryFilters { get; set; }
    }
}