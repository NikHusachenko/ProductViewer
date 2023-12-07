using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using ProductViewer.Common;
using ProductViewer.DataAccess;
using ProductViewer.Database.Entities;
using ProductViewer.Services.ProductService.Models;
using ProductViewer.Services.ProductService.Models.Query;
using ProductViewer.Services.Response;

namespace ProductViewer.Services.ProductService
{
    public class ProductService : IProductService
    {
        private readonly IGenericRepository<ProductEntity> _productRepository;
        private readonly string _rootPath;

        public ProductService(IGenericRepository<ProductEntity> productRepository,
            IHostingEnvironment hostingEnvironment)
        {
            _productRepository = productRepository;
            _rootPath = hostingEnvironment.WebRootPath;
        }

        public async Task<ResponseService<long>> Create(CreateProductHttpPostModel vm)
        {
            ProductEntity dbRecord = await _productRepository.First("SELECT * FROM Products WHERE Title = @Title", new { Title = vm.Title });
            if (dbRecord != null)
            {
                return ResponseService<long>.Error(Errors.PRODUCT_ALREADY_EXISTS_ERROR);
            }

            dbRecord = new ProductEntity()
            {
                Description = vm.Description,
                Title = vm.Title,
                Count = vm.Count,
                Price = vm.Price,
                Rate = vm.Rate,
            };

            try
            {
                await _productRepository.Create(dbRecord);
            }
            catch (Exception ex)
            {
                return ResponseService<long>.Error(Errors.CANT_CREATE_PRODUCT_ERROR);
            }

            var response =  SaveImage(vm.Image, dbRecord.Id.ToString());
            if (response.IsError)
            {
                return ResponseService<long>.Error(response.ErrorMessage);
            }

            dbRecord.ImageExtension = response.Value.Extensions;
            dbRecord.ImageName = response.Value.Name;

            var saveResult = await Update(dbRecord);
            if (saveResult.IsError)
            {
                return ResponseService<long>.Error(saveResult.ErrorMessage);
            }
            return ResponseService<long>.Ok(dbRecord.Id);
        }

        public async Task<ResponseService> Delete(DeleteProductHttpPostModel vm)
        {
            ProductEntity dbRecord = await _productRepository.Find(vm.Id);
            if (dbRecord == null)
            {
                return ResponseService.Error(Errors.PRODUCT_NOT_FOUND_ERROR);
            }

            try
            {
                await _productRepository.Delete(dbRecord);
            }
            catch (Exception ex)
            {
                return ResponseService.Error(Errors.CANT_DELETE_PRODUCT_ERROR);
            }
            return ResponseService.Ok();
        }

        public async Task<IEnumerable<ProductEntity>> GetAll() => await _productRepository.GetAll();

        public async Task<IEnumerable<ProductEntity>> GetAll(int page = 1)
        {
            int skip = DataPagination.CalSkipRecords(page, DataPagination.PRODUCTS_ON_PAGE);
            return await _productRepository.GetChunk(skip, DataPagination.PRODUCTS_ON_PAGE);
        }

        public async Task<ResponseService<ProductEntity>> GetById(long id)
        {
            ProductEntity dbRecord = await _productRepository.Find(id);
            if (dbRecord == null)
            {
                return ResponseService<ProductEntity>.Error(Errors.PRODUCT_NOT_FOUND_ERROR);
            }
            return ResponseService<ProductEntity>.Ok(dbRecord);
        }

        public async Task<ResponseService> Update(UpdateProductHttpPostModel vm)
        {
            ProductEntity dbRecord = await _productRepository.Find(vm.Id);
            if (dbRecord == null)
            {
                return ResponseService.Error(Errors.PRODUCT_NOT_FOUND_ERROR);
            }

            dbRecord.Title = vm.Title;
            dbRecord.Description = vm.Description;
            dbRecord.Rate = vm.Rate;
            dbRecord.Price = vm.Price;
            dbRecord.Count = vm.Count;
            
            if (vm.Image != null)
            {
                var removeResult = RemoveImage(dbRecord.ImageName, dbRecord.ImageExtension);
                if (removeResult.IsError)
                {
                    return removeResult;
                }

                var saveResult = SaveImage(vm.Image, dbRecord.Id.ToString());
                if(saveResult.IsError)
                {
                    return ResponseService.Error(saveResult.ErrorMessage);
                }

                dbRecord.ImageExtension = saveResult.Value.Extensions;
                dbRecord.ImageName = saveResult.Value.Name;
            }

            return await Update(dbRecord);
        }

        public async Task<ProductOrderingHttpGetModel> GetAll(ProductOrderingQueryModel vm)
        {
            string query = $"SELECT * FROM {DbTables.PRODUCT_TABLE_NAME} WHERE Rate >= @MinimalRate AND Price >= @MinimalPrice ";
            if (vm.MaximumPrice > vm.MinimalPrice)
            {
                query += "AND Price <= @MaximumPrice ";
            }

            if (vm.IsAvailable)
            {
                query += "AND Count > 0 ";
            }

            query += "ORDER BY ";
            switch (SortTypeDisplay.GetStringAsEnum(vm.Type))
            {
                case SortType.Count:
                    {
                        query += "Price ";
                        break;
                    }
                case SortType.Id:
                    {
                        query += "Id ";
                        break;
                    }
                case SortType.Price:
                    {
                        query += "Price ";
                        break;
                    }
                case SortType.Rate:
                    {
                        query += "Rate ";
                        break;
                    }
                default:
                    {
                        query += "Id ";
                        break;
                    }
            }

            if (SortDirectionDisplay.GetStringAsEnum(vm.Direction) == SortDirection.Descending)
            {
                query += "DESC ";
            }

            int skip = DataPagination.CalSkipRecords(vm.Page, DataPagination.PRODUCTS_ON_PAGE);
            query += "OFFSET @skip ROWS FETCH NEXT @take ROWS ONLY";

            IEnumerable<ProductEntity> products = await _productRepository.GetAll(query, new
            {
                Page = vm.Page,
                MinimalPrice = vm.MinimalPrice,
                MaximumPrice = vm.MaximumPrice,
                MinimalRate = vm.MinimalRate,
                IsAvailable = vm.IsAvailable,
                Type = vm.Type,
                Direction = vm.Direction,
                skip = skip,
                take = DataPagination.PRODUCTS_ON_PAGE,
            });

            return new ProductOrderingHttpGetModel()
            {
                Products = products,
                QueryFilters = vm,
            };
        }

        private async Task<ResponseService> Update(ProductEntity entity)
        {
            try
            {
                await _productRepository.Update(entity);
            }
            catch (Exception ex)
            {
                return ResponseService.Error(Errors.CANT_UPDATE_PRODUCT_ERROR);
            }
            return ResponseService.Ok();
        }

        private ResponseService<ProductImageModel> SaveImage(IFormFile image, string newName)
        {
            string directory = $"{_rootPath}\\{Cofigurations.IMAGE_DIRECTORY_NAME}";
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            string[] originalImageNameParts = image.FileName.Split('.');
            string originalFileExtension = originalImageNameParts.Last();
            string newFileName = $"{newName}.{originalFileExtension}";

            try
            {
                using (FileStream fileStream = new FileStream($"{directory}\\{newFileName}", FileMode.Create))
                {
                    image.CopyTo(fileStream);
                }
            }
            catch (Exception ex)
            {
                return ResponseService<ProductImageModel>.Error(Errors.CANT_SAVE_PRODUCT_IMAGE_ERROR);
            }

            return ResponseService<ProductImageModel>.Ok(new ProductImageModel()
            {
                Extensions = originalFileExtension,
                Name = newName,
            });
        }

        private ResponseService RemoveImage(string name)
        {
            string directory = $"{_rootPath}\\{Cofigurations.IMAGE_DIRECTORY_NAME}";
            if (!Directory.Exists(directory))
            {
                return ResponseService.Ok();
            }

            string[] files = Directory.GetFiles(directory);

            foreach (string file in files)
            {
                string fileName = file.Split('.').First();
                if (fileName == name)
                {
                    try
                    {
                        File.Delete($"{directory}\\{file}");
                    }
                    catch (Exception ex)
                    {
                        return ResponseService.Error(Errors.CANT_REMOVE_PRODUCT_IMAGE_ERROR);
                    }
                    return ResponseService.Ok();
                }
            }
            return ResponseService.Ok();
        }

        private ResponseService RemoveImage(string name, string extension)
        {
            string directory = $"{_rootPath}\\{Cofigurations.IMAGE_DIRECTORY_NAME}";
            if (!Directory.Exists(directory))
            {
                return ResponseService.Ok();
            }

            string[] files = Directory.GetFiles(directory);

            foreach (string file in files)
            {
                string[] fileNameParts = file.Split('.');
                string fileName = fileNameParts.First();
                string fileExtension = fileNameParts.Last();

                if (fileName == name && fileExtension == extension)
                {
                    try
                    {
                        File.Delete($"{directory}\\{file}");
                    }
                    catch (Exception ex)
                    {
                        return ResponseService.Error(Errors.CANT_REMOVE_PRODUCT_IMAGE_ERROR);
                    }
                    return ResponseService.Ok();
                }
            }
            return ResponseService.Ok();
        }
    }
}