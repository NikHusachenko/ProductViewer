using ProductViewer.Database.Entities;

namespace ProductViewer.Services.ProductService.Models.Query
{
    public class ProductOrderingHttpGetModel
    {
        public IEnumerable<ProductEntity> Products { get; set; }
        public ProductOrderingQueryModel QueryFilters { get; set; }
    }
}