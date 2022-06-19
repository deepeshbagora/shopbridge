using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Shopbridge.Domain.Models
{
    public class Base_Product
    {

        [Key]
        public int Product_Id { get; set; }

        [Description("This field specifies Name of a specific product")]
        public string ProductName {get;set;}
        [Description("This field specifies Cost of a specific product")]
        public float Cost {get;set;}

        [Description("This field specifies Category which product belongs to.")]
        public string Category {get;set;}

        public string Description {get;set;}

        public string Features { get; set; }

    }

    public class Product : Base_Product {
        
        public int RemainingQuaitity { get; set; }

        public int ThreashHoldQuantity { get; set; }

        public float BuyingPrice {get;set;}

        public int VendorID{get;set;}

    }
}
