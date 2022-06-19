using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shopbridge.Domain.Models.Inventory;
using Shopbridge.Domain.Models;
namespace Shopbridge.Domain.Services.Interfaces;

public interface IInventory
{
    Task<double> GetInventoryValue();

    Task<List<InventoryListObj>> GetCurrentInventory();

    Task<int> GetRemainingQuantityofProduct(int ProdictID);
    
    Task<Product> AddQuantityofProduct(InventoryObject Product);

    Task<Product> ReduceQuantityOfProduct(InventoryObject Product);
    /// <summary>
     /// This Method will be used to Add quantity of multiple products like when new order from vendors comes in.
     /// </summary>
    Task<bool> BulkAddquantityofProduct(List<InventoryObject> Products);

    /// <summary>
     /// This Method will be used to reduce quantity of multiple products like after an order is placed.
     /// </summary>
    Task<bool> BulkReduceQuantityofProduct(List<InventoryObject> Products);

    Task<List<Product>> GetInventoryThreshholdReport();

}