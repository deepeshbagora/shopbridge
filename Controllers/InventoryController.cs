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
using Shopbridge.Domain.Models.Inventory;
using Shopbridge.Domain.Services.Interfaces;

namespace Shopbridge.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private readonly IInventory inventory;
        private readonly ILogger<InventoryController> logger;
        public InventoryController(IInventory _inventory) => this.inventory = _inventory;


        [HttpGet]
        public async Task<double> GetInventoryValue()
        {
           return await inventory.GetInventoryValue();
        }

        [HttpGet("List")]
        public async Task<List<InventoryListObj>> GetCurrentInventory()
        {
          
           return await inventory.GetCurrentInventory();
        }

        [HttpGet("ThresholdReport")]
        public async Task<List<Product>> GetInventoryThreshholdReport()
        {
          
           return await inventory.GetInventoryThreshholdReport();
        }

        [HttpPost("AddQuantity")]
        public async Task<Product> AddQuantityOFProduct(InventoryObject ProdQuantity)
        {
          
           return await inventory.AddQuantityofProduct(ProdQuantity);
        }

        [HttpPost("ReduceQuantity")]
        public async Task<Product> ReduceQuantityOFProduct(InventoryObject ProdQuantity)
        {
          
           return await inventory.ReduceQuantityOfProduct(ProdQuantity);
        }
       
    }
}
