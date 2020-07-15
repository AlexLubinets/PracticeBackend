using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProductApp.BLL.Constants;
using ProductApp.BLL.Interfaces;
using ProductApp.BLL.Models;

namespace ProductApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductController : ControllerBase
    {
        private readonly IProductService productService;

        public ProductController(IProductService productService)
        {
            this.productService = productService;
        }

        [HttpGet]
        //GET: api/Product
        public IActionResult GetProducts([FromQuery]ProductPagingParams pagingParams)
        {
            var products = productService.GetProductsSegment(pagingParams);

            var metadata = new
            {
                products.TotalCount,
                products.PageSize,
                products.PageNumber,
                products.TotalPages,
                products.HasNext,
                products.HasPrevious,
                pagingParams.SortOrder
            };
            Response.Headers.Add("Pagination", JsonConvert.SerializeObject(metadata));
            return Ok(products);
        }

        [HttpGet]
        [Route("{id}")]
        //GET: api/Product/id
        public async Task<IActionResult> GetProductDetails(int id)
        {
            var item = await productService.GetProductById(id);
            if (item == null)
                return BadRequest(ErrorMessages.ProductNotFound);
            return Ok(item);
        }

        [HttpPost]
        //GET: api/Product
        public async Task<IActionResult> CreateProduct(ProductDTO model)
        {
            try
            {
                var item = await productService.AddProduct(model);
                return Ok(item);
            }
            catch (ArgumentOutOfRangeException)
            {
                return BadRequest(ErrorMessages.InvalidPrice);
            }
            catch (ArgumentException)
            {
                return BadRequest(ErrorMessages.InvalidProductName);
            }
            catch
            {
                return BadRequest(ErrorMessages.ProductAlreadyExists);
            }
        }

        [HttpDelete]
        [Route("{id}")]
        //GET: api/Product/id
        public async Task<IActionResult> DeleteProduct(int id)
        {
            try
            {
                await productService.DeleteProductById(id);
                return Ok();
            }
            catch
            {
                return BadRequest(ErrorMessages.ProductNotFound);
            }
        }

        [HttpPut]
        //GET: api/Product
        public async Task<IActionResult> UpdateProduct(ProductDTO modelChanges)
        {
            try
            {
                return Ok(await productService.ChangeProduct(modelChanges));
            }
            catch (ArgumentOutOfRangeException)
            {
                return BadRequest(ErrorMessages.InvalidPrice);
            }
            catch (ArgumentException)
            {
                return BadRequest(ErrorMessages.InvalidProductName);
            }
            catch
            {
                return BadRequest(ErrorMessages.ProductAlreadyExists);
            }
        }
    }
}