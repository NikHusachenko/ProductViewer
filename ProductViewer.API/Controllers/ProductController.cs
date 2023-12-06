using Microsoft.AspNetCore.Mvc;
using ProductViewer.Services.ProductService;
using ProductViewer.Services.ProductService.Models;
using ProductViewer.Services.ProductService.Models.Query;

namespace ProductViewer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        [Route("/[action]")]
        public async Task<IActionResult> Producs([FromQuery] ProductOrderingQueryModel vm)
        {
            return Ok(await _productService.GetAll(vm));
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create([FromForm] CreateProductHttpPostModel vm)
        {
            var response = await _productService.Create(vm);
            if (response.IsError)
            {
                return BadRequest(new { responseMessage = response.ErrorMessage });
            }
            return Ok(new { id = response.Value });
        }
    }
}