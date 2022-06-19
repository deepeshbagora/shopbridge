using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Shopbridge.Data;
using Shopbridge.Domain.Models;
using Shopbridge.Domain.Models.Inventory;
using Shopbridge.Domain.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shopbridge;

namespace Shopbridge.Domain.Services
{
    public class ProductService : IProductService
    {
        private readonly ILogger<ProductService> logger;
        private readonly Shopbridge_Context dbcontext;

        private readonly IBroadCast broadcast;

        public ProductService(Shopbridge_Context _dbcontext, ILogger<ProductService> _logger,IBroadCast _broadcast)
        {
            this.dbcontext = _dbcontext;
            this.logger = _logger;
            this.broadcast = _broadcast;
        }

        public async Task<Product> AddProduct(Product product, bool FromUpdateFunction = false)
        {

            try
            {
                Console.Write(product.ToString());

                if (product.Product_Id == 0)
                {
                    dbcontext.Product.Add(product);
                    await dbcontext.SaveChangesAsync();
                }
                else
                {
                    if (!FromUpdateFunction)
                        product = await UpdateProduct(product);
                }

                return product;

            }
            catch (System.Exception ex)
            {
                logger.LogError(ex.ToString());

                throw;
            }


        }

        public async Task<List<Base_Product>> GetAllProducts()
        {
            try
            {
                List<Base_Product> productList = new List<Base_Product>();
                 Parallel.ForEach<Product>(await dbcontext.Product.ToListAsync<Product>(), product =>
                {
                    productList.Add(StaticHelper.Cast<Base_Product>(product));
                });

                return productList;
            }
            catch (System.Exception)
            {

                throw;
            }

        }
        public async Task<List<Product>> GetCompleteDetailsOfProducts()
        {
            try
            {                
                return await dbcontext.Product.ToListAsync<Product>();
            }
            catch (System.Exception)
            {

                throw;
            }

        }
        public async Task<Product> UpdateProduct(Product product)
        {
            try
            {
                // Product item = await FindProductByID(product.Product_Id);
                Product item = dbcontext.Product.FirstOrDefault(item => item.Product_Id == product.Product_Id);
                if (item != null)
                {

                    item.ProductName = product.ProductName;
                    item.Category = product.Category;
                    item.Description = product.Description;
                    item.Cost = product.Cost;
                    item.Features = product.Features;
                    item.RemainingQuaitity = product.RemainingQuaitity;
                    item.ThreashHoldQuantity = product.ThreashHoldQuantity;
                    item.BuyingPrice = product.BuyingPrice;
                    dbcontext.Update<Product>(item);
                    await dbcontext.SaveChangesAsync();
                }
                else
                {
                    product.Product_Id = 0;
                    product = await AddProduct(product, true);
                }

                return product;
            }
            catch (System.Exception)
            {

                throw;
            }

        }

        public async Task<Base_Product> FindProductByID(int ProductID)
        {

            try
            {
                return StaticHelper.Cast<Base_Product>(await dbcontext.FindAsync<Product>(ProductID));
            }
            catch (System.Exception)
            {

                throw;
            }

        }

        public async Task<Product> FindProductCompleteByID(int ProductID)
        {

            try
            {
                return await dbcontext.FindAsync<Product>(ProductID);
            }
            catch (System.Exception)
            {

                throw;
            }

        }

        public async Task<bool> DeleteProduct(int Product_Id)
        {

            try
            {
                Product item = await FindProductCompleteByID(Product_Id);
                if (item != null)
                {

                    dbcontext.Remove(item);
                    await dbcontext.SaveChangesAsync();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (System.Exception)
            {

                throw;
            }
        }

        public async Task<int> GetProductIDByName(string ProductName,string Category = ""){
            try
            {
                Product P = await dbcontext.Product.FirstOrDefaultAsync(i => i.ProductName == ProductName && i.Category == (Category != "" ? Category : i.Category));
                return P.Product_Id;
            }
            catch (System.Exception)
            {
                
                throw;
            }
        }

        public async Task<Product> AddorRemoveProduct(InventoryObject product,ProductQuantOperations operation){
            try
            {
                 Product item = await FindProductCompleteByID(product.ProductID);
                if(item != null){
                     item.RemainingQuaitity += operation == ProductQuantOperations.Add ? product.Quantity : (product.Quantity * -1);
                Product Updateditem = await UpdateProduct(item);

                if(operation == ProductQuantOperations.Remove && Updateditem.RemainingQuaitity < Updateditem.ThreashHoldQuantity)
                    PushThresholdAlert(Updateditem);

                return Updateditem;
                }
               return item;
            }
            catch (System.Exception)
            {

                throw;
            }
        }


        public void PushThresholdAlert(Product products){
            BroadCastModel br = new BroadCastModel{
                Reason="BELOWTHREASHOLD",
                Description="Very few quantity of Product Name: " + products.ProductName + " left please hurry."
            };

            broadcast.Broadcast(br);
            
        }

        public async Task<double> GetInventoryCurrentValue(){
           return await dbcontext.Product.SumAsync(i => i.Cost);
        }

        public async Task<List<InventoryListObj>> GetCurrentInventory(){
            
            List<InventoryListObj> Inventory = new List<InventoryListObj>();               

            var items = await dbcontext.Product//.Where(x => x.Id == id)
                  .Select(x => new
                               {
                                    P1 = x.ProductName,
                                    P2 = x.Cost
                               }).ToListAsync();
            foreach (var item in items)
            {               
                Inventory.Add(new InventoryListObj{ ProductName= item.P1,Cost = item.P2});
            }
            return Inventory;
        }

        public async Task<List<Product>> GetInventoryThreshholdReport(){
            List<Product> item = await dbcontext.Product.Where(x => x.RemainingQuaitity < x.ThreashHoldQuantity).ToListAsync();
            return item;
           
        }

    }
}
