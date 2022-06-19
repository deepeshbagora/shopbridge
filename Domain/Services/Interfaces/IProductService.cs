using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shopbridge.Domain.Models;
using Shopbridge.Domain.Models.Inventory;

namespace Shopbridge.Domain.Services.Interfaces
{
    public interface IProductService
    {
        Task<List<Base_Product>> GetAllProducts();

        Task<List<Product>> GetCompleteDetailsOfProducts();

        Task<Product> AddProduct(Product product,bool FromUpdateFunction=false);

        Task<Product> UpdateProduct(Product product);

        Task<Base_Product> FindProductByID(int ProductID);

        Task<Product> FindProductCompleteByID(int ProductID);

        Task<bool> DeleteProduct(int Product_Id);

        Task<Product> AddorRemoveProduct(InventoryObject product,ProductQuantOperations operation);

        void PushThresholdAlert(Product product);

        Task<double> GetInventoryCurrentValue();

        Task<List<InventoryListObj>> GetCurrentInventory();

        Task<List<Product>> GetInventoryThreshholdReport();
    }
}
