using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Shopbridge.Domain.Models.Inventory
{
    public class InventoryObject
    {
        public int ProductID { get; set; }        

        public int Quantity { get; set; }
    }

    public enum ProductQuantOperations{
        Add,
        Remove
    }

    public class InventoryListObj{
        public string ProductName { get; set; }

        public float Cost { get; set; }
    }
}
