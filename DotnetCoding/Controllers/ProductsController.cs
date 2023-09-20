using Microsoft.AspNetCore.Mvc;
using DotnetCoding.Core.Models;
using DotnetCoding.Services.Interfaces;
using DotnetCoding.Core.Models.Request;

namespace DotnetCoding.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        public readonly IProductService _productService;
        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        /// <summary>
        /// Get the list of product
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetProductList()
        {
            var productDetailsList = await _productService.GetAllProducts();
            if (productDetailsList == null)
            {
                return NotFound();
            }
            return Ok(productDetailsList);
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddProduct([FromBody] ProductDetails request)
        {
            try
            {
                var createdProduct = await _productService.CreateProduct(request);

                return Ok(createdProduct);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }


        [HttpPut("update/{productId}")]
        public async Task<IActionResult> UpdateProduct(int productId, [FromBody] ProductDetails request)
        {
            try
            {
                request.Id = productId;

                await _productService.UpdateProduct(request);

                return Ok("Product updated successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpDelete("delete/{productId}")]
        public async Task<IActionResult> DeleteProduct(int productId)
        {
            try
            {
                await _productService.DeleteProduct(productId);

                return Ok("Product pending approval to delete.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpPost("search")]
        public async Task<IActionResult> SearchProducts([FromBody] ProductSearchCriteria criteria)
        {
            try
            {
                // Get all active products
                var activeProducts = await _productService.GetAllProducts();

                var filteredProducts = ApplyProductFilters(activeProducts, criteria);

                return Ok(filteredProducts);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        private IEnumerable<ProductDetails> ApplyProductFilters(IEnumerable<ProductDetails> products, ProductSearchCriteria criteria)
        {
            // Filter by Product name (case-insensitive contains)
            if (!string.IsNullOrEmpty(criteria.ProductName))
            {
                products = products.Where(p => p.ProductName.IndexOf(criteria.ProductName, StringComparison.OrdinalIgnoreCase) >= 0);
            }

            // Filter by Price range
            if (criteria.MinPrice.HasValue)
            {
                products = products.Where(p => p.ProductPrice >= criteria.MinPrice.Value);
            }

            if (criteria.MaxPrice.HasValue)
            {
                products = products.Where(p => p.ProductPrice <= criteria.MaxPrice.Value);
            }

            return products;
        }


    }
}
