using ProductViewer.Database.Entities;
using ProductViewer.Services.ProductService.Models;
using ProductViewer.Services.ProductService.Models.Query;
using ProductViewer.Services.Response;

namespace ProductViewer.Services.ProductService
{
    public interface IProductService
    {
        Task<ResponseService<long>> Create(CreateProductHttpPostModel vm);
        Task<ResponseService> Delete(DeleteProductHttpPostModel vm);
        Task<ResponseService> Update(UpdateProductHttpPostModel vm);

        Task<ResponseService<ProductEntity>> GetById(long id);
        Task<IEnumerable<ProductEntity>> GetAll();
        Task<IEnumerable<ProductEntity>> GetAll(int page = 1);
        Task<ProductOrderingHttpGetModel> GetAll(ProductOrderingQueryModel vm);
    }
}