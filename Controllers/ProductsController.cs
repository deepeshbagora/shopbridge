using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Shopbridge.Data;
using Shopbridge.Domain.Models;
using Shopbridge.Domain.Services.Interfaces;

namespace Shopbridge.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService productService;
        private readonly ILogger<ProductsController> logger;
        public ProductsController(IProductService _productService)
        {
            this.productService = _productService;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Base_Product>>> GetProduct()
        {
            try
            {
                return await productService.GetAllProducts();
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, ex);
                // throw;
            }

        }
        [HttpGet("Complete")]
        public async Task<ActionResult<IEnumerable<Base_Product>>> GetCompleteDetailsOfProducts()
        {
            try
            {
                return await productService.GetCompleteDetailsOfProducts();
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, ex);
                // throw;
            }

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Base_Product>> GetProduct(int id)
        {
            try
            {
                
                return await productService.FindProductByID(ProductID: id);
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, ex);
                // throw;
            }
        }

        [HttpGet("Complete/{id}")]
        public async Task<ActionResult<Product>> GetCompleteDetailsOfProductsByID(int id)
        {
            try
            {
                
                return await productService.FindProductCompleteByID(ProductID: id);
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, ex);
                // throw;
            }
        }

        // [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, Product product)
        {
            try
            {                
                product.Product_Id = id != product.Product_Id ? id : product.Product_Id;
                return Ok(await productService.UpdateProduct(product));
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, ex);
                // throw;
            }
        }

        //[Authorize]
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            try
            {
                return await productService.AddProduct(product);
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, ex);
                //throw;
            }

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            try
            {
                return Ok(await productService.DeleteProduct(Product_Id: id));
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, ex);
                // throw;
            }
        }

        private bool ProductExists(int id)
        {
            try
            {
                return productService.FindProductByID(id).Result != null ? true : false;
            }
            catch (System.Exception)
            {
                throw;
            }

        }
    }
}
