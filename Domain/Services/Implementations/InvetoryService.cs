

using System.Collections.Generic;
using System.Threading.Tasks;
using Shopbridge.Domain.Models;
using Shopbridge.Domain.Models.Inventory;
using Shopbridge.Domain.Services.Interfaces;

namespace Shopbridge;

public class Inventory : IInventory
{
    private IProductService productService;
    public Inventory(IProductService _productservice)
    {
        productService = _productservice;
    }


    public async Task<Product> AddQuantityofProduct(InventoryObject Product)
    {
        try
        {
            return await productService.AddorRemoveProduct(product: Product, ProductQuantOperations.Add);
        }
        catch (System.Exception)
        {

            throw;
        }
    }

    public async Task<bool> BulkAddquantityofProduct(List<InventoryObject> Products)
    {
        try
        {
            await Parallel.ForEachAsync(Products, async (item, cancellationToken) =>
            {

                await productService.AddorRemoveProduct(product: item, ProductQuantOperations.Add);

            });
           return true;
        }
        catch (System.Exception)
        {

            throw;
        }
    }

    public async Task<bool> BulkReduceQuantityofProduct(List<InventoryObject> Products)
    {
        try
        {
            await Parallel.ForEachAsync(Products, async (item, cancellationToken) =>
            {

                await productService.AddorRemoveProduct(product: item, ProductQuantOperations.Remove);

            });
           return true;
        }
        catch (System.Exception)
        {

            throw;
        }
    }

    public async Task<List<InventoryListObj>> GetCurrentInventory()
    {
        return await productService.GetCurrentInventory();
    }

    public async Task<List<Product>> GetInventoryThreshholdReport()
    {
        return await productService.GetInventoryThreshholdReport();
    }

    public async Task<double> GetInventoryValue()
    {
        return await productService.GetInventoryCurrentValue();
        
        //throw new System.NotImplementedException();
    }

    public async Task<int> GetRemainingQuantityofProduct(int ProductID)
    {
        Product P = await productService.FindProductCompleteByID(ProductID);
        return P.RemainingQuaitity;
    }

    public async Task<Product> ReduceQuantityOfProduct(InventoryObject Product)
    {
        try
        {
            return await productService.AddorRemoveProduct(product: Product, ProductQuantOperations.Remove);
        }
        catch (System.Exception)
        {

            throw;
        }
    }
}