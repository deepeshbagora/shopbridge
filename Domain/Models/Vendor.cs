using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Shopbridge.Domain.Models
{
    public class Vendor
    {
        [Description("This field specifies Unique key to identify Vendor Seperately")]
        [Key]
        public int ID { get; set; }

        [Description("This field specifies Name of a specific product")]
        public string VendorName {get;set;}
        [Description("This field specifies Cost of a specific product")]
        public string Address {get;set;}
        [Description("This field specifies Category which product belongs to.")]
        public string Email {get;set;}
        [Description("This field navigates Vendor specific details for product")]
        public int ContactNo {get;set;}
        [Description("This field contains json format string as Key-value pairs for additional properties applies to product")]
        public string GSTIN {get;set;}  
        [Description("This field specifies Bank details of the Vendor")]
        public BankDetails BankDetails { get; set; }

    }

    public class BankDetails{

        [Description("This field specifies Unique key to identify BankDetails Seperately")]
        [Key]
        public int ID { get; set; }

        [Description("This field specifies Account Number of Vendor")]
        public long AccountNo{get;set;}

        [Description("This field specifies Name of Bank in which vendor holds Account")]
        public string BankName { get; set; }
        [MaxLength(11)]
        [Description("This field specifies IFSC code of Bank in which vendor holds Account")]
        public string IFSC { get; set; }

        [Description("This field specifies Type of Account which vendor holds")]
        public string AccountType { get; set; }

    }
}
