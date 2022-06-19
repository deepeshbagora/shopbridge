using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Shopbridge.Domain.Models;

namespace Shopbridge.Data
{
    public class Shopbridge_Context : DbContext
    {
        public Shopbridge_Context (DbContextOptions<Shopbridge_Context> options)
            : base(options)
        {
        }

        public DbSet<Product> Product { get; set; }
        public DbSet<Vendor> Vendors{get;set;}
    }
}
